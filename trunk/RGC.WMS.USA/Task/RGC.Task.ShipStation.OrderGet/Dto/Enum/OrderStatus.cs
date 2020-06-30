namespace RGC.Task.ShipStation.OrderGet.Dto.Enum
{
    /// <summary>
    /// jerry 2020/06/02
    /// </summary>
    public enum OrderStatus
    {
        awaiting_payment,
        awaiting_shipment,
        pending_fulfillment,
        shipped,
        on_hold,
        cancelled
    }
}
