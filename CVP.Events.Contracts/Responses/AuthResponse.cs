using System;

namespace CVP.Events.Contracts.Responses;

public class AuthResponse
{
    public string Access_Token { get; set; } = string.Empty;
    public int Expires_In { get; set; }
}
