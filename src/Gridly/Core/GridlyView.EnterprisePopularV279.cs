using System.ComponentModel;
using Gridly.Formatting;
using Gridly.Summary;

namespace Gridly.Core;

public enum GridlyPopularFeaturePreset
{
    Balanced,
    MasterData,
    SupportDesk,
    LargeDataReview,
    DataEntry
}

public partial class GridlyView
{
    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("Popüler grid bileşenlerinde beklenen arama, özet, column chooser, conditional format ve hızlı filtre davranışlarını tek preset altında açar.")]
    public bool EnablePopularFeaturePack { get; set; } = true;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(GridlyPopularFeaturePreset.Balanced)]
    [Description("MasterData, Support Desk, yoğun veri veya veri giriş ekranları için önerilen Gridly popüler özellik preset'i.")]
    public GridlyPopularFeaturePreset PopularFeaturePreset { get; set; } = GridlyPopularFeaturePreset.Balanced;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("Ctrl+F modern arama panelini ve aramada sonucu filtreleme seçeneğini varsayılan olarak hazırlar.")]
    public bool PopularFeaturesEnableSearchPanel { get; set; } = true;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("Özet/footer satırını varsayılan olarak etkinleştirir.")]
    public bool PopularFeaturesEnableSummaryFooter { get; set; } = true;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("İlk kolonu dondurma, column chooser ve auto-size menülerini hazır hale getirir.")]
    public bool PopularFeaturesEnableColumnTools { get; set; } = true;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("Filtre popup resize, tooltip, auto-width, advanced filter ve preset davranışlarını açar.")]
    public bool PopularFeaturesEnableFilterTools { get; set; } = true;

    [Category("Gridly - v27.9 Popular Features")]
    [DefaultValue(true)]
    [Description("Durum/progress/uyarı kolonları için hızlı conditional format helperlarını etkin kullanıma hazırlar.")]
    public bool PopularFeaturesEnableConditionalFormatting { get; set; } = true;

    public void ApplyPopularFeaturePack(GridlyPopularFeaturePreset? preset = null)
    {
        if (!EnablePopularFeaturePack) return;

        var effectivePreset = preset ?? PopularFeaturePreset;
        PopularFeaturePreset = effectivePreset;

        if (PopularFeaturesEnableSearchPanel)
        {
            EnableModernSearchPanel = true;
            SearchPanelCanFilterResults = true;
            HighlightSearchText = true;
            HighlightGlobalFilterText = true;
            EnableIncrementalSearch = true;
        }

        if (PopularFeaturesEnableSummaryFooter)
        {
            ShowSummaryFooter = true;
            MaxSummaryScanRows = effectivePreset == GridlyPopularFeaturePreset.LargeDataReview ? 200_000 : 50_000;
        }

        if (PopularFeaturesEnableColumnTools)
        {
            ShowHeaderMenuColumnChooserItem = true;
            ShowColumnChooserInHeaderMenu = true;
            ShowColumnChooserWindowInHeaderMenu = true;
            ColumnChooserMenuStaysOpen = true;
            EnableColumnAutoResizeOnDoubleClick = true;
            AutoResizeIncludeHeader = true;
            IncludeCellImagesInAutoResizeWidth = true;
            if (effectivePreset is GridlyPopularFeaturePreset.MasterData or GridlyPopularFeaturePreset.LargeDataReview)
                FrozenColumnCount = Math.Max(FrozenColumnCount, 1);
        }

        if (PopularFeaturesEnableFilterTools)
        {
            FilterMenuMode = Gridly.Filtering.GridlyFilterMenuMode.Both;
            FilterPopupResizable = true;
            FilterPopupRememberSize = true;
            FilterPopupShowValueTooltips = true;
            FilterPopupAutoWidthForLongValues = true;
            ShowAdvancedFilterMenuItems = true;
            EnableFilterPresets = true;
            EnableSmartFilterEngine = true;
            SmartFilterSearchAllRows = true;
            SmartFilterTopValuesFirst = true;
            FastFilterMenuForHugeLists = true;
            TypedFilterSearchesAllRows = true;
        }

        HeaderContextMenuBehavior = GridlyHeaderContextMenuBehavior.Full;
        MenuProfile = GridlyMenuProfile.Full;
        ShowStateMenuItems = true;
        ShowScenarioMenuItems = true;
        PersistColumnFilters = true;
        PersistVisualPreferences = true;
        EnableGrouping = true;
        SmoothMouseWheelScroll = true;

        switch (effectivePreset)
        {
            case GridlyPopularFeaturePreset.MasterData:
                SetViewMode(GridlyViewMode.DenseList);
                RowHeight = Math.Max(RowHeight, 30);
                MaxFilterDistinctScanRows = 1_000_000;
                MaxAsyncFilterDistinctScanRows = 1_000_000;
                break;
            case GridlyPopularFeaturePreset.SupportDesk:
                SetViewMode(GridlyViewMode.DashboardCard);
                ShowQuickFilterBar = true;
                ShowFloatingFilterButton = true;
                ShowActiveFilterChips = true;
                break;
            case GridlyPopularFeaturePreset.LargeDataReview:
                SetViewMode(GridlyViewMode.Details);
                RowHeight = 26;
                MaxFilterDistinctScanRows = 2_000_000;
                MaxAsyncFilterDistinctScanRows = 2_000_000;
                break;
            case GridlyPopularFeaturePreset.DataEntry:
                SetViewMode(GridlyViewMode.Details);
                AllowEditAllCells = true;
                CellEditActivationKey = Keys.F2;
                ShowSummaryFooter = true;
                break;
            default:
                break;
        }

        BuildViewIndex();
        RefreshView();
    }

    public void AddSemanticStatusConditionalFormat(string aspectName)
    {
        var column = Columns.FirstOrDefault(c => string.Equals(c.AspectName, aspectName, StringComparison.OrdinalIgnoreCase));
        if (column == null) return;

        ConditionalFormats.Add(new GridlyConditionalFormat
        {
            Column = column,
            Predicate = (_, _, value) => ContainsAny(value, "open", "fail", "error", "kritik", "critical", "missing", "eksik"),
            BackColor = Color.FromArgb(88, 32, 38),
            ForeColor = Color.FromArgb(255, 210, 210)
        });
        ConditionalFormats.Add(new GridlyConditionalFormat
        {
            Column = column,
            Predicate = (_, _, value) => ContainsAny(value, "waiting", "bekliyor", "warning", "uyarı", "hold"),
            BackColor = Color.FromArgb(92, 72, 24),
            ForeColor = Color.FromArgb(255, 236, 180)
        });
        ConditionalFormats.Add(new GridlyConditionalFormat
        {
            Column = column,
            Predicate = (_, _, value) => ContainsAny(value, "done", "closed", "ok", "tamam", "resolved"),
            BackColor = Color.FromArgb(28, 78, 48),
            ForeColor = Color.FromArgb(196, 245, 214)
        });
        Invalidate();
    }

    public void AddNumericSummary(string aspectName, GridlySummaryType type = GridlySummaryType.Sum, string format = "Σ {0}")
    {
        var column = Columns.FirstOrDefault(c => string.Equals(c.AspectName, aspectName, StringComparison.OrdinalIgnoreCase));
        if (column == null) return;
        Summaries.Add(new GridlySummaryItem { Column = column, Type = type, Format = format });
        ShowSummaryFooter = true;
        Invalidate();
    }

    public void AddCountSummary(string aspectName, string format = "{0} kayıt")
    {
        var column = Columns.FirstOrDefault(c => string.Equals(c.AspectName, aspectName, StringComparison.OrdinalIgnoreCase));
        if (column == null) return;
        Summaries.Add(new GridlySummaryItem { Column = column, Type = GridlySummaryType.Count, Format = format });
        ShowSummaryFooter = true;
        Invalidate();
    }

    private static bool ContainsAny(object? value, params string[] tokens)
    {
        string text = Convert.ToString(value) ?? string.Empty;
        if (text.Length == 0) return false;
        foreach (string token in tokens)
        {
            if (text.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0) return true;
        }
        return false;
    }
}
