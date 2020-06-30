namespace RGC.WMS.USA.Application.Purchase.Dto
{
    public class PackingListFilterInput
    {
        public PackingListFilterInput()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
        public string SearchType { get; set; }
        public string SearchKey { get; set; }
        public long WarehouseId { get; set; }
        /// <summary>
        /// 1:未删除; 2:已删除
        /// </summary>
        public int IsDeleted { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
