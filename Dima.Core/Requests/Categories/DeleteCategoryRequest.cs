using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class DeleteCategoryRequest : BaseRequest
    {
        public long id { get; set; }
    }
}