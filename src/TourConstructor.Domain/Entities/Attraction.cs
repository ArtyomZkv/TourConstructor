using TourConstructor.Domain.Interfaces;
using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Entities
{
    public class Attraction : Place, IVisitable
    {
        public decimal EntryPrice { get; private set; }
        public VisitDuration VisitDuration { get; private set; }

        public Attraction(string name, string? address,
            string? contactData, string? description, byte? allowAge,
            Coordinates positionPoint, decimal entryPrice, VisitDuration visitDuration, IReadOnlyCollection<Tag>? tags = null)
            : base(name, address, contactData, description, allowAge, positionPoint, tags)
        {
            if (entryPrice < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            EntryPrice = entryPrice;
            VisitDuration = visitDuration ?? throw new ArgumentNullException(nameof(visitDuration), "Необходимо задать длительность посещения");
        }
    }
}
