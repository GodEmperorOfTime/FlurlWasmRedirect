using BlazorWasm.Client;
using Flurl.Http.Configuration;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(services => {
  return new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
});

builder.Services.AddScoped<IFlurlClient>(services => {
  var httpClient = services.GetRequiredService<HttpClient>();
  return new FlurlClient(httpClient);
});

builder.Services.AddScoped<IWeatherForecastClient, HttpWeatherForecastClient>();
builder.Services.AddScoped<IWeatherForecastClient, FlurlWeatherForecastClient>();
builder.Services.AddScoped<IWeatherForecastClient, FlurlOverHttpWeatherForecastClient>();

await builder.Build().RunAsync();
