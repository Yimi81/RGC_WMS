namespace RGC.WMS.USA.Domain.Entities.Order
{
    public enum SSOrderStatus
    {
        awaiting_payment,
        awaiting_shipment,
        pending_fulfillment,
        shipped,
        on_hold,
        cancelled
    }
}
