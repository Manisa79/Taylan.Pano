namespace Gridly.Core;

/// <summary>
/// v34 Build Quality / Stability tarama sonuç seviyesi.
/// </summary>
public enum GridlyQualitySeverity
{
    Info,
    Warning,
    Error
}

/// <summary>
/// Gridly public API ve runtime davranışlarını hızlı kontrol etmek için hafif kalite maddesi.
/// </summary>
public sealed class GridlyQualityCheckItem
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

public sealed class GridlyQualityReport
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<GridlyQualityCheckItem> Items { get; } = new();
    public int ErrorCount => Items.Count(x => x.Severity == GridlyQualitySeverity.Error);
    public int WarningCount => Items.Count(x => x.Severity == GridlyQualitySeverity.Warning);
    public bool IsClean => ErrorCount == 0;

    public void Add(GridlyQualitySeverity severity, string area, string code, string message, string suggestion = "")
    {
        Items.Add(new GridlyQualityCheckItem
        {
            Severity = severity,
            Area = area,
            Code = code,
            Message = message,
            Suggestion = suggestion
        });
    }
}
