using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Entities
{
    public abstract class Place
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } 
        public string? Address { get; private set; }
        public string? ContactData { get; private set; }
        public string? Description { get; private set; }
        public byte? AllowAge { get; private set; }
        public Coordinates PositionPoint { get; private set; }
        private HashSet<Tag> _tags = new();

        public IReadOnlyCollection<Tag> Tags => _tags;

        protected Place( string name, string? address, string? contactData, 
            string? description, byte? allowAge, Coordinates positionPoint, IReadOnlyCollection<Tag>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название места не может быть пустым", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            ContactData = contactData;
            Description = description;
            AllowAge = allowAge;
            PositionPoint = positionPoint ?? throw new ArgumentNullException(nameof(positionPoint), "Необходимо задать координаты места");
            foreach (var tag in tags ?? Enumerable.Empty<Tag>())
            {
                _tags.Add(tag);
            }
        }
    }
} 
