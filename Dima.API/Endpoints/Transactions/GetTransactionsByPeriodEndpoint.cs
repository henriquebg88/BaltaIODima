using System.Security.Claims;
using Dima.API.Commom.API;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.API.Endpoints.Categories
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapGet("", HandleAsyhnc)
                    .WithName("Categories: GetByPeriodUser")
                    .WithSummary("Retorna uma lista de Transações existentes de um usuário, por um período. Caso período não seja especificado, trás do mês atual.")
                    .WithOrder(5)
                    .Produces<PagedResponse<List<Transaction>?>>();
        
        private static async Task<IResult> HandleAsyhnc(
            ClaimsPrincipal user,
            ITransactionHandler handler, 
            [FromQuery]DateTime? startDate = null,
            [FromQuery]DateTime? endDate = null,            
            [FromQuery]int pageNumber = Configurations.DefaultPageNumber, 
            [FromQuery]int pageSize = Configurations.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = user.Identity?.Name ?? "",
                pageNumber = pageNumber,
                pageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetListByPeriodAsync(request);

            return result.isSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
