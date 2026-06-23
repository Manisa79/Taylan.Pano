using Gridly.Columns;
using Gridly.Core;

namespace Gridly.Exporting;

public enum GridlyPdfExportMode
{
    Auto = 0,
    Table = 1,
    Card = 2
}

public enum GridlyPdfPageOrientation
{
    Portrait = 0,
    Landscape = 1
}

public enum GridlyPdfThemeMode
{
    PrintFriendly = 0,
    PreserveGridTheme = 1
}

public sealed class GridlyPdfExportOptions
{
    public string Title { get; set; } = "Gridly Export";
    public string? Subtitle { get; set; }
    public GridlyPdfExportMode Mode { get; set; } = GridlyPdfExportMode.Auto;
    public GridlyPdfPageOrientation Orientation { get; set; } = GridlyPdfPageOrientation.Landscape;
    public GridlyPdfThemeMode ThemeMode { get; set; } = GridlyPdfThemeMode.PrintFriendly;
    public bool FitToPageWidth { get; set; } = true;
    public bool ShowGridLines { get; set; } = true;
    public bool ZebraRows { get; set; } = true;
    public bool ShowHeader { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowFilterSummary { get; set; } = true;
    public bool IncludeHiddenColumns { get; set; }
    public int MaxRows { get; set; } = 50000;
    public int CardColumns { get; set; } = 2;
    public int CardMinHeight { get; set; } = 92;
    public int CardGap { get; set; } = 10;
    public int MarginLeft { get; set; } = 36;
    public int MarginTop { get; set; } = 36;
    public int MarginRight { get; set; } = 36;
    public int MarginBottom { get; set; } = 36;
    public string? FooterText { get; set; }
    public Func<object, GridlyCardVisualInfo?>? CardVisualInfoResolver { get; set; }
    public GridlyCardLayoutDefinition? CardLayout { get; set; }
    public Func<GridlyColumn, bool>? ColumnPredicate { get; set; }
}
