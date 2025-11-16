using System;

namespace CVP.Events.Api.Sdk;

public sealed class EventsApiOptions
{
    public string? BaseUrl { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}
