using System;
using CVP.Events.Contracts.Responses;

namespace CVP.Events.Models;

public sealed class EventItemVM
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public static class EventItemVMExtensions
{
    extension(AllEventsResponse response)
    {
        public EventItemVM[] ToItemViewModel() => response.Items.Length > 0
        ? [.. response.Items.Select(i => new EventItemVM() { Title = i.Title, Description = i.Description, StartDate = i.StartDate, EndDate = i.EndDate })]
        : [];
    }
}
