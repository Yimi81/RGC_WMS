using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Domain.Do
{
    public class SearchFilterDo
    {
        public SearchFilterDo()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
        public string Sorting { get; set; }
        public string SearchKey { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public int Status { get; set; }
        /// <summary>
        ///  value: 1:未删除; 2:已删除
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
