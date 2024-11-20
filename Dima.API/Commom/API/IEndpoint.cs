using Microsoft.AspNetCore.Routing;

namespace Dima.API.Commom.API
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}