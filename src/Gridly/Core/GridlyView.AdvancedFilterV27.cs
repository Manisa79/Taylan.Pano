using System.ComponentModel;
using System.Text.Json;
using Gridly.Filtering;

namespace Gridly.Core;

/// <summary>
/// v27.4 Advanced Filter & Preset Engine.
/// Multi-column filter builder, AND/OR logic and reusable filter presets.
/// </summary>
public partial class GridlyView
{
    [Category("Gridly - v27.4 Advanced Filter")]
    [DefaultValue(true)]
    [Description("Başlık/gövde menülerinde gelişmiş filtre ve filtre preset komutlarını gösterir.")]
    public bool ShowAdvancedFilterMenuItems { get; set; } = true;

    [Category("Gridly - v27.4 Advanced Filter")]
    [DefaultValue(true)]
    [Description("Gelişmiş filtre presetlerini kaydetme/yükleme özelliklerini etkinleştirir.")]
    public bool EnableFilterPresets { get; set; } = true;

    [Category("Gridly - v27.4 Advanced Filter")]
    [DefaultValue(GridlyFilterLogic.And)]
    [Description("Kolon filtrelerinin varsayılan birleşim mantığı. AND klasik davranıştır; OR gelişmiş filtre ekranında kullanılabilir.")]
    public GridlyFilterLogic AdvancedFilterLogic
    {
        get => _filters.Logic;
        set
        {
            _filters.Logic = value;
            BuildViewIndex();
            Invalidate();
        }
    }

    [Category("Gridly - v27.4 Advanced Filter")]
    [DefaultValue("")]
    [Description("Filtre presetlerinin saklanacağı klasör. Boşsa LocalAppData altında Gridly/FilterPresets kullanılır.")]
    public string FilterPresetFolder { get; set; } = string.Empty;

    [Category("Gridly - v27.4 Advanced Filter")]
    [DefaultValue("")]
    [Description("Son yüklenen/kaydedilen filtre preset adı.")]
    public string ActiveFilterPresetName { get; private set; } = string.Empty;

    public void ShowAdvancedFilterBuilder()
    {
        using var form = new GridlyAdvancedFilterBuilderForm(Columns, CreateFilterPresetSnapshot(ActiveFilterPresetName), _theme);
        if (form.ShowDialog(FindForm()) == DialogResult.OK)
        {
            ApplyFilterPreset(form.Preset);
            QueueAutoSaveUserLayout();
        }
    }

    public GridlyFilterPreset CreateFilterPresetSnapshot(string name = "")
    {
        return new GridlyFilterPreset
        {
            Name = string.IsNullOrWhiteSpace(name) ? ActiveFilterPresetName : name,
            Description = "Gridly filter preset",
            Logic = _filters.Logic,
            GlobalText = _filters.GlobalText,
            Filters = _filters.Filters.Select(x => new GridlyColumnFilterPresetItem
            {
                AspectName = x.AspectName,
                Mode = x.Mode,
                Text = x.Text,
                Text2 = x.Text2,
                Enabled = x.Enabled,
                SelectedValues = x.SelectedValues == null ? null : x.SelectedValues.OrderBy(v => v).ToList()
            }).ToList()
        };
    }

    public void ApplyFilterPreset(GridlyFilterPreset? preset)
    {
        if (preset == null) return;
        _filters.Clear();
        _filters.GlobalText = preset.GlobalText ?? string.Empty;
        _filters.Logic = preset.Logic;

        if (preset.Filters != null)
        {
            foreach (var item in preset.Filters)
            {
                if (item == null || !item.Enabled || string.IsNullOrWhiteSpace(item.AspectName)) continue;
                _filters.Set(new GridlyColumnFilter
                {
                    AspectName = item.AspectName,
                    Mode = item.Mode,
                    Text = item.Text,
                    Text2 = item.Text2,
                    Enabled = item.Enabled,
                    SelectedValues = item.SelectedValues == null ? null : new HashSet<string>(item.SelectedValues)
                });
            }
        }

        ActiveFilterPresetName = preset.Name ?? string.Empty;
        BuildViewIndex();
        Invalidate();
        QueueAutoSaveUserLayout();
    }

    public void SaveCurrentFilterPreset(string presetName)
    {
        if (string.IsNullOrWhiteSpace(presetName))
            throw new ArgumentException("Filtre preset adı boş olamaz.", nameof(presetName));

        var preset = CreateFilterPresetSnapshot(presetName.Trim());
        SaveFilterPreset(preset);
    }

    public void SaveFilterPreset(GridlyFilterPreset preset)
    {
        if (preset == null) throw new ArgumentNullException(nameof(preset));
        if (string.IsNullOrWhiteSpace(preset.Name))
            preset.Name = string.IsNullOrWhiteSpace(Name) ? "GridlyFilterPreset" : Name;

        string path = GetFilterPresetPath(preset.Name);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var json = JsonSerializer.Serialize(preset, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
        ActiveFilterPresetName = preset.Name;
    }

    public bool LoadFilterPreset(string presetName)
    {
        if (string.IsNullOrWhiteSpace(presetName)) return false;

        // v28.13 platform presetleri bellekte tutulur; eski v27.4 presetleri dosyadan okunur.
        // Tek public API kalsın diye önce yeni platform presetine, sonra dosya tabanlı eski yapıya bakıyoruz.
        if (TryLoadPlatformFilterPreset(presetName))
            return true;

        string path = GetFilterPresetPath(presetName);
        if (!File.Exists(path)) return false;
        var preset = JsonSerializer.Deserialize<GridlyFilterPreset>(File.ReadAllText(path));
        if (preset == null) return false;
        ApplyFilterPreset(preset);
        return true;
    }

    public IReadOnlyList<string> GetFilterPresetNames()
    {
        string folder = GetFilterPresetFolder();
        if (!Directory.Exists(folder)) return Array.Empty<string>();
        return Directory.EnumerateFiles(folder, "*.json")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase)
            .ToList()!;
    }

