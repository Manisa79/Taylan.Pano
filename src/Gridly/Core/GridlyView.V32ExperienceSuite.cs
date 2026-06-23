namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Kullanıcının görünüm/kolon/filtre alışkanlıklarını host uygulamanın kaydedebilmesi için Gridly tarafında niyet bayrağıdır.")]
    public bool EnableUxIntelligence { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(true)]
    [Description("Son seçilen görünüm modunun host uygulama tarafından saklanmasını önerir. SaveExperienceSnapshot çıktısına ViewMode dahil edilir.")]
    public bool RememberLastViewMode { get; set; } = true;

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(true)]
    [Description("Her ActiveScenario için son seçilen ViewMode değerini runtime içinde hatırlar.")]
    public bool RememberViewModePerScenario { get; set; } = true;

    private readonly Dictionary<GridlyViewScenario, GridlyViewMode> _viewModeMemoryByScenario = new();
    private bool _suppressViewModeMemory;

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Makine/hat kartlarında Running/Waiting/Fault/Offline gibi fabrika durumlarının overlay mantığında kullanılacağını belirtir.")]
    public bool EnableFactoryStatusOverlay { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue("Status")]
    [Description("Factory Status Overlay için okunacak property/kolon adı. Örn: Status, MachineStatus, Durum.")]
    public string FactoryStatusAspectName { get; set; } = "Status";

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<object, GridlyMachineStatus>? FactoryStatusGetter { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Search Everywhere ve komut paleti kullanımlarında tüm kolonlar üzerinde hızlı arama yapılmasını sağlar.")]
    public bool EnableSearchEverywhere { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Kullanıcının kolon/kart/dashboard yerleşimi oluşturup saklayabileceği Layout Studio akışı için hazır ayar.")]
    public bool EnableLayoutStudio { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("KPI, chart, table, card, heatmap, timeline ve gallery widget tanımlarını tek Gridly içinde yönetme akışını açar.")]
    public bool EnableDashboardBuilder { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Host uygulamanın ürettiği AI/akıllı öneri kartlarının Gridly üstünde/yanında gösterileceğini belirtir.")]
    public bool EnableAiInsights { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(false)]
    [Description("Çok büyük satır sayılarında kart/poster/tile virtualization davranışlarının host veri sağlayıcısıyla birlikte kullanılacağını belirtir.")]
    public bool EnableVirtualizationPro { get; set; }

    [Category("Gridly - V32 Experience Suite")]
    [DefaultValue(100000)]
    [Description("Virtualization Pro senaryolarında hedeflenen büyük veri eşiği.")]
    public int VirtualizationProTargetRows { get; set; } = 100000;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Gridly - V32 Experience Suite")]
    [Description("Dashboard Builder için host uygulamanın doldurabileceği widget tanımları.")]
    public List<GridlyDashboardWidgetDefinition> DashboardWidgets { get; } = new();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<GridlyAiInsight> AiInsights { get; } = new();

    public void ApplyV32ExperiencePhase(GridlyExperiencePhase phase)
    {
        switch (phase)
        {
            case GridlyExperiencePhase.UxIntelligence:
                EnableUxIntelligence = true;
                RememberLastViewMode = true;
                break;
            case GridlyExperiencePhase.FactoryIntelligence:
                EnableFactoryStatusOverlay = true;
                SetViewMode(GridlyViewMode.HeatMap);
                break;
            case GridlyExperiencePhase.TimelineEngine:
                SetViewMode(GridlyViewMode.Timeline);
                break;
            case GridlyExperiencePhase.DocumentExplorer:
                SetViewMode(GridlyViewMode.Gallery);
                EnableMediaLazyLoading = true;
                EnableMediaImageCache = true;
                break;
            case GridlyExperiencePhase.VirtualizationPro:
                EnableVirtualizationPro = true;
                SetViewMode(GridlyViewMode.DenseList);
                break;
            case GridlyExperiencePhase.SearchEverywhere:
                EnableSearchEverywhere = true;
                EnableModernSearchPanel = true;
                SearchPanelCanFilterResults = true;
                ShowQuickFilterBar = true;
                break;
            case GridlyExperiencePhase.CommandPalette:
                EnableCommandPalette = true;
                break;
            case GridlyExperiencePhase.LayoutStudio:
                EnableLayoutStudio = true;
                break;
            case GridlyExperiencePhase.DashboardBuilder:
                EnableDashboardBuilder = true;
                SetViewMode(GridlyViewMode.KpiDashboard);
                EnsureDefaultDashboardWidgets();
                break;
            case GridlyExperiencePhase.AiLayer:
                EnableAiInsights = true;
                break;
            case GridlyExperiencePhase.Ecosystem:
                EnableCommandPalette = true;
                EnableLayoutStudio = true;
                EnableDashboardBuilder = true;
                EnableSearchEverywhere = true;
                break;
        }

        RefreshView();
    }

    public void ApplyV32UltimateExperiencePack()
    {
        EnableUxIntelligence = true;
        RememberLastViewMode = true;
        EnableFactoryStatusOverlay = true;
        EnableSearchEverywhere = true;
        EnableCommandPalette = true;
        EnableLayoutStudio = true;
        EnableDashboardBuilder = true;
        EnableAiInsights = true;
        EnableVirtualizationPro = true;
        EnableMediaLazyLoading = true;
        EnableMediaImageCache = true;
        EnableModernSearchPanel = true;
                SearchPanelCanFilterResults = true;
                ShowQuickFilterBar = true;
        EnsureDefaultDashboardWidgets();
        RefreshView();
    }

    public string SaveExperienceSnapshot()
    {
        string view = RememberLastViewMode ? _viewMode.ToString() : string.Empty;
        string widgets = string.Join(",", DashboardWidgets.Select(w => w.Key + ":" + w.Kind));
        return "ViewMode=" + view + Environment.NewLine +
               "UxIntelligence=" + EnableUxIntelligence + Environment.NewLine +
               "FactoryOverlay=" + EnableFactoryStatusOverlay + Environment.NewLine +
               "SearchEverywhere=" + EnableSearchEverywhere + Environment.NewLine +
               "CommandPalette=" + EnableCommandPalette + Environment.NewLine +
               "LayoutStudio=" + EnableLayoutStudio + Environment.NewLine +
               "DashboardBuilder=" + EnableDashboardBuilder + Environment.NewLine +
               "AiInsights=" + EnableAiInsights + Environment.NewLine +
               "VirtualizationPro=" + EnableVirtualizationPro + Environment.NewLine +
               "Widgets=" + widgets;
    }

    public IEnumerable<object> SearchEverywhere(string? query)
    {
        if (string.IsNullOrWhiteSpace(query)) return Array.Empty<object>();
        string needle = query.Trim();
        return GetObjects().Where(row => Columns.VisibleColumns.Any(col =>
        {
            string? value = Convert.ToString(col.GetValue(row));
            return !string.IsNullOrEmpty(value) && value.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
        })).ToArray();
    }

    public void RememberCurrentViewModeForActiveScenario()
    {
        if (!RememberViewModePerScenario || _suppressViewModeMemory) return;
        _viewModeMemoryByScenario[ActiveScenario] = ViewMode;
    }

    internal IDisposable SuspendViewModeMemory()
    {
        _suppressViewModeMemory = true;
        return new GridlyViewModeMemoryScope(this);
    }

    private sealed class GridlyViewModeMemoryScope : IDisposable
    {
        private GridlyView? _owner;
        public GridlyViewModeMemoryScope(GridlyView owner) => _owner = owner;
        public void Dispose()
        {
            if (_owner != null) _owner._suppressViewModeMemory = false;
            _owner = null;
        }
    }

    public bool TryRestoreRememberedViewMode(GridlyViewScenario scenario)
    {
        if (!RememberViewModePerScenario) return false;
        if (!_viewModeMemoryByScenario.TryGetValue(scenario, out var remembered)) return false;
        if (ViewMode == remembered) return true;
        SetViewMode(remembered);
        return true;
    }

    public GridlyMachineStatus ResolveFactoryStatus(object row)
    {
        if (FactoryStatusGetter != null) return FactoryStatusGetter(row);
        string? value = null;
        var col = Columns.FirstOrDefault(c => string.Equals(c.AspectName, FactoryStatusAspectName, StringComparison.OrdinalIgnoreCase) || string.Equals(c.Header, FactoryStatusAspectName, StringComparison.OrdinalIgnoreCase));
        if (col != null) value = Convert.ToString(col.GetValue(row));
        if (string.IsNullOrWhiteSpace(value))
        {
            var prop = row.GetType().GetProperty(FactoryStatusAspectName);
            if (prop != null) value = Convert.ToString(prop.GetValue(row));
        }
        return value?.Trim().ToLowerInvariant() switch
        {
            "running" or "online" or "çalışıyor" or "calisiyor" or "ok" => GridlyMachineStatus.Running,
            "waiting" or "bekliyor" or "warning" or "uyarı" => GridlyMachineStatus.Waiting,
            "fault" or "arıza" or "ariza" or "fail" or "error" => GridlyMachineStatus.Fault,
            "offline" or "kapalı" or "kapali" => GridlyMachineStatus.Offline,
            "maintenance" or "bakım" or "bakim" => GridlyMachineStatus.Maintenance,
            _ => GridlyMachineStatus.Unknown
        };
    }

    public void AddAiInsight(string title, string message, string severity = "Info", string suggestedAction = "")
    {
        AiInsights.Add(new GridlyAiInsight { Title = title, Message = message, Severity = severity, SuggestedAction = suggestedAction });
        EnableAiInsights = true;
    }

    public void EnsureDefaultDashboardWidgets()
    {
        if (DashboardWidgets.Count > 0) return;
        DashboardWidgets.Add(new GridlyDashboardWidgetDefinition { Key = "total", Title = "Toplam", Kind = GridlyDashboardWidgetKind.Kpi, AspectName = "Code" });
        DashboardWidgets.Add(new GridlyDashboardWidgetDefinition { Key = "status", Title = "Durum Dağılımı", Kind = GridlyDashboardWidgetKind.HeatMap, AspectName = "Status" });
        DashboardWidgets.Add(new GridlyDashboardWidgetDefinition { Key = "timeline", Title = "Zaman Akışı", Kind = GridlyDashboardWidgetKind.Timeline, AspectName = "Detail" });
    }
    public bool IsDashboardWidgetEnabled(GridlyDashboardWidgetKind kind)
        => DashboardWidgets.Any(w => w.Kind == kind);

    public void SetDashboardWidgetEnabled(GridlyDashboardWidgetKind kind, bool enabled)
    {
        EnableDashboardBuilder = true;

        if (!enabled)
        {
            DashboardWidgets.RemoveAll(w => w.Kind == kind);
            RefreshView();
            return;
        }

        if (!IsDashboardWidgetEnabled(kind))
            DashboardWidgets.Add(CreateDefaultDashboardWidget(kind));

        RefreshView();
    }

    private static GridlyDashboardWidgetDefinition CreateDefaultDashboardWidget(GridlyDashboardWidgetKind kind)
    {
        return kind switch
        {
            GridlyDashboardWidgetKind.Kpi => new GridlyDashboardWidgetDefinition { Key = "kpi", Title = "KPI", Kind = kind, AspectName = "Code" },
            GridlyDashboardWidgetKind.HeatMap => new GridlyDashboardWidgetDefinition { Key = "heat", Title = "HeatMap", Kind = kind, AspectName = "Status" },
            GridlyDashboardWidgetKind.Chart => new GridlyDashboardWidgetDefinition { Key = "chart", Title = "MiniChart", Kind = kind, AspectName = "Progress" },
            GridlyDashboardWidgetKind.Timeline => new GridlyDashboardWidgetDefinition { Key = "timeline", Title = "Timeline", Kind = kind, AspectName = "Detail" },
            GridlyDashboardWidgetKind.Gallery => new GridlyDashboardWidgetDefinition { Key = "gallery", Title = "Gallery", Kind = kind, AspectName = "Image" },
            GridlyDashboardWidgetKind.Table => new GridlyDashboardWidgetDefinition { Key = "table", Title = "Table", Kind = kind, AspectName = "Name" },
            GridlyDashboardWidgetKind.Card => new GridlyDashboardWidgetDefinition { Key = "card", Title = "Card", Kind = kind, AspectName = "Name" },
            _ => new GridlyDashboardWidgetDefinition { Key = kind.ToString().ToLowerInvariant(), Title = kind.ToString(), Kind = kind, AspectName = "Name" }
        };
    }
}
