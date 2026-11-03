using TourConstructor.Application.Tests.Fakes;
using TourConstructor.Application.Planning;
using TourConstructor.Application.Models;
using TourConstructor.Domain.ValueObjects;
using TourConstructor.Domain.Entities;

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
    }
}
