using System;

namespace CVP.Events.Contracts.Responses;

public class AllEventsResponse
{
    public int Total { get; set; }
    public EventItem[] Items { get; set; } = [];
}

public class EventItem
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
