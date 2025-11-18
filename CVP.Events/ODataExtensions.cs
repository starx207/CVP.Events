using System;
using CVP.Events.Contracts.Responses;
using CVP.Events.Models;

namespace CVP.Events;

public static class ODataExtensions
{
    extension(string sValue)
    {
        public string? ToTitleFilter() => ToQueryContainsFilter(sValue, nameof(EventItem.Title).ToLower());

        public string? ToQueryContainsFilter(string propertyName) => sValue is { Length: > 0 }
            ? $"contains({propertyName}, '{sValue.Replace("'", "''")}')"
            : null;
    }

    extension(SortOption option)
    {
        public string ToSortQuery() => option switch
        {
            SortOption.StartDateAsc => "startDate asc",
            SortOption.StartDateDesc => "startDate desc",
            SortOption.TitleAZ => "title asc",
            SortOption.TitleZA => "title desc",
            _ => throw new NotSupportedException()
        };
    }
}
