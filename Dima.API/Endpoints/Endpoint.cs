using Dima.API.Commom.API;
using Dima.API.Endpoints.Categories;
using Dima.API.Endpoints.Identity;
using Dima.API.Models;

namespace Dima.API.Endpoints
{
    public static class Endpoint
    {
        //Extension Method (Método de Extensão)
        private const string V1 = "v1";
        private const string CATEGORIES = "categories";
        private const string TRANSACTIONS = "transactions";
        private const string IDENTITY = "identity";

        public static void MapEndpoints(this WebApplication app)
        {
            var endpointsV1 = app.MapGroup(V1);
            
            app.MapGroup("")
                .WithTags("Health check")
                .MapGet("/", () => new {message = "Estou vivo."});

            endpointsV1.MapGroup(V1 + CATEGORIES)
                       .WithTags(CATEGORIES)
                       .RequireAuthorization()
                       .MapEndpoint<CreateCategoryEndpoint>()
                       .MapEndpoint<UpdateCategoryEndpoint>()
                       .MapEndpoint<DeleteCategoryEndpoint>()
                       .MapEndpoint<GetByIdCategoryEndpoint>()
                       .MapEndpoint<GetAllCategoryEndpoint>();

            endpointsV1.MapGroup(V1 + TRANSACTIONS)
                       .WithTags(TRANSACTIONS)
                       .RequireAuthorization()
                       .MapEndpoint<CreateTransactionEndpoint>()
                       .MapEndpoint<UpdateTransactionEndpoint>()
                       .MapEndpoint<DeleteTransactionEndpoint>()
                       .MapEndpoint<GetByIdTransactionEndpoint>()
                       .MapEndpoint<GetTransactionsByPeriodEndpoint>();

            endpointsV1.MapGroup(V1 + IDENTITY)
                       .WithTags(IDENTITY)
                       .MapIdentityApi<User>()
                       .RequireAuthorization();

            endpointsV1.MapGroup(V1 + IDENTITY)
                       .WithTags(IDENTITY)
                       .MapEndpoint<LogoutEndpoint>()
                       .MapEndpoint<GetRolesEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(endpoint);
            return endpoint;
        }
        
    }
}