using TourConstructor.Application.Tests.Fakes;
using TourConstructor.Application.Planning;
using TourConstructor.Application.Models;
using TourConstructor.Domain.ValueObjects;
using TourConstructor.Domain.Entities;
using TourConstructor.Domain.Enums;

namespace TourConstructor.Application.Tests.Planning
{
    public class TripPlannerTests
    {
        [Fact]
        public async Task PlanAsync_NoPlacesInRadius_ReturnsNoPlacesInRadiusReason()
        {
            //Arrange
            var planner = new TripPlanner(new FakePlaceRepository(), new FakeTravelTimeProvider());

            var departureTime = new DateTimeOffset(2026, 11, 7, 9, 0, 0, TimeSpan.FromHours(3));

            var criteria = new TripCriteria(new Coordinates(56.32, 44.0), departureTime, departureTime.AddHours(10),
                new[] { "музей"}, 2, null, null, false);

            //Act
            var plan = await planner.PlanAsync(criteria, CancellationToken.None);

            // Assert

            Assert.Empty(plan.Variants);
            Assert.Equal(NoRouteReason.NoPlacesInRadius, plan.Reason);
            Assert.Equal(0, plan.PlacesBeforeFailure);
            
        }

        [Fact]
        public async Task PlanAsync_WhenDiningExcluded_ReturnsOnlyDiningNearbyReason()
        {
            //Arrange
            FakePlaceRepository repositoryWithDinnerPlace = new();

            repositoryWithDinnerPlace.Places.Add(new Dining("Пельменная", "г. Нижний Новгород, Ул. Тестовая, 18",
                null, null, null, new Coordinates(56.33, 44.01), 2000, new VisitDuration(10, 30, 60), [new Tag("Пельмени")]));

            var planner = new TripPlanner(repositoryWithDinnerPlace, new FakeTravelTimeProvider());

            var departureTime = new DateTimeOffset(2026, 11, 7, 9, 0, 0, TimeSpan.FromHours(3));

            var criteria = new TripCriteria(new Coordinates(56.32, 44.0), departureTime, departureTime.AddHours(10),
                ["Музей"], 2, null, null, false);

            //Act
            var plan = await planner.PlanAsync(criteria, CancellationToken.None);

            //Assert
            Assert.Empty(plan.Variants);
            Assert.Equal(NoRouteReason.OnlyDiningNearby, plan.Reason);
            Assert.Equal(1, plan.PlacesBeforeFailure);

        }

        [Fact]
        public async Task PlanAsync_WhenYoungestBelowAllowAge_ReturnsAgeRestrictedReason()
        {
            //Arrange
            FakePlaceRepository repositoryWithAllowAge = new();
            var normalPlace = new Attraction("Название", "г. Нижний Новгород, Ул. Тестовая, 18",
                null, null, 10, new Coordinates(56.33, 44.01), 2000, new VisitDuration(10, 30, 60), [new Tag("Тестовый")]);
            
            var placeWithDining = new Dining("Пельменная", "г. Нижний Новгород, Ул. Тестовая, 18",
                null, null, 14, new Coordinates(56.33, 44.01), 2000, new VisitDuration(10, 30, 60), [new Tag("Пельмени")]);

            repositoryWithAllowAge.Places.AddRange(normalPlace, placeWithDining);

            var planner = new TripPlanner(repositoryWithAllowAge, new FakeTravelTimeProvider());

            var departureTime = new DateTimeOffset(2026, 11, 7, 9, 0, 0, TimeSpan.FromHours(3));

            var criteria = new TripCriteria(new Coordinates(56.32, 44.0), departureTime, departureTime.AddHours(10), ["Музей"], 2, 9, null, false);

            //Act
            var plan = await planner.PlanAsync(criteria, CancellationToken.None);

            //Assert
            Assert.Empty(plan.Variants);
            Assert.Equal(NoRouteReason.AgeRestricted, plan.Reason);
            Assert.Equal(1, plan.PlacesBeforeFailure);
        }

        [Fact]
        public async Task PlanAsync_WhenGroupExceedsGuideGroupSize_ReturnsGroupTooLargeReason()
        {
            //Arrange
            FakePlaceRepository repositoryWithGuidePlaces = new();
            var placeWithGroupSize = new Guide("Гид по Кремлю", null, null, null, 14, 
                new Coordinates(56.33, 44.01), "русский", 4, new VisitDuration(10, 30, 60), 1000, PriceKind.PerPerson, [new Tag("гид")]);

            var placeWithGroupSize2 = new Guide("Гид по Кремлю", null, null, null, 14, 
                new Coordinates(56.33, 44.01), "русский", 4, new VisitDuration(10, 30, 60), 1000, PriceKind.PerPerson, [new Tag("гид")]);

            var placeWithDining = new Dining("Пельменная", "г. Нижний Новгород, Ул. Тестовая, 18",
                null, null, 14, new Coordinates(56.33, 44.01), 2000, new VisitDuration(10, 30, 60), [new Tag("Пельмени")]);

            repositoryWithGuidePlaces.Places.AddRange(placeWithGroupSize, placeWithGroupSize2, placeWithDining);

            var planner = new TripPlanner(repositoryWithGuidePlaces, new FakeTravelTimeProvider());

            var departureTime = new DateTimeOffset(2026, 11, 7, 9, 0, 0, TimeSpan.FromHours(3));

            var criteria = new TripCriteria(new Coordinates(56.32, 44.0), departureTime, departureTime.AddHours(10), ["Музей"], 5, null, null, false);

            //Act
            var plan = await planner.PlanAsync(criteria, CancellationToken.None);

            //Assert
            Assert.Empty(plan.Variants);
            Assert.Equal(NoRouteReason.GroupTooLarge, plan.Reason);
            Assert.Equal(2, plan.PlacesBeforeFailure);
        }
    }
}
