using TourConstructor.Domain.ValueObjects;

namespace TourConstructor.Application.Interfaces
{   
    /// <summary>
    /// Расчёт времени и расстояния между точками
    /// </summary>
    public interface ITravelTimeProvider
    {
        /// <summary>
        /// Рассчитывает время, за которое нужно добраться из i в j. i - откуда, j - куда.
        /// </summary>
        /// <returns>Возвращает матрицу времени NxN, где N - points.Count, [i,j] - минуты из points[i] до points[j].</returns>
        Task<int[,]> GetMinutesMatrixAsync(IReadOnlyList<Coordinates> points);

        /// <summary>
        /// Метод вызывается для расчёта максимально возможного расстояния по прямой по верхней границе скорости.
        /// </summary>
        /// <returns>Возвращает расстояние в км</returns>
        double GetMaxDistanceKm(TimeSpan travelTime);
    }
}
