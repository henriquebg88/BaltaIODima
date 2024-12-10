using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient(Configurations.HttpClientName);
        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso.", (int)HttpStatusCode.OK, "Login realizado com sucesso.")
                : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o Login");
        }

        public async Task Logout()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);
            await httpClient.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await httpClient.PostAsJsonAsync("v1/identity/register", request);

            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso.", (int)HttpStatusCode.Created, "Cadastro realizado com sucesso.")
                : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o Cadastro");
        }
    }
}