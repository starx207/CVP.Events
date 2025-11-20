using System;
using CVP.Events.Contracts.Requests;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVP.Events.Api.Sdk.Internal;

internal sealed class AuthTokenProvider
{
    private readonly IAuthApi _authApi;
    private readonly IOptions<EventsApiOptions> _options;
    private readonly ILogger<AuthTokenProvider>? _logger;
    private readonly IDistributedCache? _cache;

    public AuthTokenProvider(IAuthApi authApi, IOptions<EventsApiOptions> options, IEnumerable<ILogger<AuthTokenProvider>> loggers, IEnumerable<IDistributedCache> caches)
    {
        _authApi = authApi;
        _options = options;
        _logger = loggers.FirstOrDefault();
        _cache = caches.FirstOrDefault();
    }

    public async Task<string> GetAuthTokenAsync(CancellationToken cancellationToken)
    {
        var tokenKey = "EventApiToken";
        if (_cache is not null)
        {
            if (await _cache.GetStringAsync(tokenKey, cancellationToken) is { } cachedToken)
            {
                _logger?.LogDebug("Using cached authentication token");
                return cachedToken;
            }
        }

        _logger?.LogDebug("Authenticating request");
        var response = await _authApi.AuthenticateAsync(new AuthRequest()
        {
            ClientId = _options.Value.ClientId ?? "",
            ClientSecret = _options.Value.ClientSecret ?? ""
        }, cancellationToken);

        if (_cache is not null)
        {
            await _cache.SetStringAsync(tokenKey, response.Access_Token, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(response.Expires_In)
            }, cancellationToken);
        }

        return response.Access_Token;
    }
}
