using System.Security.Claims;
using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapDelete("{id}", HandleAsyhnc)
                    .WithName("Transactions: Delete")
                    .WithSummary("Exclui uma Transação existente.")
                    .WithOrder(3)
                    .Produces<Response<Transaction?>>();
        
        private static async Task<IResult> HandleAsyhnc(ClaimsPrincipal user, ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? ""
            };

            var result = await handler.DeleteteAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
