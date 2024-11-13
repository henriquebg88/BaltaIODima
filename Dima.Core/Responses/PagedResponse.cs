
using System.Text.Json.Serialization;

namespace Dima.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configurations.DefaultPageSize) : base(data)
        {
            base.data = data;
            this.totalCount = totalCount;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
        }

        public PagedResponse(TData? data, int statusCode, string? message = null) : base(data, statusCode, message)
        {
            
        }
        
        public int currentPage { get; set; }
        public int pageSize { get; set; } = Configurations.DefaultPageSize;
        public int totalPages => (int)Math.Ceiling((double)totalCount/pageSize);
        public int totalCount { get; set; }

    }
}