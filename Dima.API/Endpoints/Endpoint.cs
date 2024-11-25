using Dima.API.Commom.API;
using Dima.API.Endpoints.Categories;

namespace Dima.API.Endpoints
{
    public static class Endpoint
    {
        //Extension Method (Método de Extensão)
        private const string V1 = "v1";
        private const string CATEGORIES = "categories";

        public static void MapEndpoints(this WebApplication app)
        {
            var endpointsV1 = app.MapGroup(V1);

            endpointsV1.MapGroup(CATEGORIES)
                       .WithTags(CATEGORIES)
                       //.RequireAuthorization()
                       .MapEndpoint<CreateCategoryEndpoint>()
                       .MapEndpoint<UpdateCategoryEndpoint>()
                       .MapEndpoint<DeleteCategoryEndpoint>()
                       .MapEndpoint<GetByIdCategoryEndpoint>()
                       .MapEndpoint<GetAllCategoryEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(endpoint);
            return endpoint;
        }
        
    }
}