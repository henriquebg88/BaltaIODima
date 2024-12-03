using System.Security.Claims;
using Dima.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.API.Commom.API
{
    public static class AppExtension
    {
        public static void ConfigureDevEnviroment(this WebApplication app){
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }

        public static void UseSecurity(this WebApplication app){
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapPost("/logout", async (
                    SignInManager<User> signInManager, // => Pode ser usado pra pegar o informaçõe do usuário no banco
                    UserManager<User> userManager,
                    RoleManager<IdentityRole<long>> roleManager
                ) => {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                })
                .RequireAuthorization();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapPost("/roles", (
                    ClaimsPrincipal user    // => Já pega informações do usuário logado | Informações do cookie
                ) => {
                    if(user.Identity is null || user.Identity.IsAuthenticated) return Results.Unauthorized();

                    var identity = (ClaimsIdentity)user.Identity;
                    var roles = identity
                        .FindAll(identity.RoleClaimType)
                        .Select(c => new {
                            c.Issuer,
                            c.OriginalIssuer,
                            c.Type,
                            c.Value,
                            c.ValueType
                        });

                    return TypedResults.Json(roles);
                })
                .RequireAuthorization();
        }
    }
}