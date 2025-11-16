using System;

namespace CVP.Events.Contracts.Requests;

public class AuthRequest
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
