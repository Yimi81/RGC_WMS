namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class InsuranceOptions
    {
        public string Provider { get; set; }
        public bool InsureShipment { get; set; }
        public decimal InsuredValue { get; set; }
    }
}
