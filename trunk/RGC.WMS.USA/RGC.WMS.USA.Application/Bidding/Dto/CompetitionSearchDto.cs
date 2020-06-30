using RGC.WMS.USA.Application.Dto;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bidding.Dto
{
    public class CompetitionSearchDto : SearchFilterDto
    {
        public CompetitionSearchDto()
        {
            ItemId = 0;
            ProductId = 0;
            PlatformIds = new List<long>();
            PlatformId = 0;
            Status = -1;
        }
        public List<long> PlatformIds { get; set; }
        public long PlatformId { get; set; }


        public long ItemId { get; set; }
        public long ProductId { get; set; }

        public int Status { get; set; }


    }
}
