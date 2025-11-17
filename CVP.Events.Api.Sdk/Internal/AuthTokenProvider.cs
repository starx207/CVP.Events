using System;
using CVP.Events.Contracts.Requests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVP.Events.Api.Sdk.Internal;

internal sealed class AuthTokenProvider
{
    private readonly IAuthApi _authApi;
    private readonly IOptions<EventsApiOptions> _options;
    private readonly ILogger<AuthTokenProvider>? _logger;
    private string? _cachedToken; // TODO: I should implement a real cache and then this service wouldn't need to be a singleton
    private DateTimeOffset? _tokenExpiration;

    public AuthTokenProvider(IAuthApi authApi, IOptions<EventsApiOptions> options, IEnumerable<ILogger<AuthTokenProvider>> loggers)
    {
        _authApi = authApi;
        _options = options;
        _logger = loggers.FirstOrDefault();
    }

    public async Task<string> GetAuthTokenAsync(CancellationToken cancellationToken)
    {
        if (_tokenExpiration is not null && DateTimeOffset.Now >= _tokenExpiration.Value)
        {
            _cachedToken = null;
            _tokenExpiration = null;
        }

        if (_cachedToken is null)
        {
            _logger?.LogDebug("Authenticating request");
            var response = await _authApi.AuthenticateAsync(new AuthRequest()
            {
                ClientId = _options.Value.ClientId ?? "",
                ClientSecret = _options.Value.ClientSecret ?? ""
            }, cancellationToken);

            _cachedToken = response.Access_Token;
            _tokenExpiration = DateTimeOffset.Now + TimeSpan.FromSeconds(response.Expires_In);
        }
        else
        {
            _logger?.LogDebug("Using cached authentication token");
        }

        return _cachedToken;
    }
}
