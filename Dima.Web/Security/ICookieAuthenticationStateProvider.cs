using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security
{
    public interface ICookieAuthenticationStateProvider
    {
        Task<bool> CheckAuthenticateAsync();
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void NotifyAuthenticateStateChanged();
    }
}