using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsOrganizationUserCreateDto
    {
        public long OrganizationId { get; set; }

        public List<long> UserIds { get; set; }
    }

    public class OrganizationUserSeqNoDto
    {
        public int Id { get; set; }
        public int SeqNo { get; set; }
    }
}
