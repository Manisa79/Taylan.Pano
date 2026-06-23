using System.ComponentModel;

namespace Gridly.Columns;

/// <summary>
/// Gridly List View kolonu. Yeni Gridly/GLV geçişinde ana kolon tipidir.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GLVColumn : GridlyColumn
{
    public GLVColumn() : base() { }
    public GLVColumn(string title, string aspectName, int width = 120) : base(title, aspectName, width) { }
}
