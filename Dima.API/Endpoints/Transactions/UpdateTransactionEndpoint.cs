using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPut("{id}", HandleAsyhnc)
                    .WithName("Transactions: Update")
                    .WithSummary("Atualiza uma Transação existente.")
                    .WithDescription("Atualiza uma Transação existente.")
                    .WithOrder(2)
                    .Produces<Response<Transaction?>>();
        
        private static async Task<IResult> HandleAsyhnc(ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = "henrique";
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
