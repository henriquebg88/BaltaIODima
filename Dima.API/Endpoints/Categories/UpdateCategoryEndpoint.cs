using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPut("{id}", HandleAsyhnc)
                    .WithName("Categories: Update")
                    .WithSummary("Atualiza uma Categoria existente.")
                    .WithDescription("Atualiza uma Categoria existente.")
                    .WithOrder(2)
                    .Produces<Response<Category?>>();
        
        private static async Task<IResult> HandleAsyhnc(ICaterogyHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = "henrique";
            request.id = id;

            var result = await handler.UpdateAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
