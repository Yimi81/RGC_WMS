namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsRoleCreateOrUpdateDto
    {
        public long Id { get; set; }
        public bool IsStatic { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Desc { get; set; }
    }
}
