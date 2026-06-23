using System.ComponentModel;

namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - V40 Visual Analytics")]
    [DefaultValue(false)]
    [Description("KPI, HeatMap, MiniChart, Timeline ve dashboard builder akışlarını tek analiz profili altında açar.")]
    public bool EnableVisualAnalyticsPro { get; set; }

    [Category("Gridly - V40 Visual Analytics")]
    [DefaultValue("Status")]
    public string AnalyticsStatusAspectName { get; set; } = "Status";

    [Category("Gridly - V40 Visual Analytics")]
    [DefaultValue("Progress")]
    public string AnalyticsValueAspectName { get; set; } = "Progress";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Gridly - V40 Visual Analytics")]
    public List<GridlyAnalyticsMetric> AnalyticsMetrics { get; } = new();

    public void ApplyV40AnalyticsProfile(GridlyV40AnalyticsPreset preset)
    {
        EnableVisualAnalyticsPro = true;
        EnableDashboardBuilder = true;
        EnsureDefaultDashboardWidgets();
        EnsureDefaultAnalyticsMetrics();

        switch (preset)
        {
            case GridlyV40AnalyticsPreset.KpiDashboard:
                SetViewMode(GridlyViewMode.KpiDashboard);
                break;
            case GridlyV40AnalyticsPreset.HeatMap:
                SetViewMode(GridlyViewMode.HeatMap);
                break;
            case GridlyV40AnalyticsPreset.Timeline:
                SetViewMode(GridlyViewMode.Timeline);
                break;
            case GridlyV40AnalyticsPreset.MiniCharts:
                SetViewMode(GridlyViewMode.MiniChart);
                break;
            case GridlyV40AnalyticsPreset.FactoryOverview:
                EnableFactoryStatusOverlay = true;
                SetViewMode(GridlyViewMode.HeatMap);
                break;
        }

        RefreshView();
    }

    public void EnsureDefaultAnalyticsMetrics()
    {
        if (AnalyticsMetrics.Count > 0) return;
        AnalyticsMetrics.Add(new GridlyAnalyticsMetric { Key = "open", Title = "Açık İş", Value = "24", Subtitle = "Bugün", Status = "Warning", NumberValue = 24 });
        AnalyticsMetrics.Add(new GridlyAnalyticsMetric { Key = "done", Title = "Tamamlandı", Value = "128", Subtitle = "Son 24 saat", Status = "Success", NumberValue = 128 });
        AnalyticsMetrics.Add(new GridlyAnalyticsMetric { Key = "risk", Title = "Risk", Value = "7", Subtitle = "İzlenmeli", Status = "Danger", NumberValue = 7 });
        AnalyticsMetrics.Add(new GridlyAnalyticsMetric { Key = "perf", Title = "Performans", Value = "%92", Subtitle = "Ortalama", Status = "Info", NumberValue = 92 });
    }

    public string CreateAnalyticsSummaryText()
    {
        EnsureDefaultAnalyticsMetrics();
        return string.Join(Environment.NewLine, AnalyticsMetrics.Select(m => $"{m.Title}: {m.Value} - {m.Subtitle}"));
    }
}
