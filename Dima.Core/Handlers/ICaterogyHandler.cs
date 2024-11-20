using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    //Serve para limitar que o frontend terá acesso
    // API <--> Handler <--> WEBAPP
    public interface ICaterogyHandler // Multiplas implementações. Servirá para o frontend e backend
    {
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteteAsync(DeleteCategoryRequest request);
    }
}