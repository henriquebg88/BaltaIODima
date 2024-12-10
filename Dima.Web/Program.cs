using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); --> Substituido pelo HTTPCliente do pacote de extensão da Microsoft
builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configurations.HttpClientName, opt => {
    opt.BaseAddress = new Uri("http://localhost:5139");
});

await builder.Build().RunAsync();
