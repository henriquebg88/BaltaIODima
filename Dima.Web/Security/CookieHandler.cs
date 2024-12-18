
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Dima.Web.Security
{
    public class CookieHandler : DelegatingHandler
    {
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.Headers.Add("X-Requested-With", ["XMLHttprequest"]);
            return base.Send(request, cancellationToken);
        }
    }
}