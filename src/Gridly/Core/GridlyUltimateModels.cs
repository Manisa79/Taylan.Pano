using System.ComponentModel;
using System.Text.Json.Serialization;
using Gridly.Columns;

namespace Gridly.Core;

public sealed class GridlyUltimateOptions
{
    [DefaultValue(true)] public bool EnableQueryLanguage { get; set; } = true;
    [DefaultValue(true)] public bool EnableExpressionEngine { get; set; } = true;
    [DefaultValue(true)] public bool EnableActionPipeline { get; set; } = true;
    [DefaultValue(true)] public bool EnableChangeTracking { get; set; } = true;
    [DefaultValue(true)] public bool EnableLayoutPackages { get; set; } = true;
    [DefaultValue(true)] public bool EnableEventBus { get; set; } = true;
    [DefaultValue(true)] public bool EnableSmartSuggestions { get; set; } = true;
    [DefaultValue(true)] public bool EnableDataProfiling { get; set; } = true;
    [DefaultValue(true)] public bool EnableKeyboardShortcuts { get; set; } = true;
    [DefaultValue(true)] public bool EnablePowerUserCommands { get; set; } = true;
}

public sealed class GridlyActionContext
{
    public GridlyActionContext(GridlyView grid, object? rowObject, GridlyColumn? column, string trigger)
    {
        Grid = grid;
        RowObject = rowObject;
        Column = column;
        Trigger = trigger;
    }

    public GridlyView Grid { get; }
    public object? RowObject { get; }
    public GridlyColumn? Column { get; }
    public string Trigger { get; }
    public IDictionary<string, object?> Items { get; } = new Dictionary<string, object?>();
}

public sealed class GridlyActionStep
{
    public string Name { get; set; } = string.Empty;
    public string Trigger { get; set; } = "manual";
    public Func<GridlyActionContext, bool>? Condition { get; set; }
    public Action<GridlyActionContext>? Execute { get; set; }
    public bool ContinueOnError { get; set; }
}

public sealed class GridlyActionPipeline
{
    private readonly List<GridlyActionStep> _steps = new();
    public IReadOnlyList<GridlyActionStep> Steps => _steps;

    public void Add(GridlyActionStep step)
    {
        if (step == null) throw new ArgumentNullException(nameof(step));
        _steps.RemoveAll(x => string.Equals(x.Name, step.Name, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(step.Name));
        _steps.Add(step);
    }

    public void Clear() => _steps.Clear();

    public int Run(GridlyActionContext context)
    {
        int count = 0;
        foreach (GridlyActionStep step in _steps.Where(s => string.Equals(s.Trigger, context.Trigger, StringComparison.OrdinalIgnoreCase)))
        {
            try
            {
                if (step.Condition != null && !step.Condition(context)) continue;
                step.Execute?.Invoke(context);
                count++;
            }
            catch
            {
                if (!step.ContinueOnError) throw;
            }
        }
        return count;
    }
}

public sealed class GridlyEventSubscription : IDisposable
{
    private readonly Action _dispose;
    internal GridlyEventSubscription(Action dispose) { _dispose = dispose; }
    public void Dispose() => _dispose();
}

public sealed class GridlyEventBus
{
    private readonly Dictionary<string, List<Action<GridlyEventPayload>>> _handlers = new(StringComparer.OrdinalIgnoreCase);

    public GridlyEventSubscription Subscribe(string eventName, Action<GridlyEventPayload> handler)
    {
        if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentException("Event name cannot be empty.", nameof(eventName));
        if (handler == null) throw new ArgumentNullException(nameof(handler));
        if (!_handlers.TryGetValue(eventName, out List<Action<GridlyEventPayload>>? list))
        {
            list = new List<Action<GridlyEventPayload>>();
            _handlers[eventName] = list;
        }
        list.Add(handler);
        return new GridlyEventSubscription(() => list.Remove(handler));
    }

    public void Publish(string eventName, GridlyEventPayload payload)
    {
        if (!_handlers.TryGetValue(eventName, out List<Action<GridlyEventPayload>>? list)) return;
        foreach (Action<GridlyEventPayload> handler in list.ToArray()) handler(payload);
    }
}

public sealed class GridlyEventPayload
{
    public GridlyEventPayload(GridlyView grid, object? rowObject = null, GridlyColumn? column = null)
    {
        Grid = grid;
        RowObject = rowObject;
        Column = column;
    }

    public GridlyView Grid { get; }
    public object? RowObject { get; }
    public GridlyColumn? Column { get; }
    public IDictionary<string, object?> Data { get; } = new Dictionary<string, object?>();
}

public sealed class GridlyTrackedCellChange
{
    public string ColumnKey { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public object? OldValue { get; set; }
    public object? NewValue { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.Now;
}

public sealed class GridlyRowChangeSet
{
    public object? RowObject { get; set; }
    public string RowKey { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; } = DateTime.Now;
    public List<GridlyTrackedCellChange> Changes { get; } = new();
}

public sealed class GridlyDataColumnProfile
{
    public string ColumnKey { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public int VisibleCount { get; set; }
    public int NullCount { get; set; }
    public int BlankCount { get; set; }
    public int UniqueCount { get; set; }
    public bool Numeric { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public double? Average { get; set; }
    public List<string> TopValues { get; set; } = new();
    public List<string> Samples { get; set; } = new();
}

public sealed class GridlyLayoutPackage
{
    public string Name { get; set; } = string.Empty;
    public string GridlyVersion { get; set; } = GridlyVersionInfo.Version;
    public DateTime ExportedAt { get; set; } = DateTime.Now;
    public string ViewMode { get; set; } = string.Empty;
    public int RowHeight { get; set; }
    public string Query { get; set; } = string.Empty;
    public List<GridlyColumnLayoutItem> Columns { get; set; } = new();
    public Dictionary<string, string> FilterPresets { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public sealed class GridlyColumnLayoutItem
{
    public string Key { get; set; } = string.Empty;
    public string AspectName { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public bool Visible { get; set; }
    public int Width { get; set; }
    public int DisplayIndex { get; set; }
    public string PinMode { get; set; } = string.Empty;
}

public sealed class GridlySuggestion
{
    public string Text { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string InsertText { get; set; } = string.Empty;
    public override string ToString() => string.IsNullOrWhiteSpace(Category) ? Text : $"{Text}  ({Category})";
}
