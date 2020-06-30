using HuigeTec.Core.Domain.Entities;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserCreateOrUpdateDto
    {
        public BmsUserCreateOrUpdateDto()
        {
            SystemMenuIds = new List<long>();
        }

        public long Id { get; set; }
        public string LoginName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Wechat { get; set; }
        public string Password { get; set; }
        public BmsUserStatus Status { get; set; }

        public List<long> SystemMenuIds { get; set; }
    }
}
