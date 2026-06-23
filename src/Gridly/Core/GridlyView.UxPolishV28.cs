using System.ComponentModel;
using Gridly.Filtering;

namespace Gridly.Core;

public enum GridlyUxPolishPreset
{
    Balanced,
    MasterData,
    SupportDesk,
    PosterGallery,
    LargeData
}

public enum GridlyQuickActionBarMode
{
    Hidden,
    Compact,
    Full
}

public partial class GridlyView
{
    [Category("Gridly - UX Polish v28")]
    [DefaultValue(true)]
    [Description("Kart/poster/dashboard görünümlerinde filtre butonunu kart üstünde değil üst filtre barında gösteren v28 düzenini kullanır.")]
    public bool EnableV28CardFilterPolish { get; set; } = true;

    [Category("Gridly - UX Polish v28")]
    [DefaultValue(GridlyQuickActionBarMode.Compact)]
    [Description("Popüler gridlerdeki arama/filtre/temizle benzeri hızlı aksiyonları tek üst barda toplamak için kullanılan mod.")]
    public GridlyQuickActionBarMode QuickActionBarMode { get; set; } = GridlyQuickActionBarMode.Compact;

    [Category("Gridly - UX Polish v28")]
    [DefaultValue(true)]
    [Description("Kullanıcı deneyimi için arama, filtre, kolon seçici, command palette ve aktif filtre chiplerini birlikte açan hazır paket.")]
    public bool EnableBestPracticeUxPack { get; set; } = true;

    [Category("Gridly - UX Polish v28")]
    [DefaultValue(true)]
    [Description("Filtre/chip alanı dolduğunda içerik alanını ezmeden kart viewport'unu aşağıdan başlatır.")]
    public bool ReserveTopUxAreaForCardViews { get; set; } = true;

    public void ApplyV28UxPolish(GridlyUxPolishPreset preset = GridlyUxPolishPreset.Balanced)
    {
        EnableBestPracticeUxPack = true;
        EnableV28CardFilterPolish = true;
        MoveFilterButtonToTopBar = true;
        ShowFloatingFilterButton = false;
        CardFilterUxPlacement = GridlyCardFilterUxPlacement.TopBar;
        ShowQuickFilterBar = true;
        ShowActiveFilterChips = true;
        CardViewReserveFilterArea = true;
        FilterMenuMode = GridlyFilterMenuMode.Both;
        EnableCommandPalette = true;
        EnableModernEmptyState = true;
        AllowColumnReorder = true;
        ShowColumnChooserInHeaderMenu = true;
        ShowColumnChooserWindowInHeaderMenu = true;
        HeaderContextMenuBehavior = GridlyHeaderContextMenuBehavior.Full;
        CloseHeaderContextMenuBeforeOpeningFilterPopup = true;

        switch (preset)
        {
            case GridlyUxPolishPreset.MasterData:
                QuickFilterPlaceholderText = "BOM / SAP / RefDes içinde ara...";
                CardFilterBarHeight = 76;
                CardFilterContentSpacing = 8;
                ShowSummaryFooter = true;
                break;
            case GridlyUxPolishPreset.SupportDesk:
                QuickFilterPlaceholderText = "Ticket, makine, operatör ara...";
                CardFilterBarHeight = 78;
                CardFilterContentSpacing = 10;
                break;
            case GridlyUxPolishPreset.PosterGallery:
                if (ViewMode != GridlyViewMode.Poster)
                    SetViewMode(GridlyViewMode.Poster);
                QuickFilterPlaceholderText = "Poster / kartlarda ara...";
                CardFilterBarHeight = 72;
                CardFilterContentSpacing = 10;
                ApplyPosterModeDefaults(false);
                break;
            case GridlyUxPolishPreset.LargeData:
                QuickFilterPlaceholderText = "Milyon satır içinde ara...";
                CardFilterBarHeight = 66;
                CardFilterContentSpacing = 6;
                FastFilterMenuForHugeLists = true;
                AsyncLoadFullFilterValues = true;
                TypedFilterSearchesAllRows = true;
                break;
            default:
                QuickFilterPlaceholderText = "Ara / filtrele...";
                CardFilterBarHeight = 74;
                CardFilterContentSpacing = 8;
                break;
        }

        RefreshV28UxPolish();
    }

    public void UseTopBarFilterOnly()
    {
        MoveFilterButtonToTopBar = true;
        ShowFloatingFilterButton = false;
        CardFilterUxPlacement = GridlyCardFilterUxPlacement.TopBar;
        ShowQuickFilterBar = true;
        ShowActiveFilterChips = true;
        CardViewReserveFilterArea = true;
        RefreshV28UxPolish();
    }

    public void UseHybridCardFilterUx()
    {
        MoveFilterButtonToTopBar = false;
        ShowFloatingFilterButton = true;
        CardFilterUxPlacement = GridlyCardFilterUxPlacement.TopBarAndFloatingButton;
        ShowQuickFilterBar = true;
        ShowActiveFilterChips = true;
        CardViewReserveFilterArea = true;
        RefreshV28UxPolish();
    }

    public void RefreshV28UxPolish()
    {
        RefreshCardViewFilterUx();
        Invalidate();
    }
}
