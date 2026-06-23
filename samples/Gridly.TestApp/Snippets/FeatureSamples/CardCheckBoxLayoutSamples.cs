using Gridly.Core;

namespace Gridly.FeatureSamples;

public static class CardCheckBoxLayoutSamples
{
    public static void ApplyVisibleTopLeft(GridlyView grid)
    {
        grid.ViewMode = GridlyViewMode.DashboardCard;
        grid.TileCheckBoxes = true;
        grid.TileCheckBoxPosition = GridlyTileCheckBoxPosition.TopLeft;
        grid.TileCheckBoxReserveTextArea = true;
        grid.TileCheckBoxDrawOnTop = true;
        grid.TileCheckBoxShowBackground = true;
        grid.TileCheckBoxHitPadding = 6;
    }

    public static void ApplyCleanHoverMode(GridlyView grid)
    {
        grid.ViewMode = GridlyViewMode.DashboardCard;
        grid.TileCheckBoxes = true;
        grid.TileCheckBoxPosition = GridlyTileCheckBoxPosition.TopRight;
        grid.TileCheckBoxReserveTextArea = true;
        grid.TileCheckBoxDrawOnTop = true;
        grid.TileCheckBoxVisibilityMode = GridlyTileCheckBoxVisibilityMode.CheckedOrHoverOrSelected;
    }
}
