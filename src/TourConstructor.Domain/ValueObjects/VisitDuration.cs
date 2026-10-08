namespace TourConstructor.Domain.ValueObjects
{
    public record VisitDuration
    {
        public int MinMinutes { get; }
        public int TypicalMinutes { get; }
        public int MaxMinutes { get; }

        public VisitDuration(int minMinutes, int typicalMinutes, int maxMinutes)
        {
            if(minMinutes <= 0 )
                throw new ArgumentException("Невозможно указать длительность меньше или равную 0");
            if (minMinutes > typicalMinutes || typicalMinutes > maxMinutes)
                throw new ArgumentException("Укажите корректное время");
            MinMinutes = minMinutes;
            TypicalMinutes = typicalMinutes;
            MaxMinutes = maxMinutes;
        }
    }
}
