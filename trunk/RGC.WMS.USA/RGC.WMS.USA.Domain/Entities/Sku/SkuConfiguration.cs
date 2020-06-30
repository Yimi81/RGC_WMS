using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 产品配置总表
    /// </summary>
    [Table("sku_configuration")]
    public class SkuConfiguration : FullEntity
    {

        public SkuConfiguration()
        {
            detailList=new Dictionary<long, SkuConfigurationDetail>();
            SeqNo = 0;
        }
        /// <summary>
        /// 父级配置id
        /// </summary>
        [Column("package_id")]
        public long PackageId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Column("type")]
        public ConfigurationType Type { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("c_name")]
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("e_name")]
        public string EName { get; set; }

        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }

        [NotMapped]
        public Dictionary<long,SkuConfigurationDetail> detailList { get; set; }



    }
    public class SkuConfigTree
    {
        public long Id { get; set; }

        /// <summary>
        /// 父级配置id
        /// </summary>
        public long PackageId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ConfigurationType Type { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        public virtual int SeqNo { get; set; }
        public List<SkuConfigTree> Child { get; set; }

        public SkuConfigTree()
        {
            Child = new List<SkuConfigTree>();
        }

    }

}


