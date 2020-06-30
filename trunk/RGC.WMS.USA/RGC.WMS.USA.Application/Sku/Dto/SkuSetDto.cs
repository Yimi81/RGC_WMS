using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuSetDto
    {
        public long id { get; set; }

        public long parentSkuId { get; set; }

        /// <summary>
        /// 组成skuId
        /// </summary>
        public long childSkuId { get; set; }

        /// <summary>
        /// 组成sku数量
        /// </summary>
        public int childSkuQty { get; set; }

        /// <summary>
        /// 主图
        /// </summary>
        public string primaryImageSrc { get; set; }
        public string primaryImageSrcFull { get; set; }

        /// <summary>
        /// 产品名
        /// </summary>
        public string displayName { get; set; }

    }



}
