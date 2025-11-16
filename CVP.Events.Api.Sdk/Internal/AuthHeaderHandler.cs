using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace CVP.Events.Api.Sdk.Internal;

internal class AuthHeaderHandler : DelegatingHandler
{
    private readonly AuthTokenProvider _tokenProvider;
    private readonly ILogger<AuthHeaderHandler>? _logger;

    public AuthHeaderHandler(AuthTokenProvider tokenProvider, IEnumerable<ILogger<AuthHeaderHandler>> loggers)
    {
        _tokenProvider = tokenProvider;
        _logger = loggers.FirstOrDefault();
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => SendInternalAsync(request, cancellationToken);

    private async Task<HttpResponseMessage> SendInternalAsync(HttpRequestMessage request, CancellationToken cancellationToken, int attemptNumber = 1)
    {
        _logger?.LogDebug("Requesting auth token for request: ({Method}) {Uri}", request.Method, request.RequestUri?.PathAndQuery);
        var authToken = await _tokenProvider.GetAuthTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && attemptNumber < 3)
        {
            _logger?.LogInformation("Request failed with 401. Attempting to refresh token, and try again (attempt: {attemptNumber})", attemptNumber);
            await Task.Delay(250 * attemptNumber, cancellationToken);
            // Retry the request. We have have an expired token that needs refreshed
            return await SendInternalAsync(request, cancellationToken, attemptNumber++);
        }

        return response;
    }
}
