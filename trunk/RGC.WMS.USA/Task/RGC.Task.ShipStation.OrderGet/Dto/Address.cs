namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class Address
    {
        public Address()
        {
            //country = "CA";
            //postalCode = "90230";
        }
        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string street3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string residential { get; set; }
        public string addressVerified { get; set; }
    }
}
