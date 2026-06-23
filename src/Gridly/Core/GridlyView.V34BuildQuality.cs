using System.Reflection;
using System.Text;

namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - V34 Build Quality")]
    [DefaultValue(true)]
    [Description("Geriye uyumluluk için eski property ve görünüm adlarını koruyan güvenli API yüzeyini aktif tutar.")]
    public bool EnableCompatibilityGuards { get; set; } = true;

    [Category("Gridly - V34 Build Quality")]
    [DefaultValue(true)]
    [Description("Host uygulamalarda temel kolon/view/theme ayarları için hızlı kalite kontrol helperlarını aktif eder.")]
    public bool EnableBuildQualityDiagnostics { get; set; } = true;

    [Category("Gridly - V34 Build Quality")]
    [DefaultValue(true)]
    [Description("Faz paketleri uygulanırken riskli kombinasyonları güvenli varsayılanlara çeker.")]
    public bool AutoRepairRiskyOptions { get; set; } = true;

    public GridlyQualityReport RunBuildQualityDiagnostics()
    {
        var report = new GridlyQualityReport();
        if (!EnableBuildQualityDiagnostics)
        {
            report.Add(GridlyQualitySeverity.Info, "Diagnostics", "DISABLED", "Build Quality Diagnostics kapalı.");
            return report;
        }

        CheckPublicApi(report);
        CheckColumns(report);
        CheckViewMode(report);
        CheckTheme(report);
        CheckMediaOptions(report);
        return report;
    }

    public string RunBuildQualityDiagnosticsText()
    {
        var report = RunBuildQualityDiagnostics();
        var sb = new StringBuilder();
        sb.AppendLine("Gridly v34 Build Quality Report");
        sb.AppendLine("Created: " + report.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("Errors: " + report.ErrorCount + "  Warnings: " + report.WarningCount);
        foreach (var item in report.Items)
        {
            sb.AppendLine(item.ToString());
            if (!string.IsNullOrWhiteSpace(item.Suggestion))
                sb.AppendLine("  -> " + item.Suggestion);
        }
        return sb.ToString();
    }

    public void ApplyV34BuildQualityPack()
    {
        EnableCompatibilityGuards = true;
        EnableBuildQualityDiagnostics = true;
        AutoRepairRiskyOptions = true;
        EnforceThemeAccessibility = true;
        AutoEnsureReadableTextColors = true;
        if (AutoRepairRiskyOptions)
            RepairRiskyOptions();
        RefreshView();
    }

    private void RepairRiskyOptions()
    {
        if (TilePreferredWidth < 120) TilePreferredWidth = 160;
        if (TilePreferredHeight < 120) TilePreferredHeight = 180;
        if (PosterPreferredWidth < 140) PosterPreferredWidth = 180;
        if (PosterPreferredHeight < 180) PosterPreferredHeight = 240;
        if (TilePosterImageHeight < 64) TilePosterImageHeight = 120;
        if (PosterImageHeight < 64) PosterImageHeight = 140;
        if (FilterPopupMinimumSize.Width < 260 || FilterPopupMinimumSize.Height < 220)
            FilterPopupMinimumSize = new Size(360, 320);
        if (FilterPopupDefaultSize.Width < FilterPopupMinimumSize.Width || FilterPopupDefaultSize.Height < FilterPopupMinimumSize.Height)
            FilterPopupDefaultSize = new Size(Math.Max(520, FilterPopupMinimumSize.Width), Math.Max(520, FilterPopupMinimumSize.Height));
    }

    private void CheckPublicApi(GridlyQualityReport report)
    {
        var props = typeof(GridlyView).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var duplicates = props.GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase).Where(g => g.Count() > 1).ToList();
        if (duplicates.Count == 0)
            report.Add(GridlyQualitySeverity.Info, "Public API", "API001", "Runtime public property çakışması görünmüyor.");
        else
            foreach (var d in duplicates)
                report.Add(GridlyQualitySeverity.Error, "Public API", "API_DUP", "Duplicate public property: " + d.Key, "Aynı property sadece tek partial dosyada tanımlanmalı.");
    }

    private void CheckColumns(GridlyQualityReport report)
    {
        if (Columns.Count == 0)
        {
            report.Add(GridlyQualitySeverity.Warning, "Columns", "COL_EMPTY", "Kolon tanımı yok.", "Designer.cs içinde en az bir GridlyColumn tanımlayın.");
            return;
        }

        var duplicateAspects = Columns.Where(c => !string.IsNullOrWhiteSpace(c.AspectName))
            .GroupBy(c => c.AspectName, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .ToList();
        foreach (var d in duplicateAspects)
            report.Add(GridlyQualitySeverity.Warning, "Columns", "COL_DUP_ASPECT", "Aynı AspectName birden fazla kolonda kullanılıyor: " + d.Key);

        report.Add(GridlyQualitySeverity.Info, "Columns", "COL_COUNT", "Kolon sayısı: " + Columns.Count);
    }

    private void CheckViewMode(GridlyQualityReport report)
    {
        report.Add(GridlyQualitySeverity.Info, "ViewMode", "VIEW_CURRENT", "Aktif görünüm: " + ViewMode);
        if ((ViewMode == GridlyViewMode.Poster || ViewMode == GridlyViewMode.Gallery || ViewMode == GridlyViewMode.MediaTile || ViewMode == GridlyViewMode.FilmStrip) && Columns.Count == 0)
            report.Add(GridlyQualitySeverity.Warning, "ViewMode", "VIEW_MEDIA_NO_COLUMN", "Medya görünümü açık ama görsel alabilecek kolon yok.", "ImageGetter olan bir kolon ekleyin veya MediaPlaceholderImage kullanın.");
    }

    private void CheckTheme(GridlyQualityReport report)
    {
        var ratio = Gridly.Theming.GridlyThemeAccessibility.ContrastRatio(_theme.BackColor, _theme.ForeColor);
        if (ratio < 4.5d)
            report.Add(GridlyQualitySeverity.Warning, "Theme", "THEME_CONTRAST", "Ana yazı kontrastı düşük: " + ratio.ToString("0.00"), "EnforceThemeAccessibility=true kullanın.");
        else
            report.Add(GridlyQualitySeverity.Info, "Theme", "THEME_OK", "Ana yazı kontrastı uygun: " + ratio.ToString("0.00"));
    }

    private void CheckMediaOptions(GridlyQualityReport report)
    {
        if (EnableMediaLazyLoading && !EnableMediaImageCache)
            report.Add(GridlyQualitySeverity.Warning, "Media", "MEDIA_CACHE_OFF", "Lazy loading açık ama media image cache kapalı.", "Audix gibi arşivlerde iki ayarı birlikte kullanın.");
        if (ShowMediaQualityBadge && string.IsNullOrWhiteSpace(MediaQualityBadgeAspectName) && MediaQualityBadgeGetter == null)
            report.Add(GridlyQualitySeverity.Warning, "Media", "MEDIA_BADGE_NO_SOURCE", "Medya rozeti açık ama badge kaynağı yok.", "MediaQualityBadgeAspectName veya MediaQualityBadgeGetter tanımlayın.");
    }
}
