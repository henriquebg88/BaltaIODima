namespace Dima.Core.Requests
{
    public abstract class PagedRequest
    {
        public int pageNumber { get; set; } = Configurations.DefaultPageNumber;
        public int pageSize { get; set; } = Configurations.DefaultPageSize;
    }
}