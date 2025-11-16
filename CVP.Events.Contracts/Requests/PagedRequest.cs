using System;

namespace CVP.Events.Api.Sdk.Requests;

public class PagedRequest
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;

    public int? Page { get; init; } = DefaultPage;
    public int? PageSize { get; init; } = DefaultPageSize;
}
