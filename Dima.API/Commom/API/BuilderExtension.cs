using Dima.API.Data;
using Dima.API.Handlers;
using Dima.API.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Commom.API
{
    public static class BuilderExtension
    {
        public static void AddDimaConfiguration(this WebApplicationBuilder builder){
            Configurations.connectionString = builder.Configuration.GetConnectionString("Default") ?? "";
            Configurations.FrontendURL = builder.Configuration.GetConnectionString("FrontendURL") ?? "";
            Configurations.BackendURL = builder.Configuration.GetConnectionString("BackendURL") ?? "";
        }


        public static void AddSwaggerDocumentation(this WebApplicationBuilder builder){
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( SW => {
                //Para evitar que o Swagger considere iguais e agrupe as classes com mesmo nome, mas de namespaces difentes.
                SW.CustomSchemaIds( n => n.FullName );
            });
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            //Deve estar depois do Swagger. Autenticação antes.
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)  //Identifica o usuario - Formas de autenticação: JWT, Bearer e o utilizado neste curso
                .AddIdentityCookies(); //Serão criados, pelo servidor, cookies para trafegar no cabeçalho da requisição
            builder.Services.AddAuthorization();  //Limita o usuario
        }

        public static void AddDataContexts(this WebApplicationBuilder builder){
            builder.Services.AddDbContext<AppDbContext>( x => {
                            x.UseSqlServer(Configurations.connectionString);
            });

            builder.Services.AddIdentityCore<User>() //Core -> sem criar páginas
                            .AddRoles<IdentityRole<long>>()
                            .AddEntityFrameworkStores<AppDbContext>() //Usar o EF para fazer o armazenamento
                            .AddApiEndpoints(); 
        }

        public static void AddDimaServices(this WebApplicationBuilder builder){
            #region Explicação AddTransient/Singleton/Scoped
                /**
                AddTransient<Handler>():
                    Isso registra o tipo Handler no contêiner de injeção de dependência com o tempo de vida transitório. 
                    Ou seja, toda vez que o ASP.NET Core precisar de uma instância do tipo Handler, ele criará uma nova instância dessa classe.
                    A instância de Handler será criada e destruída a cada vez que for solicitada. 
                    Por exemplo, se o Handler for injetado em uma requisição HTTP, uma nova instância será criada para cada requisição.
                    -Fluxo de vida:
                        Se você pedir o serviço duas vezes (em momentos diferentes ou no mesmo contexto), o ASP.NET Core criará duas instâncias distintas do serviço.

                AddSingleton<Handler>(): 
                    A instância do serviço é criada uma única vez e compartilhada em toda a vida da aplicação.

                AddScoped<Handler>(): 
                    A instância do serviço é criada uma vez por requisição (escopo de requisição). 
                    Ou seja, a mesma instância é reutilizada durante o processamento de uma única requisição HTTP, mas uma nova instância será criada para cada nova requisição.
                    -Fluxo de vida:
                        Se você pedir o serviço dentro do mesmo contexto de requisição (por exemplo, dentro do mesmo controlador ou serviço), o ASP.NET Core vai retornar a mesma instância do serviço.
                        Se a requisição acabar (por exemplo, a resposta HTTP for enviada ao cliente), a instância do serviço será descartada.
                **/
            #endregion

            //Injeção de Dependência:
            //Registrar um tipo de serviço (no caso, o handler) no container de dependências (para injeção de dependência), 
            //com tempo de vida transitório (Ou seja, toda vez que o ASP.NET Core precisar de uma instância do tipo Handler, ele criará uma nova instância dessa classe.)
            builder.Services.AddTransient<ICaterogyHandler, CategoryHandler>(); // O Aspnet consegue inferir que o Request vem do Body, mas não sabe de onde vem o Handler
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>(); // O Aspnet consegue inferir que o Request vem do Body, mas não sabe de onde vem o Handler
        }

        public static void AddDimaCors(this WebApplicationBuilder builder){
            builder.Services.AddCors(options => {
                options.AddPolicy(
                    APIConfiguration.CorsPolicyName,
                    policy => policy
                        .WithOrigins([
                            Configurations.BackendURL,
                            Configurations.FrontendURL
                        ])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                )
            });
        }
    }
}