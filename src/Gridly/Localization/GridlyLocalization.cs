using System.Globalization;

namespace Gridly.Localization;

/// <summary>
/// Application-wide localization facade for Gridly. Set this once at application startup
/// and every GridlyView / built-in dialog / built-in menu will use the same language.
/// </summary>
public static class GridlyLocalization
{
    public static event EventHandler? LanguageChanged;

    public static GridlyLanguage Language
    {
        get => GridlyText.Language;
        set
        {
            if (GridlyText.Language == value) return;
            GridlyText.Language = value;
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public static GridlyLanguage EffectiveLanguage => GridlyText.EffectiveLanguage;

    public static IReadOnlyList<GridlyLanguage> SupportedLanguages { get; } = new[]
    {
        GridlyLanguage.Auto,
        GridlyLanguage.Turkish,
        GridlyLanguage.English,
        GridlyLanguage.German,
        GridlyLanguage.French,
        GridlyLanguage.Spanish,
        GridlyLanguage.Italian,
        GridlyLanguage.Russian,
        GridlyLanguage.Arabic,
        GridlyLanguage.Chinese,
        GridlyLanguage.Japanese
    };

    public static void Use(GridlyLanguage language) => Language = language;

    public static string T(string key) => GridlyText.T(key);

    public static string DisplayName(GridlyLanguage language)
    {
        return language switch
        {
            GridlyLanguage.Auto => "Auto / System",
            GridlyLanguage.Turkish => "Türkçe",
            GridlyLanguage.English => "English",
            GridlyLanguage.German => "Deutsch",
            GridlyLanguage.French => "Français",
            GridlyLanguage.Spanish => "Español",
            GridlyLanguage.Italian => "Italiano",
            GridlyLanguage.Russian => "Русский",
            GridlyLanguage.Arabic => "العربية",
            GridlyLanguage.Chinese => "中文",
            GridlyLanguage.Japanese => "日本語",
            _ => language.ToString()
        };
    }

    public static GridlyLanguage FromName(string? value, GridlyLanguage fallback = GridlyLanguage.Auto)
    {
        if (string.IsNullOrWhiteSpace(value)) return fallback;
        if (Enum.TryParse<GridlyLanguage>(value, ignoreCase: true, out var parsed)) return parsed;

        string normalized = value.Trim().ToLowerInvariant();
        return normalized switch
        {
            "tr" or "turkish" or "türkçe" => GridlyLanguage.Turkish,
            "en" or "english" => GridlyLanguage.English,
            "de" or "german" or "deutsch" => GridlyLanguage.German,
            "fr" or "french" or "français" => GridlyLanguage.French,
            "es" or "spanish" or "español" => GridlyLanguage.Spanish,
            "it" or "italian" or "italiano" => GridlyLanguage.Italian,
            "ru" or "russian" => GridlyLanguage.Russian,
            "ar" or "arabic" => GridlyLanguage.Arabic,
            "zh" or "chinese" => GridlyLanguage.Chinese,
            "ja" or "japanese" => GridlyLanguage.Japanese,
            _ => fallback
        };
    }

    public static void UseCurrentUICulture()
    {
        Language = GridlyLanguage.Auto;
        _ = CultureInfo.CurrentUICulture;
    }
}
