using System;

namespace RGC.WMS.USA.Application.Item.Dto
{
    public class ItemPriceSynchronizeDto
    {
        public long ItemId { get; set; }
        /// <summary>
        /// 当前零售价
        /// </summary>
        public int RetailPrice { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime ValidDate { get; set; }

        /// <summary>
        /// 预生效零售价
        /// </summary>
        public int PreRetailPrice { get; set; }
    }
}
