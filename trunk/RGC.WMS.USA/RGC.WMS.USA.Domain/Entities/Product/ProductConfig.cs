using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Product
{
    [Table("product_config")]

    public class ProductConfig : FullEntity
    {

        public ProductConfig()
        {
            detailList = new Dictionary<long, ProductConfigDetail>();
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
        public Dictionary<long, ProductConfigDetail> detailList { get; set; }



    }
    public class ProductConfigTree
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
        public List<ProductConfigTree> child { get; set; }

        public ProductConfigTree()
        {
            child = new List<ProductConfigTree>();
        }

    }
}
