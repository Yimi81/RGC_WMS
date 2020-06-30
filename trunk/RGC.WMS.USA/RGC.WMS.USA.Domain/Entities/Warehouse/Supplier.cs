using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 供应商表
    /// </summary>
    [Table("supplier")]
    public class Supplier : FullEntity
    {
        #region 供应商名称
        /// <summary>
        /// 供应商简称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 供应商全称
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