    public void ApplyBuiltInFilterPreset(string presetName)
    {
        if (string.IsNullOrWhiteSpace(presetName)) return;
        string key = presetName.Trim().ToUpperInvariant();
        var preset = new GridlyFilterPreset { Name = presetName, Logic = GridlyFilterLogic.And };

        if (key.Contains("OPEN") || key.Contains("AÇIK"))
            preset.Filters.Add(new GridlyColumnFilterPresetItem { AspectName = "Status", Mode = GridlyFilterMode.Contains, Text = "Open" });
        else if (key.Contains("EKSİK") || key.Contains("MISSING"))
            preset.Filters.Add(new GridlyColumnFilterPresetItem { AspectName = "Status", Mode = GridlyFilterMode.Contains, Text = "Eksik" });
        else if (key.Contains("WAIT"))
            preset.Filters.Add(new GridlyColumnFilterPresetItem { AspectName = "Status", Mode = GridlyFilterMode.Contains, Text = "Waiting" });
        else if (key.Contains("SAP"))
            preset.Filters.Add(new GridlyColumnFilterPresetItem { AspectName = "Type", Mode = GridlyFilterMode.Contains, Text = "SAP" });
        else if (key.Contains("BOM"))
            preset.Filters.Add(new GridlyColumnFilterPresetItem { AspectName = "Type", Mode = GridlyFilterMode.Contains, Text = "BOM" });
        else
            preset.GlobalText = presetName;

        ApplyFilterPreset(preset);
    }

    private void AddAdvancedFilterMenu(ToolStripItemCollection items)
    {
        if (!ShowAdvancedFilterMenuItems) return;
        var menu = new ToolStripMenuItem("Gelişmiş Filtre");
        menu.DropDownItems.Add("Filtre Oluşturucu...", null, (_, _) => ShowAdvancedFilterBuilder());
        var andItem = new ToolStripMenuItem("AND mantığı") { Checked = AdvancedFilterLogic == GridlyFilterLogic.And };
        andItem.Click += (_, _) => AdvancedFilterLogic = GridlyFilterLogic.And;
        var orItem = new ToolStripMenuItem("OR mantığı") { Checked = AdvancedFilterLogic == GridlyFilterLogic.Or };
        orItem.Click += (_, _) => AdvancedFilterLogic = GridlyFilterLogic.Or;
        menu.DropDownItems.Add(andItem);
        menu.DropDownItems.Add(orItem);
        menu.DropDownItems.Add(new ToolStripSeparator());
        menu.DropDownItems.Add("Preset olarak kaydet...", null, (_, _) => SaveFilterPresetWithDialog()).Enabled = EnableFilterPresets;

        var presetMenu = new ToolStripMenuItem("Preset yükle");
        foreach (string presetName in GetFilterPresetNames())
        {
            string captured = presetName;
            presetMenu.DropDownItems.Add(captured, null, (_, _) => LoadFilterPreset(captured));
        }
        presetMenu.Enabled = EnableFilterPresets && presetMenu.DropDownItems.Count > 0;
        menu.DropDownItems.Add(presetMenu);

        var quick = new ToolStripMenuItem("Hazır hızlı filtreler");
        string[] builtIns = { "Open Tickets", "Waiting", "Eksik BOM", "SAP Satırları", "BOM Satırları" };
        foreach (string name in builtIns)
            quick.DropDownItems.Add(name, null, (_, _) => ApplyBuiltInFilterPreset(name));
        menu.DropDownItems.Add(quick);

        menu.DropDownItems.Add(new ToolStripSeparator());
        menu.DropDownItems.Add("Filtreleri temizle", null, (_, _) => ClearFilters()).Enabled = HasActiveFilters;
        items.Add(menu);
    }

    private void SaveFilterPresetWithDialog()
    {
        string? presetName = GridlyPresetNameDialog.ShowDialog(FindForm(), "Filtre preset adı", string.IsNullOrWhiteSpace(ActiveFilterPresetName) ? "GridlyFilter" : ActiveFilterPresetName);
        if (!string.IsNullOrWhiteSpace(presetName))
            SaveCurrentFilterPreset(presetName);
    }

    private string GetFilterPresetFolder()
    {
        if (!string.IsNullOrWhiteSpace(FilterPresetFolder))
            return FilterPresetFolder;

        string appName = Application.ProductName;
        if (string.IsNullOrWhiteSpace(appName)) appName = "Gridly";
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName, "Gridly", "FilterPresets");
    }

    private string GetFilterPresetPath(string presetName)
    {
        string folder = GetFilterPresetFolder();
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, MakeSafeFileName(presetName) + ".json");
    }
}
