using System.ComponentModel;
using System.Drawing;
using Gridly.Columns;
using Gridly.Theming;

namespace Gridly.Rendering;

/// <summary>
/// v27 renderer altyapısının hafif çekirdeği. Mevcut çizim motorunu bozmadan kolonlara anlamlı görsel profil bağlamak için kullanılır.
/// Sonraki adımda GridlyView paint akışına tam entegre edilebilir; bugün MasterData/AOI presetlerinde güvenli metadata sağlar.
/// </summary>
public interface IGridlyCellRenderer
{
    string Name { get; }
    void Render(Graphics graphics, Rectangle bounds, object? value, GridlyColumn column, GridlyTheme theme, bool selected, bool hot);
}

public enum GridlyCellVisualProfile
{
    Default,
    Badge,
    IconText,
    Progress,
    Tags,
    Hyperlink,
    ActionButton,
    WarningStatus
}

public static class GridlyCellVisualProfileExtensions
{
    public static GridlyColumn ApplyVisualProfile(this GridlyColumn column, GridlyCellVisualProfile profile)
    {
        if (column == null) throw new ArgumentNullException(nameof(column));

        column.Tag = profile;
        switch (profile)
        {
            case GridlyCellVisualProfile.Badge:
            case GridlyCellVisualProfile.WarningStatus:
                column.Kind = GridlyColumnKind.Badge;
                break;
            case GridlyCellVisualProfile.Progress:
                column.Kind = GridlyColumnKind.ProgressBar;
                break;
            case GridlyCellVisualProfile.Hyperlink:
                column.Kind = GridlyColumnKind.Hyperlink;
                break;
            case GridlyCellVisualProfile.ActionButton:
                column.Kind = GridlyColumnKind.Button;
                break;
        }

        return column;
    }
}

public sealed class GridlyRendererRegistry
{
    private readonly Dictionary<string, IGridlyCellRenderer> _renderers = new(StringComparer.OrdinalIgnoreCase);

    public void Register(IGridlyCellRenderer renderer)
    {
        if (renderer == null) throw new ArgumentNullException(nameof(renderer));
        _renderers[renderer.Name] = renderer;
    }

    public bool TryGet(string name, out IGridlyCellRenderer renderer)
        => _renderers.TryGetValue(name ?? string.Empty, out renderer!);
}

public sealed class GridlyBadgeRenderer : IGridlyCellRenderer
{
    public string Name => "Badge";

    public void Render(Graphics graphics, Rectangle bounds, object? value, GridlyColumn column, GridlyTheme theme, bool selected, bool hot)
    {
        string text = Convert.ToString(value) ?? string.Empty;
        using var brush = new SolidBrush(selected ? theme.SelectionBackColor : theme.PanelBackColor);
        using var fore = new SolidBrush(selected ? theme.SelectionForeColor : theme.ForeColor);
        using var pen = new Pen(theme.GridColor);
        Rectangle pill = Rectangle.Inflate(bounds, -6, -5);
        graphics.FillRoundedRectangle(brush, pill, 10);
        graphics.DrawRoundedRectangle(pen, pill, 10);
        TextRenderer.DrawText(graphics, text, SystemFonts.MessageBoxFont, pill, fore.Color, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }
}

internal static class GridlyRendererDrawingExtensions
{
    public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int radius)
    {
        using var path = CreateRoundedPath(bounds, radius);
        graphics.FillPath(brush, path);
    }

    public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int radius)
    {
        using var path = CreateRoundedPath(bounds, radius);
        graphics.DrawPath(pen, path);
    }

    private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
    {
        int d = Math.Max(1, radius * 2);
        var path = new System.Drawing.Drawing2D.GraphicsPath();
        path.AddArc(bounds.Left, bounds.Top, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}
