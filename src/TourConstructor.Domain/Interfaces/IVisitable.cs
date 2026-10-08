using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Interfaces
{
    /// <summary>
    /// Место, визит в которое занимает время в маршруте.
    /// </summary>
    public interface IVisitable
    {
        VisitDuration VisitDuration { get; }
    }
}
