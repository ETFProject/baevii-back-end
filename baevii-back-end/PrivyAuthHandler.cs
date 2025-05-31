namespace baevii_back_end;

using Microsoft.Extensions.Options;
using baevii_back_end.Configuration;
using System.Net.Http.Headers;

public class PrivyAuthHandler : DelegatingHandler
{
    private readonly PrivyConfiguration _privyConfig;

    public PrivyAuthHandler(IOptions<PrivyConfiguration> privyConfigOptions)
    {
        _privyConfig = privyConfigOptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var byteArray = System.Text.Encoding.ASCII.GetBytes($"{_privyConfig.AppId}:{_privyConfig.Secret}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        request.Headers.Add("privy-app-id", _privyConfig.AppId);
        return await base.SendAsync(request, cancellationToken);
    }
}
