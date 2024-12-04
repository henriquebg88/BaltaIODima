using Dima.API.Commom.API;
using Dima.API.Models;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Identity;

namespace Dima.API.Endpoints.Identity
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("/logout", HandleAsyhnc).RequireAuthorization();

        private static async Task<IResult> HandleAsyhnc(SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
                return Results.Ok();
        }
    }
}