using System;

namespace CVP.Events.Api.Sdk.Requests;

public class CreateEventRequest
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}
