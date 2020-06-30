namespace RGC.WMS.USA.Domain.Do
{
    public class PageDo
    {
        /// <summary>
        /// 总计记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 每页最大记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页记录数
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
