using System.ComponentModel;
using Gridly.Core;
using Gridly.Tree;

namespace Gridly.Compatibility;

/// <summary>
/// Gridly/GLV tabanlı hızlı liste görünümü.
/// Gridly tabanlı ana hızlı liste kontrolüdür; eski hızlı alias tamamen kaldırıldı.
/// </summary>
[Designer(typeof(global::Gridly.Design.GridlyViewDesigner))]
[ToolboxItem(true)]
[DesignTimeVisible(true)]
[Description("FastGridlyView: yüksek performanslı Gridly liste kontrolü.")]
public class FastGridlyView : GridlyView
{
    public FastGridlyView()
    {
        AutoEnsureReadableTextColors = true;
        AutoApplyThemeToColumnHeaders = true;
        UseUnifiedThemeVisuals = true;
        AutoThemeFromParent = true;
        ApplyUltimatePerformanceProfile();
        PreserveFixedColumnWidthsOnAutoFit = true;
        IncludeCellImagesInAutoResizeWidth = true;
    }
}

/// <summary>
/// DataListView benzeri kullanım için hazır ayarlı GridlyView.
/// DataSource/DataTable, AutoGenerateColumns ve inline edit altyapısı GridlyView içindedir.
/// </summary>
[Designer(typeof(global::Gridly.Design.GridlyViewDesigner))]
[ToolboxItem(true)]
[DesignTimeVisible(true)]
[Description("DataListView: DataSource/DataTable odaklı Gridly liste kontrolü.")]
public class DataListView : GridlyView
{
    public DataListView()
    {
        AutoEnsureReadableTextColors = true;
        AutoApplyThemeToColumnHeaders = true;
        UseUnifiedThemeVisuals = true;
        AutoThemeFromParent = true;
        AutoGenerateColumns = true;
        EnableCellEditing = true;
        AllowEditAllCells = true;
        EnableInlineDatabaseEditing = true;
        ShowGridLines = true;
        FullRowSelect = true;
    }
}

/// <summary>
/// TreeListView benzeri kullanım için isim uyumluluğu. Asıl ağaç mantığı TreeGridlyView'dedir.
/// </summary>
[Designer(typeof(global::Gridly.Design.GridlyViewDesigner))]
[ToolboxItem(true)]
[DesignTimeVisible(true)]
[Description("TreeListView: TreeGridlyView tabanlı hiyerarşik Gridly kontrolü.")]
public class TreeListView : TreeGridlyView
{
    public TreeListView()
    {
        AutoEnsureReadableTextColors = true;
        AutoApplyThemeToColumnHeaders = true;
        UseUnifiedThemeVisuals = true;
        AutoThemeFromParent = true;
    }
}
