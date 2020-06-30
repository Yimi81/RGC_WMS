namespace RGC.WMS.USA.Application.Dto
{
    public class SearchFilterDto
    {
        public SearchFilterDto()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
        public string Sorting { get; set; }
        public string SearchKey { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int Status { get; set; }
        public int IsDeleted { get; set; }
    }
}
