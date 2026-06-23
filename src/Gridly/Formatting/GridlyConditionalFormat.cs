using Gridly.Columns;

namespace Gridly.Formatting;

public sealed class GridlyConditionalFormat
{
    public GridlyColumn? Column { get; set; }
    public Func<object, GridlyColumn, object?, bool> Predicate { get; set; } = (_,_,__) => false;
    public Color? BackColor { get; set; }
    public Color? ForeColor { get; set; }
    public Font? Font { get; set; }
    public Image? Icon { get; set; }

    public bool IsMatch(object row, GridlyColumn column)
    {
        if (Column != null && !ReferenceEquals(Column, column)) return false;
        return Predicate(row, column, column.GetValue(row));
    }
}
