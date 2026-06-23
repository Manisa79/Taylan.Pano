using System.ComponentModel;
using Gridly.Columns;

namespace Gridly.Core;

public enum GridlyCardFieldRole
{
    Title = 0,
    Subtitle = 1,
    Body = 2,
    Footer = 3,
    BadgeText = 4,
    BadgeColor = 5,
    AccentColor = 6,
    StatusText = 7,
    Hidden = 8
}

public enum GridlyCardLayoutDensity
{
    Compact = 0,
    Comfortable = 1,
    Spacious = 2
}

public sealed class GridlyCardLayoutField
{
    public string ColumnKey { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public GridlyCardFieldRole Role { get; set; } = GridlyCardFieldRole.Body;
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public bool ShowCaption { get; set; }
    public int MaxLines { get; set; } = 1;
    public Color? AccentColor { get; set; }
    public override string ToString() => string.IsNullOrWhiteSpace(Caption) ? ColumnKey : Caption;
}

public sealed class GridlyCardLayoutDefinition
{
    public string Name { get; set; } = "Default";
    public GridlyCardLayoutDensity Density { get; set; } = GridlyCardLayoutDensity.Comfortable;
    public bool ShowAccentBar { get; set; } = true;
    public bool ShowStatusDot { get; set; } = true;
    public bool AutoBadgesFromBadgeColumns { get; set; } = true;
    public List<GridlyCardLayoutField> Fields { get; } = new();

    public static GridlyCardLayoutDefinition FromColumns(IEnumerable<GridlyColumn> columns)
    {
        var definition = new GridlyCardLayoutDefinition();
        int order = 0;
        foreach (GridlyColumn column in columns)
        {
            definition.Fields.Add(new GridlyCardLayoutField
            {
                ColumnKey = column.LayoutKey,
                Caption = column.Header,
                Order = order,
                Role = order switch
                {
                    0 => GridlyCardFieldRole.Title,
                    1 => GridlyCardFieldRole.Subtitle,
                    _ => GridlyCardFieldRole.Body
                },
                Visible = column.Visible,
                ShowCaption = order > 1
            });
            order++;
        }
        return definition;
    }
}
