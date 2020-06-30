using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// sku成本表
    /// 
    /// </summary>
    [Table("sku_cost")]
    public class SkuCost : FullEntity
    {
        public SkuCost()
        {
            SkuCostBatchDict = new Dictionary<long, SkuCostBatch>();
            SkuCostBatchList = new List<SkuCostBatch>();
        }

        /// <summary>
        /// SkuId
        /// </summary>
        [Column("sku_id")]
        public long SkuId { get; set; }


        [Column("product_id")]
        public long ProductId { get; set; }

        [NotMapped]
        public bool IsAddBatch { get; set; }

        [NotMapped]
        public SkuInfo Sku { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SkuCostBatch> SkuCostBatchDict { get; set; }

        [NotMapped]
        public List<SkuCostBatch> SkuCostBatchList { get; set; }

        #region 成本价格信息
        /// <summary>
        /// FOB
        /// </summary>
        [Column("fob")]
        public int FOB { get; set; }

        [NotMapped]
        public string FOBString { get; set; }

        /// <summary>
        /// DDP
        /// Delivered Duty Paid（named place of destination)
        /// 税后交货（……指定目的地）
        /// 工厂给的一个报价 含义是：货物从工厂到美国仓库的价格
        /// </summary>
        [Column("ddp")]
        public int DDP { get; set; }

        [NotMapped]
        public string DDPString { get; set; }

        /// <summary>
        /// 海运费
        /// </summary>
        [Column("sea_freight")]
        public int SeaFreight { get; set; }

        [NotMapped]
        public string SeaFreightString { get; set; }

        /// <summary>
        /// 卸柜费
        /// </summary>
        [Column("unloading_charge")]
        public int UnloadingCharge { get; set; }

        [NotMapped]
        public string UnloadingChargeString { get; set; }

        /// <summary>
        /// ELC
        /// 货物到达美国仓的成本
        /// 和工厂做DDP：ELC = DDP + 卸货费
        /// 和工厂做FOB：ELC = FOB + 海运费 + 关税 + 卸货费
        /// </summary>
        [Column("elc")]
        public int ELC { get; set; }

        [NotMapped]
        public string ELCString { get; set; }

        /// <summary>
        /// Z3运费
        /// 美国内陆运费
        /// </summary>
        [Column("z3_freight")]
        public int Z3Freight { get; set; }

        [NotMapped]
        public string Z3FreightString { get; set; }

        /// <summary>
        /// Z5运费
        /// 美国内陆运费
        /// </summary>
        [Column("z5_freight")]
        public int Z5Freight { get; set; }

        [NotMapped]
        public string Z5FreightString { get; set; }


        /// <summary>
        /// MSRP
        /// Manufacturer Suggested Retail Price
        /// 厂商建议零售价格
        /// </summary>
        [Column("msrp")]
        public int Msrp { get; set; }

        [NotMapped]
        public string MsrpString { get; set; }

        /// <summary>
        /// MAP 
        /// minimum advertised price
        /// 最低零售价
        /// </summary>
        [Column("map")]
        public int Map { get; set; }

        [NotMapped]
        public string MapString { get; set; }

        #endregion

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [NotMapped]
        public string CreatorUser { get; set; }

        [NotMapped]
        public string ModifyTimeString { get; set; }

        [NotMapped]
        public string CreationTimeString { get; set; }
    }
}
