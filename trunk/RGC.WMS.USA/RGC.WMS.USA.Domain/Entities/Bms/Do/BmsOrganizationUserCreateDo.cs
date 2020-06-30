using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsOrganizationUserCreateDo
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
