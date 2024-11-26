using Dima.API.Commom.API;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.API.Endpoints.Categories
{
    public class GetByIdTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapGet("{id}", HandleAsyhnc)
                    .WithName("Transactions: Get")
                    .WithSummary("Retorna uma Transação existente.")
                    .WithOrder(4)
                    .Produces<Response<Transaction?>>();
        
        private static async Task<IResult> HandleAsyhnc(ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id,
                UserId = "henrique"
            };

            var result = await handler.GetByIdAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
