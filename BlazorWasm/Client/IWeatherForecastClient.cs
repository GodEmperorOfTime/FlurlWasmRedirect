using BlazorWasm.Shared;
using Flurl.Http;
using System.Net.Http.Json;

namespace BlazorWasm.Client;

public interface IWeatherForecastClient
{

  string Name { get; }

  Task<WeatherForecast[]> GetForecastsAsync();

}

class HttpWeatherForecastClient : IWeatherForecastClient
{

  public string Name => "HttpClient";

  readonly HttpClient httpClient;

  public HttpWeatherForecastClient(HttpClient httpClient)
  {
    this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
  }

  public async Task<WeatherForecast[]> GetForecastsAsync()
  {
    return await httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast")
      ?? Array.Empty<WeatherForecast>();
  }
}

class FlurlWeatherForecastClient : IWeatherForecastClient
{

  readonly IFlurlClient flurlClient;

  public string Name => "FlurlClient";

  public FlurlWeatherForecastClient(IFlurlClient flurlClient)
  {
    this.flurlClient = flurlClient ?? throw new ArgumentNullException(nameof(flurlClient));
  }

  public async Task<WeatherForecast[]> GetForecastsAsync()
  {
    var response = await flurlClient.Request("WeatherForecast").GetAsync();
    Console.WriteLine($"{response.StatusCode}: {response.ResponseMessage}");
    return await response.GetJsonAsync<WeatherForecast[]>()
      ?? Array.Empty<WeatherForecast>();
  }

}
