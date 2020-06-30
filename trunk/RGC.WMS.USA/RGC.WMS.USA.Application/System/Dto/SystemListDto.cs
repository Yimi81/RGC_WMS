namespace RGC.WMS.USA.Application.System.Dto
{
    public class SystemListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DomainName { get; set; }
        public string IPAddress { get; set; }
        public bool IsStatic { get; set; }
    }

    public class SystemSimpleListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
