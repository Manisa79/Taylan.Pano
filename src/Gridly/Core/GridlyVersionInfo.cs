using System.Reflection;

namespace Gridly.Core;

/// <summary>
/// Product/version helper for host apps, test apps and diagnostics screens.
/// </summary>
public static class GridlyVersionInfo
{
    public const string ProjectDisplayName = "Gridly";
    public const string ControlName = "GridlyView";
    public const string PackageId = "Gridly.WinForms";
    public const string SuggestedProjectName = ProjectDisplayName;
    public const string Version = "1.0.52.2";
    public const string BuildName = "1.0.52.2-community-preview-media-scenarios-compile-cleanup";

    public static string AssemblyVersion
        => typeof(GridlyVersionInfo).Assembly.GetName().Version?.ToString() ?? Version;

    public static string InformationalVersion
        => typeof(GridlyVersionInfo).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? BuildName;

    public static string DisplayText => $"{ProjectDisplayName} {InformationalVersion}";
}
