namespace TourConstructor.Application.Models
{
    /// <summary>
    /// Причина, по которой не построен ни один маршрут.
    /// Фильтры до <see cref="NotEnoughTime"/> проверяются по порядку значений; причина — первый шаг,
    /// после которого не осталось мест. Последние три причины проверяются вместе при построении маршрута;
    /// причина — та, по которой не подошло больше всего мест, при равенстве — раньше по порядку значений.
    /// </summary>
    public enum NoRouteReason
    {
        /// <summary>В радиусе поездки нет мест.</summary>
        NoPlacesInRadius,

        /// <summary>Рядом только заведения питания, а еда в запросе выключена.</summary>
        OnlyDiningNearby,

        /// <summary>Ни одно место не подходит по возрастному цензу.</summary>
        AgeRestricted,

        /// <summary>Остались только гиды, и ни один не берёт группу такого размера.</summary>
        GroupTooLarge,

        /// <summary>Каждое место по отдельности дороже бюджета.</summary>
        OverBudget,

        /// <summary>Ни до одного места не успеть доехать, посетить его и вернуться к старту до времени возвращения.</summary>
        NotEnoughTime,

        /// <summary>До места успеть можно, но после любого места не остаётся времени на заведение питания.</summary>
        NotEnoughTimeWithDining,

        /// <summary>После любого места не остаётся бюджета на заведение питания.</summary>
        NotEnoughBudgetWithDining
    }
}
