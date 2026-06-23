using Gridly.Theming;

namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - V35 Theme Studio")]
    [DefaultValue(false)]
    [Description("Theme Studio preset ve canlı önizleme akışını aktif eder.")]
    public bool EnableThemeStudio { get; set; }

    [Category("Gridly - V35 Theme Studio")]
    [DefaultValue(GridlyThemeStudioPreset.FactoryOsDark)]
    [Description("Gridly Theme Studio içinde seçili olan hazır tema paleti.")]
    public GridlyThemeStudioPreset ThemeStudioPreset { get; set; } = GridlyThemeStudioPreset.FactoryOsDark;

    [Category("Gridly - V35 Theme Studio")]
    [DefaultValue(true)]
    [Description("Theme Studio preset uygulanırken v33 erişilebilirlik normalizasyonunu zorunlu tutar.")]
    public bool ThemeStudioEnforceAccessibility { get; set; } = true;

    public void ApplyThemeStudioPreset(GridlyThemeStudioPreset preset)
    {
        EnableThemeStudio = true;
        ThemeStudioPreset = preset;
        var theme = GridlyThemeStudio.Create(preset);
        if (ThemeStudioEnforceAccessibility)
            theme = GridlyThemeAccessibility.Normalize(theme);
        ApplyTheme(theme);
    }

    public IReadOnlyList<GridlyThemeStudioPalette> GetThemeStudioPalettes()
    {
        return GridlyThemeStudio.BuiltInPalettes();
    }

    public string ExportCurrentThemeStudioPalette(string name = "Current")
    {
        return GridlyThemeStudio.ExportPaletteText(_theme, name);
    }

    public void ApplyV35ThemeStudioPack(GridlyThemeStudioPreset preset = GridlyThemeStudioPreset.FactoryOsDark)
    {
        EnableThemeStudio = true;
        ThemeStudioEnforceAccessibility = true;
        EnforceThemeAccessibility = true;
        AutoEnsureReadableTextColors = true;
        ApplyThemeStudioPreset(preset);
    }
}
