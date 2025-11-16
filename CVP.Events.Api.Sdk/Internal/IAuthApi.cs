using System;
using CVP.Events.Contracts.Requests;
using CVP.Events.Contracts.Responses;
using Refit;

namespace CVP.Events.Api.Sdk.Internal;

internal interface IAuthApi
{
    [Post("/api/auth")]
    Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken = default);
}
