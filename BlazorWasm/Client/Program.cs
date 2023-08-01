using BlazorWasm.Client;
using Flurl.Http.Configuration;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IFlurlClientFactory, AluPerBaseUrlFlurlClientFactory>();
builder.Services.AddScoped<IFlurlClient>(s => {
  var flurlFac = s.GetRequiredService<IFlurlClientFactory>();
  return flurlFac.Get(new Uri(builder.HostEnvironment.BaseAddress));
});
builder.Services.AddScoped(sp =>
{
  return new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
});
builder.Services.AddScoped<IWeatherForecastClient, HttpWeatherForecastClient>();
builder.Services.AddScoped<IWeatherForecastClient, FlurlWeatherForecastClient>();

await builder.Build().RunAsync();
