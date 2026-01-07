using System;                         // ✅ For Uri
using System.Net.Http;                // ✅ For HttpClient
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using SportsClubUI;
using SportsClubUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for calling .NET API
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5194/")
    });

// Custom API service
builder.Services.AddScoped<SportsApiService>();

await builder.Build().RunAsync();
