using System.ComponentModel;
using Gridly.Layout;

namespace Gridly.Core;

public partial class GridlyView
{
    private GridlyCardLayoutDefinition? _cardLayoutDefinition;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridlyCardLayoutDefinition CardLayoutDefinition
    {
        get => _cardLayoutDefinition ??= GridlyCardLayoutDefinition.FromColumns(Columns.VisibleColumns);
        set { _cardLayoutDefinition = value; Invalidate(); }
    }

    [Category("Gridly - Card Layout")]
    [DefaultValue(true)]
    [Description("Card/Tile/Dashboard görünümünde kolonlardan otomatik başlık/alt başlık/body/badge yerleşimi üretir.")]
    public bool UseCardLayoutDefinition { get; set; } = true;

    public void ResetCardLayoutDefinition()
    {
        _cardLayoutDefinition = GridlyCardLayoutDefinition.FromColumns(Columns.VisibleColumns);
        Invalidate();
    }

    public bool ShowCardLayoutDesigner(IWin32Window? owner = null)
    {
        using var form = new GridlyCardLayoutDesignerForm(Columns.VisibleColumns, CardLayoutDefinition, _theme);
        if (form.ShowDialog(owner ?? FindForm()) != DialogResult.OK) return false;
        CardLayoutDefinition = form.Result;
        ApplyCardLayoutToColumns();
        return true;
    }

    public void ApplyCardLayoutToColumns()
    {
        if (_cardLayoutDefinition == null) return;
        foreach (GridlyCardLayoutField field in _cardLayoutDefinition.Fields)
        {
            Gridly.Columns.GridlyColumn? column = Columns.FirstOrDefault(c => string.Equals(c.LayoutKey, field.ColumnKey, StringComparison.OrdinalIgnoreCase));
            if (column == null) continue;
            column.VisibleInCard = field.Visible;
            column.CardOrder = field.Order;
            column.CardRole = field.Role.ToString();
            column.CardShowCaption = field.ShowCaption;
            column.CardMaxLines = field.MaxLines;
        }
        Invalidate();
        QueueAutoSaveUserLayout();
    }

    public string ExportCardLayoutText()
    {
        var d = CardLayoutDefinition;
        return string.Join(Environment.NewLine, d.Fields.OrderBy(x => x.Order).Select(x => $"{x.Order};{x.ColumnKey};{x.Caption};{x.Role};{x.Visible};{x.ShowCaption};{x.MaxLines}"));
    }

    private void AddCardLayoutDesignerMenu(ToolStripItemCollection items)
    {
        var menu = new ToolStripMenuItem("Kart yerleşimi");
        menu.DropDownItems.Add("Card Layout Designer...", null, (_, _) => ShowCardLayoutDesigner());
        menu.DropDownItems.Add("Otomatik kart yerleşimi üret", null, (_, _) => ResetCardLayoutDefinition());
        menu.DropDownItems.Add("Kolonlara uygula", null, (_, _) => ApplyCardLayoutToColumns());
        items.Add(menu);
    }
}
