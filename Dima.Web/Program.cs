using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using MudBlazor.Services;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Dima.Core.Handlers;
using Dima.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configurations.ApiURL = builder.Configuration.GetValue<string>("ApiUrl") ?? "";

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); --> Substituido pelo HTTPCliente do pacote de extensão da Microsoft

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>(); // Quando o app pedir o primeiro, enviar o segundo
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationState>()); // Forçar cast no caso de método que continua usando o primeiro

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configurations.HttpClientName, opt => {
    opt.BaseAddress = new Uri(Configurations.ApiURL);
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();

await builder.Build().RunAsync();
