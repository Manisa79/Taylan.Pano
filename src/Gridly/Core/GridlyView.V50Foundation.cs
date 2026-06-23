using System.ComponentModel;

namespace Gridly.Core;

[Flags]
public enum GridlyFeatureModule
{
    Core = 1,
    Media = 2,
    Analytics = 4,
    Kanban = 8,
    Timeline = 16,
    Dashboard = 32,
    PropertyGrid = 64,
    Explorer = 128,
    ThemeStudio = 256,
    Interaction = 512,
    LayoutStudio = 1024,
    Accessibility = 2048,
    All = Core | Media | Analytics | Kanban | Timeline | Dashboard | PropertyGrid | Explorer | ThemeStudio | Interaction | LayoutStudio | Accessibility
}

public sealed class GridlyModuleProfileInfo
{
    public string Name { get; set; } = string.Empty;
    public GridlyFeatureModule Modules { get; set; } = GridlyFeatureModule.Core;
    public string Description { get; set; } = string.Empty;
}

public sealed class GridlyStabilityCheckResult
{
    public string CheckName { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public string Message { get; set; } = string.Empty;
}

public partial class GridlyView
{
    [Category("Gridly 5.0 - Modules")]
    [DefaultValue(GridlyFeatureModule.All)]
    [Description("Gridly 5.0 modüler kullanım profili. Audix, AOI, FactoryOS gibi uygulamalarda sadece gereken deneyim paketlerini aktif etmeyi kolaylaştırır.")]
    public GridlyFeatureModule EnabledFeatureModules { get; set; } = GridlyFeatureModule.All;

    [Category("Gridly 5.0 - Stability")]
    [DefaultValue(true)]
    [Description("Yeni faz özellikleri açıkken erişilebilir tema, güvenli medya durumu ve örnek merkez uyumluluğu gibi koruma ayarlarını otomatik uygular.")]
    public bool EnableGridly5SafetyDefaults { get; set; } = true;

    public void ApplyGridly5FoundationDefaults()
    {
        EnabledFeatureModules = GridlyFeatureModule.All;
        EnableGridly5SafetyDefaults = true;
        EnforceThemeAccessibility = true;
        UseUnifiedThemeVisuals = true;
        AutoApplyThemeToColumnHeaders = true;
        AutoApplyThemeToContextMenus = true;
        ThemeStudioEnforceAccessibility = true;
        ShowMediaPlaybackState = true;
        ShowMediaNowPlayingBadge = true;
        ShowMediaEqualizerIndicator = true;
        ShowMediaOverlayButton = true;
        EnableMediaPro = true;
        EnableEnterpriseLayout = true;
        EnableSearchEverywhere = true;
        EnableLayoutStudio = true;
        EnableDashboardBuilder = true;
        EnableThemeStudio = true;
        EnableCommandPalette = true;
        Invalidate();
    }

    public void ApplyAudixMediaProfile()
    {
        EnabledFeatureModules = GridlyFeatureModule.Core | GridlyFeatureModule.Media | GridlyFeatureModule.Explorer | GridlyFeatureModule.ThemeStudio | GridlyFeatureModule.Interaction | GridlyFeatureModule.LayoutStudio | GridlyFeatureModule.Accessibility;
        ApplyV36MediaProPack();
        ShowMediaPlaybackState = true;
        ShowMediaNowPlayingBadge = true;
        ShowMediaEqualizerIndicator = true;
        ShowMediaOverlayButton = true;
        MediaVideoPreviewMode = true;
        MediaImageRoundedCorners = true;
        MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
        EnableThemeStudio = true;
        ThemeStudioEnforceAccessibility = true;
        EnableCommandPalette = true;
        EnableSearchEverywhere = true;
        Invalidate();
    }

    public void ApplyFactoryIntelligenceProfile()
    {
        EnabledFeatureModules = GridlyFeatureModule.Core | GridlyFeatureModule.Analytics | GridlyFeatureModule.Kanban | GridlyFeatureModule.Timeline | GridlyFeatureModule.Dashboard | GridlyFeatureModule.ThemeStudio | GridlyFeatureModule.Accessibility;
        EnableDashboardBuilder = true;
        EnableVisualAnalyticsPro = true;
        EnableThemeStudio = true;
        ThemeStudioEnforceAccessibility = true;
        EnforceThemeAccessibility = true;
        Invalidate();
    }

