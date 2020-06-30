using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user")]
    public class BmsUserExtend : HuigeTec.Core.Domain.Entities.BmsUser
    {
        [NotMapped]
        public static string FrontWord { get; set; }

        [NotMapped]
        public static string EndWord { get; set; }


        /// <summary>
        /// 创建登录密码
        /// </summary>
        /// <returns></returns>
        public override string CreatePassword(string password)
        {
            MD5 md5Hasher = MD5.Create();
            FrontWord = "RGC";
            EndWord = "ManageUser";
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(FrontWord + password + EndWord));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));        // X：十六进制 2：每次都是两位数
            }

            return sBuilder.ToString().Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 默认的组织架构Id
        /// </summary>
        [Column("primary_organization_id")]
        public Int64 PrimaryOrganizationId { get; set; }

        [NotMapped]
        public Dictionary<Int64, BmsUserRole> UserRoleDict { get; set; }

        [NotMapped]
        public new virtual Dictionary<Int64, BmsUserMenuExtend> UserMenuExtendDict { get; set; }
        [NotMapped]
        public Dictionary<Int64, BmsUserOrganization> UserOrganizationDict { get; set; }
        [NotMapped]
        public Dictionary<Int64, BmsUserSystem> UserSystemDict { get; set; }
        [NotMapped]
        public Dictionary<Int64, BmsUserPlatform> UserPlatformDict { get; set; }
        public BmsUserExtend()
        {
            UserRoleDict = new Dictionary<long, BmsUserRole>();
            UserMenuExtendDict = new Dictionary<long, BmsUserMenuExtend>();
            UserOrganizationDict = new Dictionary<long, BmsUserOrganization>();
            UserSystemDict = new Dictionary<long, BmsUserSystem>();
            UserPlatformDict = new Dictionary<long, BmsUserPlatform>();
        }
    }
}
