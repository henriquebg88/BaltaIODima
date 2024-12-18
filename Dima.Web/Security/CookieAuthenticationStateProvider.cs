using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security
{
    public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {
        private bool _isAuthenticated = false;
        private readonly HttpClient _httpClient = clientFactory.CreateClient();

        public async Task<bool> CheckAuthenticateAsync()
        {
            GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;

            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var userInfo = await GetUserAsync();

            if (userInfo == null) return new AuthenticationState(user);

            var claims = await GetClaimsAsync(userInfo);

            var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
            user = new ClaimsPrincipal(id);

            _isAuthenticated = true;

            return new AuthenticationState(user);
        }

        public void NotifyAuthenticateStateChanged() => base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        public async Task<User?> GetUserAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User?>( "v1/identity/manage/info" );
            }
            catch (System.Exception)
            {
                return null;                
            }
        }

        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(
                user.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email).Select(c => new Claim(c.Key, c.Value))
            );

            RoleClaim[]? roles;

            try
            {
                roles = await _httpClient.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            }
            catch (System.Exception)
            {
                return claims;
            }

            foreach (var role in roles ?? [])
            {
                if(!string.IsNullOrWhiteSpace(role.Type) && !string.IsNullOrWhiteSpace(role.Value))
                    claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
            }

            return claims;
        }
    }
}