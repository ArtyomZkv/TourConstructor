using TourConstructor.Domain.Interfaces;
using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Entities
{
    public class Guide: Place, IVisitable
    {
        public string Languages { get; private set; }
        public int GroupSize { get; private set; }
        public VisitDuration VisitDuration { get; private set; }
        public decimal Price { get; private set; }
        public Guide(string name, string? address,
            string? contactData, string? description, byte? allowAge,
            Coordinates positionPoint, string languages, int groupSize, VisitDuration visitDuration, decimal price, IReadOnlyCollection<Tag>? tags = null)
            : base(name, address, contactData, description, allowAge, positionPoint, tags)
        {
            if (groupSize <= 0)
                throw new ArgumentException("Размер группы не может быть отрицательным или 0");
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            if (string.IsNullOrWhiteSpace(languages))
                throw new ArgumentException("Необходимо указать хотя бы один язык экскурсии", nameof(languages));

            Languages = languages;
            GroupSize = groupSize;
            VisitDuration = visitDuration ?? throw new ArgumentNullException(nameof(visitDuration), "Необходимо задать длительность экскурсии");
            Price = price;
        }
    }
}
