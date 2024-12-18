using System.Security.Claims;
using Dima.API.Commom.API;
using Dima.Core.Models.Account;

namespace Dima.API.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapGet("/roles", Handle).RequireAuthorization();

        private static Task<IResult> Handle(ClaimsPrincipal user)
        {
            if(user.Identity is null || user.Identity.IsAuthenticated) return Task.FromResult<IResult>(Results.Unauthorized());

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity
                .FindAll(identity.RoleClaimType)
                .Select(c => new RoleClaim {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                });

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
    }
}
