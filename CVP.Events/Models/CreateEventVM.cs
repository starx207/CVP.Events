using System;
using System.ComponentModel.DataAnnotations;
using CVP.Events.Validation;

namespace CVP.Events.Models;

public sealed class CreateEventVM
{
    private TimeSpan _duration = TimeSpan.FromHours(1);

    public CreateEventVM()
    {
        var now = DateTime.Now;
        StartDate = new DateTime(now.Year, now.Month, now.Day, now.Hour + 1, 0, 0);
    }

    [Required(ErrorMessage = "Event title is required")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Start date is required")]
    [DateBefore(nameof(EndDate), ErrorMessage = "Start date must be before end date")]
    public DateTime StartDate
    {
        get;
        set
        {
            if (value == field)
            {
                return;
            }
            field = value;
            EndDate = value.Add(_duration);
        }
    }

    [Required(ErrorMessage = "End date is required")]
    [DateAfter(nameof(StartDate), ErrorMessage = "End date must be after start date")]
    public DateTime EndDate
    {
        get;
        set
        {
            if (value == field)
            {
                return;
            }
            field = value;
            if (value > StartDate)
            {
                _duration = value - StartDate;
            }
        }
    }
}
