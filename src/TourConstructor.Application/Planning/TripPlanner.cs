using TourConstructor.Application.Interfaces;
using TourConstructor.Application.Models;
using TourConstructor.Domain.Entities;

namespace TourConstructor.Application.Planning
{
    public class TripPlanner
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly ITravelTimeProvider _travelTimeProvider;

        public TripPlanner(IPlaceRepository placeRepository, ITravelTimeProvider travelTimeProvider)
        {
            _placeRepository = placeRepository;
            _travelTimeProvider = travelTimeProvider;
        }

        public async Task<TripPlan> PlanAsync(TripCriteria criteria, CancellationToken ct)
        {
            var halfOfTripTime = (criteria.ReturnTime - criteria.DepartureTime) / 2;
            
            double radiusKm = _travelTimeProvider.GetMaxDistanceKm(halfOfTripTime);

            var places = await _placeRepository.GetCandidatesAsync(criteria.Start, radiusKm, ct);

            if (places.Count == 0)
                return new TripPlan([], NoRouteReason.NoPlacesInRadius, 0);

            var placesAfterDining = ExcludeDiningIfNotIncluded(places, criteria);
            if (placesAfterDining.Count == 0)
                return new TripPlan([], NoRouteReason.OnlyDiningNearby, places.Count);
            

            throw new NotImplementedException();
        }

        private List<Place> ExcludeDiningIfNotIncluded(IReadOnlyList<Place> allPlaces, TripCriteria criteria)
            => allPlaces
                .Where(place => criteria.IncludeDining || place is not Dining)
                .ToList();
    }
}
