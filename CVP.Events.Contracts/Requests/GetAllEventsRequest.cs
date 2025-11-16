using System;

namespace CVP.Events.Api.Sdk.Requests;

public class GetAllEventsRequest : PagedRequest
{
    public string? Filter { get; init; }
    public string? SortBy { get; init; }
}
