namespace TourConstructor.Domain.Entities
{
    public class Tag : IEquatable<Tag>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Tag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название тега не может быть пустым", nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim().ToLowerInvariant();
        }

        public bool Equals(Tag? otherTag)
        {
            if (otherTag is null)
                return false;
            else if (ReferenceEquals(this, otherTag))
                return true;

            return Name == otherTag.Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Tag tag)
                return Equals(tag);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
