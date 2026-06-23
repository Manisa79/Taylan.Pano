using Gridly.Columns;

namespace Gridly.Intelligence;

public sealed class GridlyChangeTracker
{
    private readonly Dictionary<string, Dictionary<string, object?>> _snapshot = new(StringComparer.OrdinalIgnoreCase);

    public void Capture(IEnumerable<object> rows, IEnumerable<GridlyColumn> columns, Func<object, string>? keySelector = null)
    {
        _snapshot.Clear();
        foreach (object row in rows)
        {
            string key = ResolveKey(row, keySelector);
            _snapshot[key] = CaptureRow(row, columns);
        }
    }

    public IReadOnlyList<Gridly.Core.GridlyRowChangeSet> Detect(IEnumerable<object> rows, IEnumerable<GridlyColumn> columns, Func<object, string>? keySelector = null)
    {
        List<Gridly.Core.GridlyRowChangeSet> result = new();
        List<GridlyColumn> colList = columns.ToList();
        foreach (object row in rows)
        {
            string key = ResolveKey(row, keySelector);
            Dictionary<string, object?> current = CaptureRow(row, colList);
            if (!_snapshot.TryGetValue(key, out Dictionary<string, object?>? old))
            {
                Gridly.Core.GridlyRowChangeSet added = new Gridly.Core.GridlyRowChangeSet { RowObject = row, RowKey = key, ChangedAt = DateTime.Now };
                foreach (GridlyColumn col in colList)
                    added.Changes.Add(new Gridly.Core.GridlyTrackedCellChange { ColumnKey = col.Key, Header = col.Header, OldValue = null, NewValue = current.TryGetValue(col.Key, out object? nv) ? nv : null });
                result.Add(added);
                continue;
            }
            Gridly.Core.GridlyRowChangeSet set = new Gridly.Core.GridlyRowChangeSet { RowObject = row, RowKey = key, ChangedAt = DateTime.Now };
            foreach (GridlyColumn col in colList)
            {
                old.TryGetValue(col.Key, out object? ov);
                current.TryGetValue(col.Key, out object? nv);
                if (!object.Equals(Normalize(ov), Normalize(nv)))
                    set.Changes.Add(new Gridly.Core.GridlyTrackedCellChange { ColumnKey = col.Key, Header = col.Header, OldValue = ov, NewValue = nv, ChangedAt = DateTime.Now });
            }
            if (set.Changes.Count > 0) result.Add(set);
        }
        return result;
    }

    private static Dictionary<string, object?> CaptureRow(object row, IEnumerable<GridlyColumn> columns)
    {
        Dictionary<string, object?> values = new(StringComparer.OrdinalIgnoreCase);
        foreach (GridlyColumn col in columns) values[col.Key] = col.GetValue(row);
        return values;
    }

    private static string ResolveKey(object row, Func<object, string>? keySelector)
    {
        if (keySelector != null) return keySelector(row) ?? string.Empty;
        object? id = GridlyExpressionEngine.GetValue(row, "Id") ?? GridlyExpressionEngine.GetValue(row, "ID") ?? GridlyExpressionEngine.GetValue(row, "Key");
        return Convert.ToString(id) ?? row.GetHashCode().ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private static object? Normalize(object? value) => value == DBNull.Value ? null : value;
}
