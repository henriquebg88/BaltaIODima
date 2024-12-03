using Dima.API.Commom.API;
using Dima.API.Endpoints.Categories;

namespace Dima.API.Endpoints
{
    public static class Endpoint
    {
        //Extension Method (Método de Extensão)
        private const string V1 = "v1";
        private const string CATEGORIES = "categories";
        private const string TRANSACTIONS = "transactions";

        public static void MapEndpoints(this WebApplication app)
        {
            var endpointsV1 = app.MapGroup(V1);
            
            app.MapGroup("")
                .WithTags("Health check")
                .MapGet("/", () => new {message = "Estou vivo."});

            endpointsV1.MapGroup(CATEGORIES)
                       .WithTags(CATEGORIES)
                       .RequireAuthorization()
                       .MapEndpoint<CreateCategoryEndpoint>()
                       .MapEndpoint<UpdateCategoryEndpoint>()
                       .MapEndpoint<DeleteCategoryEndpoint>()
                       .MapEndpoint<GetByIdCategoryEndpoint>()
                       .MapEndpoint<GetAllCategoryEndpoint>();

            endpointsV1.MapGroup(TRANSACTIONS)
                       .WithTags(TRANSACTIONS)
                       .RequireAuthorization()
                       .MapEndpoint<CreateTransactionEndpoint>()
                       .MapEndpoint<UpdateTransactionEndpoint>()
                       .MapEndpoint<DeleteTransactionEndpoint>()
                       .MapEndpoint<GetByIdTransactionEndpoint>()
                       .MapEndpoint<GetTransactionsByPeriodEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(endpoint);
            return endpoint;
        }
        
    }
}