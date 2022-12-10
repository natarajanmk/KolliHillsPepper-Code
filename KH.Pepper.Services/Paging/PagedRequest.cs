namespace KH.Pepper.Core.AppServices
{
    public class PagedRequest
    {
        public int Page { get; set; }        
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
    }
}
