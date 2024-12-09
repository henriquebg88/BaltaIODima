using Dima.Core.Models;
using Dima.Core.Requests.Account;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    //Serve para limitar que o frontend terá acesso
    // API <--> Handler <--> WEBAPP
    public interface IAccountHandler // Multiplas implementações. Servirá para o frontend e backend
    {
        Task<Response<string>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task Logout();
    }
}