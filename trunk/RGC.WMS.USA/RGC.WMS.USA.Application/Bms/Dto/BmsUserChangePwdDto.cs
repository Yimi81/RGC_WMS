namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserChangePwdDto
    {
        public long UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
