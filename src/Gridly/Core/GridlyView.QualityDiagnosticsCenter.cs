using System.ComponentModel;
using System.Text;
using Gridly.Columns;

namespace Gridly.Core;

public sealed class GridlyDiagnosticsCenterItem
{
    public GridlyQualitySeverity Severity { get; set; } = GridlyQualitySeverity.Info;
    public string Area { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Suggestion { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Severity} | {Area} | {Code} | {Message}";
    }
}

public sealed class GridlyDiagnosticsCenterReport
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<GridlyDiagnosticsCenterItem> Items { get; } = new();
    public int ErrorCount => Items.Count(x => x.Severity == GridlyQualitySeverity.Error);
    public int WarningCount => Items.Count(x => x.Severity == GridlyQualitySeverity.Warning);
    public bool IsClean => ErrorCount == 0 && WarningCount == 0;

    public void Add(GridlyQualitySeverity severity, string area, string code, string message, string suggestion = "")
    {
        Items.Add(new GridlyDiagnosticsCenterItem
        {
            Severity = severity,
            Area = area,
            Code = code,
            Message = message,
            Suggestion = suggestion
        });
    }
}

public partial class GridlyView
{
    [Category("Gridly - Diagnostics Center")]
    [DefaultValue(true)]
    [Description("Build quality, runtime hardening, real usage, performance, media ve dashboard kontrollerini tek raporda toplar.")]
    public bool EnableGridlyDiagnosticsCenter { get; set; } = true;

    public GridlyDiagnosticsCenterReport RunGridlyDiagnosticsCenter()
    {
        var report = new GridlyDiagnosticsCenterReport();
        if (!EnableGridlyDiagnosticsCenter)
        {
            report.Add(GridlyQualitySeverity.Info, "Diagnostics", "CENTER_DISABLED", "Gridly Diagnostics Center kapali.");
            return report;
        }

        AppendBuildQualityDiagnostics(report);
        AppendRuntimeDiagnostics(report);
        AppendRealUsageDiagnostics(report);
        AppendViewModeDiagnostics(report);
        AppendMediaDiagnostics(report);
        AppendDashboardDiagnostics(report);
        AppendPerformanceDiagnostics(report);
        return report;
    }

    public string RunGridlyDiagnosticsCenterText()
    {
        var report = RunGridlyDiagnosticsCenter();
        var sb = new StringBuilder();
        sb.AppendLine("Gridly Diagnostics Center");
        sb.AppendLine("Created: " + report.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("Errors: " + report.ErrorCount + "  Warnings: " + report.WarningCount);
        sb.AppendLine();

        foreach (var item in report.Items)
        {
            sb.AppendLine(item.ToString());
            if (!string.IsNullOrWhiteSpace(item.Suggestion))
                sb.AppendLine("  -> " + item.Suggestion);
        }

        sb.AppendLine();
        sb.AppendLine(GetViewModeDecisionGuideText());
        return sb.ToString();
    }

    public void ShowGridlyDiagnosticsCenter()
    {
        string text = RunGridlyDiagnosticsCenterText();
        var form = new Form
        {
            Text = "Gridly Diagnostics Center",
            StartPosition = FormStartPosition.CenterParent,
            Size = new Size(920, 680),
            MinimizeBox = false
        };

        var editor = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Both,
            WordWrap = false,
            Font = new Font("Consolas", 9.5f),
            Text = text
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 46,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(8)
        };

        var close = new Button { Text = "Kapat", Width = 96 };
        close.Click += (_, __) => form.Close();
        var copy = new Button { Text = "Kopyala", Width = 96 };
        copy.Click += (_, __) => Clipboard.SetText(editor.Text);
        var refresh = new Button { Text = "Yenile", Width = 96 };
        refresh.Click += (_, __) => editor.Text = RunGridlyDiagnosticsCenterText();

        buttons.Controls.Add(close);
        buttons.Controls.Add(copy);
        buttons.Controls.Add(refresh);
        form.Controls.Add(editor);
        form.Controls.Add(buttons);

        var owner = FindForm();
        if (owner == null)
            form.Show();
        else
            form.Show(owner);
    }

    public string GetViewModeDecisionGuideText()
    {
        return "View Mode Decision Guide" + Environment.NewLine +
               "- Details/DenseList: buyuk veri, klasik tablo, hizli filtre ve satir tarama." + Environment.NewLine +
               "- RowCard: satiri kart gibi gosterir; aksiyon, rozet ve kisa metin icin iyi." + Environment.NewLine +
               "- RowPreview: RowCard'a benzer ama daha kompakt onizleme gibi davranir; uzun listede daha az yer kaplar." + Environment.NewLine +
               "- DetailCard: secili/veri satirini okunabilir detay karti gibi acar; media varsa kapak/poster paneli ekler." + Environment.NewLine +
               "- PropertyCard: DetailCard'in daha form/property okuma odakli hali; etiket-deger duzeni belirgindir." + Environment.NewLine +
               "- MediaTile/Poster/Gallery/FilmStrip: muzik, film, fotograf ve dokuman kapak/thumbnail senaryolari." + Environment.NewLine +
               "- KpiDashboard/HeatMap/MiniChart: ozet metrik, fabrika durumu ve trend odakli dashboard yuzeyi.";
    }

