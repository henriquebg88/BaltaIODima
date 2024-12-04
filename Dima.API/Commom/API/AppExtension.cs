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
        }
        
    }
}