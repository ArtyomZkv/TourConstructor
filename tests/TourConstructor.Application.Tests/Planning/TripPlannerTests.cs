using TourConstructor.Application.Tests.Fakes;
using TourConstructor.Application.Planning;
using TourConstructor.Application.Models;
using TourConstructor.Domain.ValueObjects;

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
    }
}
