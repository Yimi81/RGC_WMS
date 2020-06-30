using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 产品配置细节表
    /// </summary>
    [Table("sku_configuration_detail")]
    public class SkuConfigurationDetail : FullEntity
    {

        public SkuConfigurationDetail()
        {
            SeqNo = 0;
        }
        /// <summary>
        /// 父级配置id
        /// </summary>
        [Column("sku_config_id")]
        public long SkuConfigId { get; set; }


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

    }

   
}


