using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Domain.Interfaces
{
    /// <summary>
    /// Место, визит в которое занимает время в маршруте и стоит денег.
    /// </summary>
    public interface IVisitable
    {
        /// <summary>
        /// Правила длительности визита.
        /// </summary>
        VisitDuration VisitDuration { get; }

        /// <summary>
        /// Сколько всего заплатит группа за визит в это место.
        /// Как считать, решает само место: например, цена за человека умножается на число людей, а цена за экскурсию берётся как есть.
        /// </summary>
        /// <param name="peopleCount">Число людей в группе.</param>
        /// <returns>Стоимость визита для всей группы.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Число людей меньше 1 или место не принимает группу такого размера.
        /// </exception>
        decimal GetCostFor(int peopleCount);
    }
}
