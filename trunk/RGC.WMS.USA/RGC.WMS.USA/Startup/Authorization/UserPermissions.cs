using System.Collections.Generic;

namespace RGC.WMS.USA.Authorization
{
    public class UserPermissions
    {
        public UserPermissions()
        {
            PermissionCodes = new List<string>();
        }
       // public ObjectId _id { get; set; }
        public long UserId { get; set; }
        public string SystemCode { get; set; }
        public List<string> PermissionCodes { get; set; }
    }
}
