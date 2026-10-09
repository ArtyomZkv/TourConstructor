using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Entities
{
    public class Accommodation : Place
    {
        public Accommodation(string name, string? address,
            string? contactData, string? description, byte? allowAge,
            Coordinates positionPoint, IReadOnlyCollection<Tag>? tags = null)
            : base(name, address, contactData, description, allowAge, positionPoint, tags)
        {
        }
    }
}
