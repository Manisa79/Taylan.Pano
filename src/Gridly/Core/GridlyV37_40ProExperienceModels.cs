using System.ComponentModel;

namespace Gridly.Core;

public enum GridlyV37LayoutScope
{
    User,
    Project,
    Machine,
    Scenario
}

public enum GridlyV38PerformancePreset
{
    Balanced,
    LargeData,
    MediaLibrary,
    VirtualMillionRows,
    LowMemory
}

public enum GridlyV39InteractionPreset
{
    Standard,
    PowerUser,
    TouchFriendly,
    AudixMedia,
    FactoryOperator
}

public enum GridlyV40AnalyticsPreset
{
    KpiDashboard,
    HeatMap,
    Timeline,
    MiniCharts,
    FactoryOverview
}

public enum GridlyMediaSmartPreset
{
    Music,
    Movie,
    Photo,
    Document
}

public sealed class GridlyLayoutPresetInfo
{
    public string Name { get; set; } = string.Empty;
    public string ViewMode { get; set; } = string.Empty;
    public string GroupBy { get; set; } = string.Empty;
    public string GlobalFilter { get; set; } = string.Empty;
    public DateTime SavedAt { get; set; } = DateTime.Now;
    public List<GridlyColumnLayoutInfo> Columns { get; set; } = new();
}

public sealed class GridlyColumnLayoutInfo
{
    public string Name { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public string AspectName { get; set; } = string.Empty;
    public int Width { get; set; }
    public int DisplayIndex { get; set; }
    public bool Visible { get; set; }
}

public sealed class GridlyShortcutAction
{
    public string KeyText { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class GridlyAnalyticsMetric
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double NumberValue { get; set; }
}
