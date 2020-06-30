using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserListDto
    {
        public long Id { get; set; }

        public string LoginName { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public BmsUserStatus Status { get; set; }

        public string LastLoginTime { get; set; }

    }

    public class BmsUsersimpleListDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }
    }
}
