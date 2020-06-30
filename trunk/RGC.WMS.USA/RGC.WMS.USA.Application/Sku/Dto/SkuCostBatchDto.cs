using System.Collections.Generic;
using RGC.WMS.USA.Domain.Entities.Sku;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    /// <summary>
    /// sku成本批次dto
    /// 
    /// </summary>
    public class SkuCostBatchDto
    {
        public SkuCostBatchDto()
        {
           
        }
        public long Id { get; set; }
        /// <summary>
        /// SkuCostId
        /// </summary>
        public long SkuCostId { get; set; }

        public SkuCost SkuCost { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        public long SkuId { get; set; }

        public SkuInfo Sku { get; set; }

        public long ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        public string BatchNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string CreatorUser { get; set; }

        public string ModifyTimeString { get; set; }

        public string CreationTimeString { get; set; }
    }

 
}
