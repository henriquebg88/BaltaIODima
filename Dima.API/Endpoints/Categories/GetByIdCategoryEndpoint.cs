using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class GetByIdCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapGet("{id}", HandleAsyhnc)
                    .WithName("Categories: Get")
                    .WithSummary("Retorna uma Categoria existente.")
                    .WithOrder(4)
                    .Produces<Response<Category?>>();
        
        private static async Task<IResult> HandleAsyhnc(ICaterogyHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                id = id,
                UserId = "henrique"
            };

            var result = await handler.GetByIdAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
