using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Bms;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserDto
    {
        public BmsUserDto()
        {
            UserRoleList = new List<BmsUserRole>();
            OrgsList = new List<BmsOrganizationIdsDto>();
            UserPlatformList = new List<BmsUserPlatform>();
        }
        public long Id { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Sex { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string LoginName { get; set; }

        public long CurrentOrgId { get; set; }

        public BmsUserStatus Status { get; set; }

        public List<BmsUserRole> UserRoleList { get; set; }

        public List<BmsOrganizationIdsDto> OrgsList { get; set; }

        public List<BmsUserPlatform> UserPlatformList { get; set; }
    }
}
