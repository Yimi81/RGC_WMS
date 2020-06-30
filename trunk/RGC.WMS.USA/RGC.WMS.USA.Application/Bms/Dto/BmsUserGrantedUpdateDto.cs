using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserGrantedUpdateDto
    {
        public BmsUserGrantedUpdateDto()
        {
            checkIds = new List<long>();
        }
        public List<long> checkIds { get; set; }

        public long UserId { get; set; }
    }
}
