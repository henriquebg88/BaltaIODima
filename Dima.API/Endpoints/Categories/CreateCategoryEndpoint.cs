using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("", HandleAsyhnc)
                    .WithName("Categories: Create")
                    .WithSummary("Cria uma nova Categoria.")
                    .WithDescription("Cria uma nova Categoria.")
                    .WithOrder(1)
                    .Produces<Response<Category?>>();
        
        private static async Task<IResult> HandleAsyhnc(ICaterogyHandler handler, CreateCategoryRequest request)
        {
            request.UserId = "henrique";
            var result = await handler.CreateAsync(request);

            return result.isSuccess
                ? TypedResults.Created($"/{result.data?.id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
