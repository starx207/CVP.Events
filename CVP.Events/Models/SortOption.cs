namespace CVP.Events.Models;

public enum SortOption
{
    StartDateAsc,
    StartDateDesc,
    TitleAZ,
    TitleZA
}

public static class SortOptionExtensions
{
    extension(SortOption option)
    {
        public string ToDisplayName() => option switch
        {
            SortOption.StartDateAsc => "Start Date (Oldest First)",
            SortOption.StartDateDesc => "Start Date (Newest First)",
            SortOption.TitleAZ => "Title (A-Z)",
            SortOption.TitleZA => "Title (Z-A)",
            _ => throw new NotSupportedException()
        };
    }
}
