using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapDelete("{id}", HandleAsyhnc)
                    .WithName("Categories: Delete")
                    .WithSummary("Exclui uma nova Categoria existente.")
                    .WithOrder(3)
                    .Produces<Response<Category?>>();
        
        private static async Task<IResult> HandleAsyhnc(ICaterogyHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                id = id,
                UserId = "henrique"
            };

            var result = await handler.DeleteteAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
