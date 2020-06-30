using HuigeTec.Core.Helpers;
using System;

namespace RGC.WMS.USA.Application.Item.Dto
{
    public class ItemPriceRecordDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }

        /// <summary>
        /// 默认0已通过审批，1审批未通过，2审批拒绝（这个逻辑暂时不加）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 中文品名
        /// </summary>
        public string ItemName { get; set; }

        public string Src { get; set; }

        public string SrcFull { get; set; }

        /// <summary>
        /// 工厂型号
        /// </summary>
        public string FactoryModel { get; set; }

        public long ProductId { get; set; }
        public long PlatformId { get; set; }

        public string PlatformName { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public int WholeSalePrice { get; set; }

        public string WholeSalePriceString => ParseHelper.Fen2YuanString(WholeSalePrice);

        /// <summary>
        /// 零售价
        /// </summary>
        public int RetailPrice { get; set; }

        public string RetailPriceString => ParseHelper.Fen2YuanString(RetailPrice);

        public string ValidTimeString
        {
            get
            {
                return ValidTime.ToString();
            }
        }
        public DateTime ValidTime { get; set; }

        public string CreationTimeString
        {
            get
            {
                return CreationTime.ToString();
            }
        }
        public DateTime CreationTime { get; set; }

        public string CreationUserName { get; set; }
        public long CreatorUserId { get; set; }

        /// <summary>
        /// 下发级别 0普通 1紧急
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否同步
        /// </summary>
        public bool IsSync { get; set; }

        /// <summary>
        /// 请求同步时间
        /// </summary>
        public DateTime RequestSyncTime { get; set; }
        public string RequestSyncTimeString
        {
            get
            {
                return RequestSyncTime.ToString();
            }
        }
    }
}
