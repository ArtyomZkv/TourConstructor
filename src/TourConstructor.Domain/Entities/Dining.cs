using TourConstructor.Domain.Interfaces;
using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Entities
{
    public class Dining : Place, IVisitable
    {
        public decimal AverageCheck { get; private set; }
        public VisitDuration VisitDuration { get; private set; }

        public Dining(string name, string? address,
            string? contactData, string? description, byte? allowAge,
            Coordinates positionPoint, decimal averageCheck, VisitDuration visitDuration, IReadOnlyCollection<Tag>? tags = null)
            : base(name, address, contactData, description, allowAge, positionPoint, tags)
        {
            if (averageCheck < 0)
                throw new ArgumentException("Средний чек не может быть отрицательным");
            AverageCheck = averageCheck;
            VisitDuration = visitDuration ?? throw new ArgumentNullException(nameof(visitDuration), "Необходимо задать длительность посещения");
        }
    }
}
