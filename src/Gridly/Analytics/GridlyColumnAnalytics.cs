using Gridly.Columns;

namespace Gridly.Core;

public sealed class GridlyColumnAnalytics
{
    public string ColumnText { get; init; } = string.Empty;
    public string AspectName { get; init; } = string.Empty;
    public int RowCount { get; init; }
    public int BlankCount { get; init; }
    public int DistinctCount { get; init; }
    public IReadOnlyList<GridlyValueCount> TopValues { get; init; } = Array.Empty<GridlyValueCount>();

    public static GridlyColumnAnalytics From(GridlyColumn column, IReadOnlyList<object> rows, int maxDistinct = 8)
    {
        var counts = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
        int blank = 0;
        foreach (var row in rows)
        {
            string value = Convert.ToString(column.GetValue(row)) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(value)) blank++;
            string key = string.IsNullOrWhiteSpace(value) ? "(Blanks)" : value.Trim();
            counts.TryGetValue(key, out int current);
            counts[key] = current + 1;
        }

        return new GridlyColumnAnalytics
        {
            ColumnText = column.Header,
            AspectName = column.AspectName,
            RowCount = rows.Count,
            BlankCount = blank,
            DistinctCount = counts.Count,
            TopValues = counts
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Take(Math.Max(1, maxDistinct))
                .Select(kv => new GridlyValueCount(kv.Key, kv.Value))
                .ToList()
        };
    }
}

public readonly record struct GridlyValueCount(string Value, int Count);
