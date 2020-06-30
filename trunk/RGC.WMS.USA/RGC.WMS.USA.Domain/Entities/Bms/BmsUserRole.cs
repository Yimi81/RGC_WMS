using System;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user_role")]
    public class BmsUserRole : CreationEntity
    {
        public BmsUserRole()
        {
            IsStatic = false;
        }
        [Column("user_id")]
        public Int64 UserId { get; set; }
        [Column("role_id")]
        public Int64 RoleId { get; set; }

        /// <summary>
        /// 是否常量，常量不能修改
        /// </summary>
        [Column("is_static")]
        public bool IsStatic { get; set; }
    }
}