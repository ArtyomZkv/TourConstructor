namespace TourConstructor.Application.Models
{
    /// <summary>
    /// Результат планирования поездки.
    /// </summary>
    /// <param name="Variants">Варианты маршрута; пустой список, если не построен ни один.</param>
    /// <param name="Reason">Почему маршрутов нет; null, если они есть.</param>
    /// <param name="PlacesBeforeFailure">
    /// Сколько мест дошло до шага, на котором закончились; null, если маршруты есть.
    /// </param>
    public record TripPlan(IReadOnlyList<TripRouteVariant> Variants, NoRouteReason? Reason, int? PlacesBeforeFailure);
}
