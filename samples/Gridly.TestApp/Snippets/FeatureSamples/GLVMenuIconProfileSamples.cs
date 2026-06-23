using Gridly.Core;

namespace Gridly.FeatureSamples;

public static class GLVMenuIconProfileSamples
{
    public static void ApplyBuiltInMenuIcons(GridlyView grid)
    {
        grid.MenuIconMode = GridlyMenuIconMode.BuiltIn;
        grid.MenuIconSize = GridlyMenuIconSize.Small16;
        grid.ApplyIconsToMergedUserMenus = true;
        grid.SaveMenuIconPreferencesInUserLayout = true;
        grid.AutoSaveUserLayout = true;
        grid.AutoLoadUserLayout = true;
        grid.UserLayoutKey = "Samples.MenuIcons.MainGrid";
    }

    public static void ApplyCustomMenuIcons(GridlyView grid, string iconFolder)
    {
        grid.MenuIconMode = GridlyMenuIconMode.BuiltInThenCustom;
        grid.MenuIconSize = GridlyMenuIconSize.Medium20;
        grid.SetCustomMenuIconFolder(iconFolder);
        // Desteklenen dosya adları örnekleri:
        // filter.png, clear_filter.png, sort_asc.png, sort_desc.png,
        // columns.png, layout.png, theme.png, view.png, copy.png, print.png, export.png, group.png, drag.png
    }

    public static void DisableMenuIcons(GridlyView grid)
    {
        grid.MenuIconMode = GridlyMenuIconMode.None;
    }
}
