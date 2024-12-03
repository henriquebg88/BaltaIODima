using System.Security.Claims;
using Dima.API.Commom.API;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.API.Endpoints.Categories
{
    public class GetAllCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapGet("", HandleAsyhnc)
                    .WithName("Categories: GetAllByUser")
                    .WithSummary("Retorna uma lista de Categorias existentes de um usuário.")
                    .WithOrder(5)
                    .Produces<PagedResponse<List<Category>?>>();
        
        private static async Task<IResult> HandleAsyhnc(
            ClaimsPrincipal user,
            ICaterogyHandler handler, 
            [FromQuery]int pageNumber = Configurations.DefaultPageNumber, 
            [FromQuery]int pageSize = Configurations.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? "",
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
