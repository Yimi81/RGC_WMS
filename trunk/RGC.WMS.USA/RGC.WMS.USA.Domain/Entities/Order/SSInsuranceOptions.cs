namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 保险
    /// jerry 2020/6/4
    /// </summary>
    public class SSInsuranceOptions
    {
        public string Provider { get; set; }
        public bool InsureShipment { get; set; }
        public decimal InsuredValue { get; set; }
    }
}
