using System.Text.Json.Serialization;

namespace Gridly.Filtering;

public sealed class GridlyFilterPreset
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GridlyFilterLogic Logic { get; set; } = GridlyFilterLogic.And;
    public string GlobalText { get; set; } = string.Empty;
    public List<GridlyColumnFilterPresetItem> Filters { get; set; } = new();
}

public sealed class GridlyColumnFilterPresetItem
{
    public string AspectName { get; set; } = string.Empty;
    public GridlyFilterMode Mode { get; set; } = GridlyFilterMode.Contains;
    public string? Text { get; set; }
    public string? Text2 { get; set; }
    public bool Enabled { get; set; } = true;
    public List<string>? SelectedValues { get; set; }

    [JsonIgnore]
    public string DisplayText
    {
        get
        {
            string op = Mode.ToString();
            if (Mode == GridlyFilterMode.ValueList)
                return $"{AspectName} {op} ({SelectedValues?.Count ?? 0} değer)";
            if (Mode == GridlyFilterMode.Between)
                return $"{AspectName} {op} {Text} - {Text2}";
            return $"{AspectName} {op} {Text}";
        }
    }
}
