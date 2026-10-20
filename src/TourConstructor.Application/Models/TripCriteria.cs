using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Application.Models
{
    public record TripCriteria(Coordinates Start, DateTimeOffset DepartureTime, DateTimeOffset ReturnTime,
                                IReadOnlyCollection<string> Tags, int PeopleCount, byte? YoungestAge, decimal? Budget);
}
