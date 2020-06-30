namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserLoginAttemptDto
    {
        public long LoginId { get; set; }
        public string LoginName { get; set; }
        public long StaffId { get; set; }
        public string LoginIP { get; set; }
        public string ClientInfo { get; set; }
        public bool AttemptResult { get; set; }
    }
}
