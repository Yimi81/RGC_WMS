namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsRoleListDto
    {
        public long Id { get; set; }
        public bool IsStatic { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Desc { get; set; }
        public string creationTime { get; set; }
    }

    public class BmsRoleSimpleListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

    }
}
