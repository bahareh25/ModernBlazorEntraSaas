using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.WebUI;
using UrlShortener.WebUI.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
//builder.Services.AddScoped<IShortLinkService, ShortLinkService>();
// ----------------AUTH----------------
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd",
        options.ProviderOptions.Authentication);

    options.ProviderOptions.DefaultAccessTokenScopes.Add(
        builder.Configuration["DownstreamApi:Scopes"]!);
});

// ---------------- HTTP CLIENT ----------------
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7207/");
})
.AddHttpMessageHandler(sp =>
{
    return sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "https://localhost:7207/" },
            scopes: new[] { builder.Configuration["DownstreamApi:Scopes"]! }
        );
});
builder.Services.AddHttpClient("PublicApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7207/");
});
// ---------------- SERVICE ----------------
builder.Services.AddScoped<IShortLinkService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var apiClient = factory.CreateClient("ApiClient");
    var publicApiClient = factory.CreateClient("PublicApiClient");
    return new ShortLinkService(apiClient,
        publicApiClient);
});
await builder.Build().RunAsync();
