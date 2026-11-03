using TourConstructor.Domain.ValueObjects;
using TourConstructor.Application.Interfaces;

namespace TourConstructor.Application.Tests.Fakes
{
    class FakeTravelTimeProvider : ITravelTimeProvider
    {
        public int[,] Minutes { get; set; } = new int[0, 0];
        public double MaxDistanceResult { get; } = 200.3;

        public Task<int[,]> GetMinutesMatrixAsync(IReadOnlyList<Coordinates> points, CancellationToken ct)
            => Task.FromResult(Minutes);

        public double GetMaxDistanceKm(TimeSpan travelTime)
            => MaxDistanceResult;
    }
}
