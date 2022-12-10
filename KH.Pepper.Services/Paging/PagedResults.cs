namespace KH.Pepper.Core.AppServices
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

        public int PageSize{ get; set; }

        public int RowCount { get; set; }

        public string SortBy{ get; set; }

        public bool SortDesc{ get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
