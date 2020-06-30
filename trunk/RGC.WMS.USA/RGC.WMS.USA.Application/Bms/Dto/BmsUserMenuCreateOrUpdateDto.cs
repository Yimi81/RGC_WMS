using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserMenuCreateOrUpdateDto
    {
        public BmsUserMenuCreateOrUpdateDto()
        {
            Menus = new List<BmsMenuSimpleDto>();
        }

        public long UserId { get; set; }

        public List<BmsMenuSimpleDto> Menus { get; set; }
    }
}
