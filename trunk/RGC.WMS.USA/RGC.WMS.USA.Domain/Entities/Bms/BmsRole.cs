using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("bms_role")]
    public class BmsRole : FullEntity
    {

        /// <summary>
        /// 是否常量，常量不能修改
        /// </summary>
        [Column("is_static")]
        public bool IsStatic { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("display_name")]
        public string DisplayName { get; set; }
        [Column("desc")]
        public string Desc { get; set; }

        [NotMapped]
        public String CreationTimeString
        {
            get
            {
                if (CreationTime != null)
                {
                    return CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
