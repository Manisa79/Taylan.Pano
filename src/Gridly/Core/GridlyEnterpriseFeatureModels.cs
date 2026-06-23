using System.Collections;
using System.ComponentModel;
using Gridly.Columns;

namespace Gridly.Core;

public enum GridlyCardActionPlacement
{
    TopRight = 0,
    TopLeft = 1,
    BottomRight = 2,
    BottomLeft = 3
}

public enum GridlyLiveUpdateMode
{
    Off = 0,
    ManualDiff = 1,
    TimerRefresh = 2,
    ProviderWatch = 3
}

public enum GridlyChangedRowAnimation
{
    None = 0,
    Flash = 1,
    AccentPulse = 2
}

public sealed class GridlyCardAction
{
    public string Key { get; set; } = string.Empty;
    public string? Text { get; set; }
    public Image? Image { get; set; }
    public GridlyCardGlyph Glyph { get; set; } = GridlyCardGlyph.None;
    public GridlyCardActionPlacement Placement { get; set; } = GridlyCardActionPlacement.TopRight;
    public Color? BackColor { get; set; }
    public Color? ForeColor { get; set; }
    public string? ToolTipText { get; set; }
    public bool Visible { get; set; } = true;
    public Action<object>? Click { get; set; }
}

public sealed class GridlyCardActionClickEventArgs : EventArgs
{
    public GridlyCardActionClickEventArgs(object rowObject, GridlyCardAction action)
    {
        RowObject = rowObject;
        Action = action;
    }

    public object RowObject { get; }
    public GridlyCardAction Action { get; }
}

public sealed class GridlyConditionalRule
{
    public string Name { get; set; } = string.Empty;
    public int Priority { get; set; }
    public GridlyColumn? Column { get; set; }
    public Func<object, GridlyColumn?, bool> Condition { get; set; } = (_, _) => false;
    public Color? BackColor { get; set; }
    public Color? ForeColor { get; set; }
    public Color? AccentColor { get; set; }
    public Image? Icon { get; set; }
    public bool StopIfTrue { get; set; }
}

public sealed class GridlySmartSearchToken
{
    public string? ColumnKey { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool Exclude { get; set; }
}

public sealed class GridlyPluginCollection : CollectionBase
{
    private readonly GridlyView _owner;

    internal GridlyPluginCollection(GridlyView owner)
    {
        _owner = owner;
    }

    public void Add(IGridlyPlugin plugin)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        List.Add(plugin);
        plugin.Attach(_owner);
    }

    public void Remove(IGridlyPlugin plugin)
    {
        if (plugin == null) return;
        if (List.Contains(plugin))
        {
            plugin.Detach(_owner);
            List.Remove(plugin);
        }
    }

    public IGridlyPlugin this[int index] => (IGridlyPlugin)List[index]!;
}

public interface IGridlyPlugin
{
    string Name { get; }
    void Attach(GridlyView grid);
    void Detach(GridlyView grid);
}
