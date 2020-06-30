using RGC.WMS.USA.Application.Dto;

namespace RGC.WMS.USA.Application.System.Dto
{
    public class SyncSearchFilterDto: SearchFilterDto
    {
        public bool IsLatest { get; set; }
    }
}
