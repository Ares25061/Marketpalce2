using BlazorClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ChatBotApi", client =>
{
    client.BaseAddress = new Uri("https://oxygenmarketapi.onrender.com"); 
});

builder.Services.AddScoped<User, User>();
builder.Services.AddScoped<UserInfo>();
builder.Services.AddScoped<TokenRefresher>();
await builder.Build().RunAsync();