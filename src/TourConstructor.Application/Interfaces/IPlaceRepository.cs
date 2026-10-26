using TourConstructor.Domain.Entities;
using TourConstructor.Application.Models;

namespace TourConstructor.Application.Interfaces
{
    /// <summary>
    /// Хранилище мест
    /// </summary>
    public interface IPlaceRepository
    {
        /// <summary>
        /// Выбирает места, которые подходят под жёсткие ограничения запроса:
        /// <list type="bullet">
        /// <item>только места, реализующие <see cref="Domain.Interfaces.IVisitable"/>;</item>
        /// <item>расстояние по прямой от <see cref="TripCriteria.Start"/> не больше <paramref name="radiusKm"/>;</item>
        /// <item>AllowAge не задан или AllowAge &lt;= <see cref="TripCriteria.YoungestAge"/> (если YoungestAge равен null, возрастной ценз не проверяется);</item>
        /// <item>у гидов GroupSize &gt;= <see cref="TripCriteria.PeopleCount"/>;</item>
        /// <item>если <see cref="TripCriteria.IncludeDining"/> выключен, кафе не возвращаются;</item>
        /// <item>если задан <see cref="TripCriteria.Budget"/>: цена за человека × PeopleCount не больше бюджета (у кафе цена — средний чек).</item>
        /// </list>
        /// По тегам и по времени визита не фильтрует.
        /// </summary>
        /// <param name="criteria">Параметры поездки.</param>
        /// <param name="radiusKm">Максимальное расстояние по прямой от старта, км.</param>
        /// <param name="ct">Токен отмены.</param>
        /// <returns>Подходящие места; пустой список, если таких нет.</returns>
        Task<IReadOnlyList<Place>> GetCandidatesAsync(TripCriteria criteria, double radiusKm, CancellationToken ct);
    }
}
