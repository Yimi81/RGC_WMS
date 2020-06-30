using System.Collections.Generic;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsUserCreateOrUpdateDo
    {
        public BmsUserCreateOrUpdateDo()
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
        public long StaffId { get; set; }

        public string CName { get; set; }

        public string Password { get; set; }

        public BmsUserStatus Status { get; set; }

        public List<long> SystemMenuIds { get; set; }
    }
}
