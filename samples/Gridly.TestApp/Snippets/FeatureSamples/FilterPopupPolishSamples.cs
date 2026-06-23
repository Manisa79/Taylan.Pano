using Gridly.Core;

namespace Gridly.FeatureSamples;

public static class FilterPopupPolishSamples
{
    public static void ApplyFilterPopupPolish(GridlyView grid)
    {
        grid.FilterMenuMode = Gridly.Filtering.GridlyFilterMenuMode.PopupMenu;
        grid.FilterPopupResizable = true;
        grid.FilterPopupEdgeResize = true;
        grid.FilterPopupRememberSize = true;
        grid.FilterPopupShowActionIcons = true;
        grid.FilterPopupHighlightActiveCommands = true;
        grid.FilterPopupDefaultSize = new Size(520, 560);
        grid.FilterPopupMinimumSize = new Size(340, 380);
    }
}
