namespace api.Helpers
{
    public class UserParams
    {
        public string SearchKey { get; set; } = "";
        public string SearchStatus { get; set; } = "";
        private const int MaxPageSize = 10000;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

    }
}