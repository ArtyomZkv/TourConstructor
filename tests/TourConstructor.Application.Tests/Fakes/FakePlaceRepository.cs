using TourConstructor.Domain.Entities;
using TourConstructor.Domain.ValueObjects;
using TourConstructor.Application.Interfaces;


namespace TourConstructor.Application.Tests.Fakes
{
    class FakePlaceRepository : IPlaceRepository
    {
        public List<Place> Places { get; } = new();

        public Task<IReadOnlyList<Place>> GetCandidatesAsync(Coordinates start, double radiusKm, CancellationToken ct)
            => Task.FromResult<IReadOnlyList<Place>>(Places);
    }
}
