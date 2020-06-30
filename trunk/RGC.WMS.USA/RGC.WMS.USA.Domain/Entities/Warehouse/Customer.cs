using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 客户表
    /// </summary>
    [Table("customer")]
    public class Customer : FullEntity
    {
        #region 客户名称
        /// <summary>
        /// 客户简称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 客户全称
        /// </summary>
        [Column("full_name")]
        public string FullName { get; set; }
        #endregion

        /// <summary>
        /// 供应商备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
    }
}
