using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    /// <summary>
    /// sku成本dto
    /// 
    /// </summary>
    public class SkuCostOutput
    {
        public SkuCostOutput()
        {

        }
        public long Id { get; set; }
        /// <summary>
        /// SkuId
        /// </summary>
        public long SkuId { get; set; }

        public long ProductId { get; set; }


      
        #region 成本价格信息

        /// <summary>
        /// FOB
        /// </summary>
        public int FOB { get; set; }

        public string FOBString { get; set; }

        /// <summary>
        /// DDP
        /// Delivered Duty Paid（named place of destination)
        /// 税后交货（……指定目的地）
        /// 工厂给的一个报价 含义是：货物从工厂到美国仓库的价格
        /// </summary>
        public int DDP { get; set; }

        public string DDPString { get; set; }

        /// <summary>
        /// 海运费
        /// </summary>
        public int SeaFreight { get; set; }

        public string SeaFreightString { get; set; }

        /// <summary>
        /// 卸柜费
        /// </summary>
        public int UnloadingCharge { get; set; }

        public string UnloadingChargeString { get; set; }

        /// <summary>
        /// ELC
        /// 货物到达美国仓的成本
        /// 和工厂做DDP：ELC = DDP + 卸货费
        /// 和工厂做FOB：ELC = FOB + 海运费 + 关税 + 卸货费
        /// </summary>
        public int ELC { get; set; }

        public string ELCString { get; set; }

        /// <summary>
        /// Z3运费
        /// 美国内陆运费
        /// </summary>
        public int Z3Freight { get; set; }

        public string Z3FreightString { get; set; }

        /// <summary>
        /// Z5运费
        /// 美国内陆运费
        /// </summary>
        public int Z5Freight { get; set; }

        public string Z5FreightString { get; set; }

        /// <summary>
        /// MSRP
        /// Manufacturer Suggested Retail Price
        /// 厂商建议零售价格
        /// </summary>
        public int Msrp { get; set; }

        public string MsrpString { get; set; }

        /// <summary>
        /// MAP 
        /// minimum advertised price
        /// 最低零售价
        /// </summary>
        public int Map { get; set; }

        public string MapString { get; set; }

        #endregion


        public string Remark { get; set; }

        public string CreatorUser { get; set; }

        public string ModifyTimeString { get; set; }

        public string CreationTimeString { get; set; }
    }
}