    public void ApplyAoiSupportDeskProfile()
    {
        EnabledFeatureModules = GridlyFeatureModule.Core | GridlyFeatureModule.Kanban | GridlyFeatureModule.Timeline | GridlyFeatureModule.Dashboard | GridlyFeatureModule.Interaction | GridlyFeatureModule.ThemeStudio | GridlyFeatureModule.Accessibility;
        EnableSearchEverywhere = true;
        EnableCommandPalette = true;
        EnableThemeStudio = true;
        ThemeStudioEnforceAccessibility = true;
        EnforceThemeAccessibility = true;
        Invalidate();
    }

    public IReadOnlyList<GridlyModuleProfileInfo> GetGridly5ModuleProfiles()
    {
        return new List<GridlyModuleProfileInfo>
        {
            new GridlyModuleProfileInfo { Name = "Audix Media", Modules = GridlyFeatureModule.Core | GridlyFeatureModule.Media | GridlyFeatureModule.Explorer | GridlyFeatureModule.Interaction | GridlyFeatureModule.ThemeStudio | GridlyFeatureModule.Accessibility, Description = "Albüm kapağı, video thumbnail, play/pause state, FilmStrip, Poster, Gallery ve medya cache odaklı profil." },
            new GridlyModuleProfileInfo { Name = "AOI Support Desk", Modules = GridlyFeatureModule.Core | GridlyFeatureModule.Kanban | GridlyFeatureModule.Timeline | GridlyFeatureModule.Dashboard | GridlyFeatureModule.Interaction | GridlyFeatureModule.Accessibility, Description = "Ticket kartları, Kanban, Timeline, SLA rozetleri, hızlı arama ve koyu/açık tema okunurluğu." },
            new GridlyModuleProfileInfo { Name = "Factory Intelligence", Modules = GridlyFeatureModule.Core | GridlyFeatureModule.Analytics | GridlyFeatureModule.Dashboard | GridlyFeatureModule.Timeline | GridlyFeatureModule.ThemeStudio | GridlyFeatureModule.Accessibility, Description = "HeatMap, KPI, mini chart, üretim durumu ve FactoryOS dashboard akışı." },
            new GridlyModuleProfileInfo { Name = "MasterData", Modules = GridlyFeatureModule.Core | GridlyFeatureModule.PropertyGrid | GridlyFeatureModule.LayoutStudio | GridlyFeatureModule.Interaction | GridlyFeatureModule.Accessibility, Description = "Kolon hafızası, property card, detay satırları, filtre presetleri ve stabil grid kullanımı." },
            new GridlyModuleProfileInfo { Name = "Bilge Defter", Modules = GridlyFeatureModule.Core | GridlyFeatureModule.Media | GridlyFeatureModule.Dashboard | GridlyFeatureModule.LayoutStudio | GridlyFeatureModule.Accessibility, Description = "Öğrenci fotoğrafları, Gallery/Card görünümü, ödeme/yoklama dashboard ve çıktı hazırlığı." }
        };
    }

    public IReadOnlyList<GridlyStabilityCheckResult> RunGridly5RuntimeChecks()
    {
        var result = new List<GridlyStabilityCheckResult>();
        result.Add(new GridlyStabilityCheckResult { CheckName = "Theme Accessibility", Passed = EnforceThemeAccessibility && ThemeStudioEnforceAccessibility, Message = EnforceThemeAccessibility && ThemeStudioEnforceAccessibility ? "Okunurluk koruması aktif." : "Koyu/açık tema okunurluk koruması kapalı." });
        result.Add(new GridlyStabilityCheckResult { CheckName = "Media Playback", Passed = ShowMediaPlaybackState, Message = ShowMediaPlaybackState ? "Audio/video playback state görünür." : "Play tuşu çalışsa bile kart state'i görünmeyebilir." });
        result.Add(new GridlyStabilityCheckResult { CheckName = "Command Palette", Passed = EnableCommandPalette, Message = EnableCommandPalette ? "Ctrl+K hızlı komut akışı aktif." : "Command Palette kapalı." });
        result.Add(new GridlyStabilityCheckResult { CheckName = "Module Profile", Passed = EnabledFeatureModules != 0, Message = EnabledFeatureModules == 0 ? "Hiç modül seçili değil." : EnabledFeatureModules.ToString() });
        return result;
    }
}
