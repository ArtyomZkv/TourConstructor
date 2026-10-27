using TourConstructor.Domain.Entities;
using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Application.Interfaces
{
    /// <summary>
    /// Хранилище мест
    /// </summary>
    public interface IPlaceRepository
    {
        /// <summary>
        /// Выбирает места, реализующие <see cref="Domain.Interfaces.IVisitable"/>, на расстоянии по прямой
        /// не больше <paramref name="radiusKm"/> от <paramref name="start"/>.
        /// </summary>
        /// <param name="start">Точка, от которой считается расстояние.</param>
        /// <param name="radiusKm">Максимальное расстояние по прямой от <paramref name="start"/>, км.</param>
        /// <param name="ct">Токен отмены.</param>
        /// <returns>Подходящие места; пустой список, если таких нет.</returns>
        Task<IReadOnlyList<Place>> GetCandidatesAsync(Coordinates start, double radiusKm, CancellationToken ct);
    }
}
