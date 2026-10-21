using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Application.Models
{
    public record RouteStop(Guid PlaceId, PlaceKind Kind, string Name, string? ContactData, Coordinates Position,
                             int TravelMinutes, DateTimeOffset ArriveAt, DateTimeOffset LeaveAt, decimal Cost,
                             IReadOnlyCollection<string> MatchedTags);
}
