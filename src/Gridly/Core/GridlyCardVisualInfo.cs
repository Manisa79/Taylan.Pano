namespace Gridly.Core;

/// <summary>
/// Kart/Tile/Dashboard görünümlerinde uygulamanın satır bazlı görsel işaretler vermesi için genel model.
/// Ticket, stok, üretim, dosya, sipariş gibi farklı senaryolarda aynı renderer altyapısı kullanılabilir.
/// </summary>
public sealed class GridlyCardVisualInfo
{
    public Color? AccentColor { get; set; }
    public GridlyCardAccentMode AccentMode { get; set; } = GridlyCardAccentMode.Auto;
    public Color? DotColor { get; set; }
    public bool? ShowDot { get; set; }
    public List<GridlyCardBadge> Badges { get; } = new();
    public List<GridlyCardAction> Actions { get; } = new();
}

public sealed class GridlyCardBadge
{
    public string? Text { get; set; }
    public Image? Image { get; set; }
    public GridlyCardGlyph Glyph { get; set; } = GridlyCardGlyph.None;
    public Color? BackColor { get; set; }
    public Color? ForeColor { get; set; }
    public GridlyCardBadgePlacement Placement { get; set; } = GridlyCardBadgePlacement.TopRight;
    public string? ToolTipText { get; set; }
}

public enum GridlyCardAccentMode
{
    Auto,
    None,
    TopBar,
    LeftBar,
    BottomBar,
    Outline,
    Glow
}

public enum GridlyCardBadgePlacement
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public enum GridlyCardGlyph
{
    None,
    Info,
    Warning,
    Error,
    Success,
    Message,
    Attachment,
    Pin,
    Lock,
    Star,
    Check,
    Clock,
    Flag,
    Bell
}
