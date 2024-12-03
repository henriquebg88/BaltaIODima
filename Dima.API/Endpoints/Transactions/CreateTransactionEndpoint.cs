using System.Security.Claims;
using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("", HandleAsyhnc)
                    .WithName("Transactions: Create")
                    .WithSummary("Cria uma nova Transação.")
                    .WithDescription("Cria uma nova Transação.")
                    .WithOrder(1)
                    .Produces<Response<Transaction?>>();
        
        private static async Task<IResult> HandleAsyhnc(ClaimsPrincipal user, ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = user.Identity?.Name ?? "";
            var result = await handler.CreateAsync(request);

            return result.isSuccess
                ? TypedResults.Created($"/{result.data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
