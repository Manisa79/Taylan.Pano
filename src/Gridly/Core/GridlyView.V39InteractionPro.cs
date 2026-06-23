using System.ComponentModel;

namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - V39 Interaction Pro")]
    [DefaultValue(false)]
    [Description("Command Palette, Search Everywhere, sağ tık aksiyonları ve klavye kısa yollarını tek profil altında açar.")]
    public bool EnableInteractionPro { get; set; }

    [Category("Gridly - V39 Interaction Pro")]
    [DefaultValue(true)]
    public bool EnableRightClickActionMenu { get; set; } = true;

    [Category("Gridly - V39 Interaction Pro")]
    [DefaultValue(true)]
    public bool EnablePowerUserShortcuts { get; set; } = true;

    [Category("Gridly - V39 Interaction Pro")]
    [DefaultValue(true)]
    public bool EnableViewModeShortcuts { get; set; } = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Gridly - V39 Interaction Pro")]
    public List<GridlyShortcutAction> ShortcutActions { get; } = new();

    public void ApplyV39InteractionProfile(GridlyV39InteractionPreset preset)
    {
        EnableInteractionPro = true;
        EnableCommandPalette = true;
        EnableSearchEverywhere = true;
        EnableModernSearchPanel = true;
        SearchPanelCanFilterResults = true;
        ShowQuickFilterBar = true;
        EnableKeyboardFilterShortcut = true;
        EnablePowerUserShortcuts = true;
        EnsureDefaultShortcutActions();

        switch (preset)
        {
            case GridlyV39InteractionPreset.TouchFriendly:
                RowHeight = Math.Max(RowHeight, 36);
                TilePreferredHeight = Math.Max(TilePreferredHeight, 132);
                break;
            case GridlyV39InteractionPreset.AudixMedia:
                SetViewMode(GridlyViewMode.Poster);
                ShowMediaOverlayButton = true;
                ShowMediaQualityBadge = true;
                break;
            case GridlyV39InteractionPreset.FactoryOperator:
                SetViewMode(GridlyViewMode.DashboardCard);
                ShowQuickFilterBar = true;
                ShowActiveFilterChips = true;
                break;
        }

        RefreshView();
    }

    public void EnsureDefaultShortcutActions()
    {
        if (ShortcutActions.Count > 0) return;
        ShortcutActions.Add(new GridlyShortcutAction { KeyText = "Ctrl+K", Title = "Search Everywhere", Description = "Satır, kolon ve filtrelerde hızlı arama." });
        ShortcutActions.Add(new GridlyShortcutAction { KeyText = "Ctrl+Shift+P", Title = "Command Palette", Description = "Görünüm, export ve layout komutlarını tek yerden çalıştır." });
        ShortcutActions.Add(new GridlyShortcutAction { KeyText = "Ctrl+1..9", Title = "View Mode", Description = "Sık kullanılan görünüm modları arasında geçiş." });
        ShortcutActions.Add(new GridlyShortcutAction { KeyText = "F2", Title = "Edit", Description = "Seçili hücreyi düzenle." });
    }
}