    private void AppendBuildQualityDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        foreach (var item in RunBuildQualityDiagnostics().Items)
            report.Add(item.Severity, item.Area, item.Code, item.Message, item.Suggestion);
    }

    private void AppendRuntimeDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        foreach (var item in RunGridly5RuntimeChecks())
            report.Add(item.Passed ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Runtime", item.CheckName, item.Message);

        foreach (var item in RunGridly502RuntimeHardeningChecks())
            report.Add(item.Passed ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, item.Area, item.Check, item.Message);
    }

    private void AppendRealUsageDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        foreach (var item in RunGridly51UsageChecks())
            report.Add(item.Passed ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, item.Profile, item.Check, item.Message);
    }

    private void AppendViewModeDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        report.Add(GridlyQualitySeverity.Info, "ViewMode", "CURRENT", "Aktif gorunum: " + ViewMode, GetViewModeDecisionGuideText().Replace(Environment.NewLine, " "));
        report.Add(RememberViewModePerScenario ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "ViewMode", "MEMORY", RememberViewModePerScenario ? "Senaryo bazli gorunum hafizasi aktif." : "Senaryo bazli gorunum hafizasi kapali.", "Runtime menusu > Gorunum > Senaryoya gore gorunumu hatirla.");

        bool hasImageSource = Columns.VisibleColumns.Any(c =>
            c.Kind == GridlyColumnKind.Image ||
            c.Kind == GridlyColumnKind.Icon ||
            c.ImageGetter != null ||
            c.StateImageGetter != null ||
            c.Image != null ||
            !string.IsNullOrWhiteSpace(c.ImageAspectName));

        if ((ViewMode == GridlyViewMode.DetailCard || ViewMode == GridlyViewMode.PropertyCard || ViewMode == GridlyViewMode.MediaTile || ViewMode == GridlyViewMode.Poster) && !hasImageSource)
            report.Add(GridlyQualitySeverity.Warning, "ViewMode", "IMAGE_SOURCE", "Gorsel agirlikli gorunum acik ama otomatik image kolonu bulunamadi.", "Kind=Image/Icon olan bir kolon, ImageGetter veya bitmap donen AspectName ekleyin.");
    }

    private void AppendMediaDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        report.Add(GridlyQualitySeverity.Info, "Media", "SMART_PRESET", "Aktif media smart preset: " + MediaSmartPreset);
        report.Add(EnableMediaImageCache ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Media", "IMAGE_CACHE", EnableMediaImageCache ? "Image cache aktif. Limit: " + MediaMemoryCacheLimit : "Image cache kapali.", "Buyuk medya listelerinde ApplyMediaSmartPreset veya ApplyV38PerformanceProfile(MediaLibrary) kullanin.");
        report.Add(EnableMediaLazyLoading ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Media", "LAZY_IMAGE", EnableMediaLazyLoading ? "Lazy image resolve aktif." : "Lazy image resolve kapali.", "Audix/film/fotograf kataloglarinda lazy resolve acik olmali.");
    }

    private void AppendDashboardDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        report.Add(EnableDashboardBuilder ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Dashboard", "BUILDER", EnableDashboardBuilder ? "Dashboard builder aktif." : "Dashboard builder kapali.", "Sag tik > Gorunum > Dashboard preset editor ile KPI/HeatMap/MiniChart acilabilir.");
        report.Add(DashboardWidgets.Count > 0 ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Dashboard", "WIDGETS", "Dashboard widget sayisi: " + DashboardWidgets.Count, "EnsureDefaultDashboardWidgets veya SetDashboardWidgetEnabled kullanin.");
        report.Add(IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.Kpi) ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Dashboard", "KPI", IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.Kpi) ? "KPI widget aktif." : "KPI widget kapali.");
        report.Add(IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.HeatMap) ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Dashboard", "HEATMAP", IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.HeatMap) ? "HeatMap widget aktif." : "HeatMap widget kapali.");
        report.Add(IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.Chart) ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Dashboard", "MINICHART", IsDashboardWidgetEnabled(GridlyDashboardWidgetKind.Chart) ? "MiniChart widget aktif." : "MiniChart widget kapali.");
    }

    private void AppendPerformanceDiagnostics(GridlyDiagnosticsCenterReport report)
    {
        report.Add(EnablePaintPerformanceMetrics ? GridlyQualitySeverity.Info : GridlyQualitySeverity.Warning, "Performance", "PAINT_METRICS", EnablePaintPerformanceMetrics ? GetPerformanceSummary() : "Paint olcumu kapali.", "Sag tik > Gorunum > Akilli presetler > Performance: medya cache.");

        if (ViewCount >= 1000 && !EnableMediaImageCache && (ViewMode == GridlyViewMode.MediaTile || ViewMode == GridlyViewMode.Poster || ViewMode == GridlyViewMode.Gallery || ViewMode == GridlyViewMode.FilmStrip))
            report.Add(GridlyQualitySeverity.Warning, "Performance", "MEDIA_BIG_LIST", "Buyuk medya listesinde cache kapali.", "Image cache + lazy resolve birlikte acilmali.");
    }
}
