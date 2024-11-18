using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : BaseRequest
    {
        public long id { get; set; }
    }
}