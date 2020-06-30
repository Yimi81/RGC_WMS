namespace RGC.WMS.USA.Application.System.Dto
{
    public class PlatformCreateOrUpdateDto
    {
        public long Id { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public bool IsAPI { get; set; }
        public bool IsPython { get; set; }
    }
}
