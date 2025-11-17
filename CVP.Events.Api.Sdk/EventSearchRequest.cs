using System;
using CVP.Events.Api.Sdk.Requests;

namespace CVP.Events.Api.Sdk;

public class EventSearchRequest
{
    public int? Top { get; init; }
    public int? Skip { get; init; }
    public string? Filter { get; init; }
    public string? OrderBy { get; init; }

    public static EventSearchRequest FromEventRequest(GetAllEventsRequest request) => new()
    {
        Top = request.PageSize,
        Skip = request is { Page: { } page, PageSize: { } size } ? ((page - 1) * size) : null,
        Filter = request.Filter,
        OrderBy = request.OrderBy
    };
}
