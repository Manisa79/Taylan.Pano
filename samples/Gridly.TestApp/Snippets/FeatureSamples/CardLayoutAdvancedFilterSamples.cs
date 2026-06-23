using Gridly.Core;
using Gridly.Columns;

namespace Gridly.FeatureSamples;

public static class CardLayoutAdvancedFilterSamples
{
    public static void Configure(GridlyView grid)
    {
        grid.FilterMenuMode = Gridly.Filtering.GridlyFilterMenuMode.PopupMenu;
        grid.LockDetailsRowHeightOnSelection = true;
        grid.DetailsRowHeight = 28;
        grid.AutoRowHeightForMultilineCells = false;

        grid.Columns.Add(new GridlyColumn("Durum", "Status", 120) { CardRole = "Title", CardOrder = 0 });
        grid.Columns.Add(new GridlyColumn("Makine Tipi", "MachineType", 140) { CardRole = "Subtitle", CardOrder = 1 });
        grid.Columns.Add(new GridlyColumn("Makine", "Machine", 160) { CardRole = "Body", CardOrder = 2, CardShowCaption = true });
        grid.Columns.Add(new GridlyColumn("Oto Kod", "AutoCode", 120) { CardRole = "BadgeText", CardOrder = 3 });

        grid.ResetCardLayoutDefinition();
        // Runtime designer:
        // grid.ShowCardLayoutDesigner();
    }
}
