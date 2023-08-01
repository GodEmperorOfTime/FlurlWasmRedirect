using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace BlazorWasm.Client;

public class AluPerBaseUrlFlurlClientFactory : PerBaseUrlFlurlClientFactory
{
  protected override IFlurlClient Create(Url url)
  {
    return base
    .Create(url)
    //.AllowHttpStatus(System.Net.HttpStatusCode.Found)
    .WithAutoRedirect(true)          
    ;
  }
}