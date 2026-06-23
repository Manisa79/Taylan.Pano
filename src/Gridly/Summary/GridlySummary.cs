using Gridly.Columns;

namespace Gridly.Summary;

public enum GridlySummaryType { None, Count, Sum, Average, Min, Max }

public sealed class GridlySummaryItem
{
    public GridlyColumn Column { get; init; } = null!;
    public GridlySummaryType Type { get; init; }
    public string Format { get; init; } = "{0}";
    public string Calculate(IEnumerable<object> rows)
    {
        var values = rows.Select(r => Column.GetValue(r)).Where(v => v != null && v != DBNull.Value).ToList();
        object result = Type switch
        {
            GridlySummaryType.Count => values.Count,
            GridlySummaryType.Sum => values.Sum(ToDecimal),
            GridlySummaryType.Average => values.Count == 0 ? 0 : values.Average(ToDecimal),
            GridlySummaryType.Min => values.Select(v => Convert.ToString(v)).OrderBy(x => x).FirstOrDefault() ?? string.Empty,
            GridlySummaryType.Max => values.Select(v => Convert.ToString(v)).OrderByDescending(x => x).FirstOrDefault() ?? string.Empty,
            _ => string.Empty
        };
        return string.Format(Format, result);
    }
    private static decimal ToDecimal(object? v) { try { return Convert.ToDecimal(v); } catch { return 0; } }
}
