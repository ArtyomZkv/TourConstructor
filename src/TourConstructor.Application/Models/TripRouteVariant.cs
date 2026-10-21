namespace TourConstructor.Application.Models
{
    public record TripRouteVariant(IReadOnlyList<RouteStop> Stops, int ReturnTravelMinutes,
                                    DateTimeOffset ReturnAt, decimal TotalCost);
}
