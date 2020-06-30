namespace RGC.WMS.USA.Domain.Entities.System.Do
{
    public class SystemCreateOrUpdateDo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DomainName { get; set; }
        public string IPAddress { get; set; }
        public bool isStatic { get; set; }
        public string Desc { get; set; }
    }
}
