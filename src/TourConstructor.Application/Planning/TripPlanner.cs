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

            var placesAfterDining = places.Where(place => criteria.IncludeDining || place is not Dining).ToList();
            if (placesAfterDining.Count == 0)
                return new TripPlan([], NoRouteReason.OnlyDiningNearby, places.Count);

            var placesAfterAllowAge = placesAfterDining.Where(place => place.AllowAge <= criteria.YoungestAge || criteria.YoungestAge is null || place.AllowAge is null).ToList();
            if (placesAfterAllowAge.Count == 0)
                return new TripPlan([], NoRouteReason.AgeRestricted, placesAfterDining.Count);
        
            var placesAfterGroupSize = placesAfterAllowAge.Where(place => place is not Guide guide || guide.GroupSize >= criteria.PeopleCount).ToList();
            if (placesAfterGroupSize.Count == 0)
                return new TripPlan([], NoRouteReason.GroupTooLarge, placesAfterAllowAge.Count);

            throw new NotImplementedException();
        }
    }
}
