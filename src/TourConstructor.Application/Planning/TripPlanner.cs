using TourConstructor.Application.Interfaces;
using TourConstructor.Application.Models;

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

            throw new NotImplementedException();
        }

    }
}
