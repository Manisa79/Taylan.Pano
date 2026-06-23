using System.ComponentModel;
using System.Drawing;

namespace Gridly.Core;

public enum GridlyMenuIconTheme
{
    Auto,
    Light,
    Dark
}

[Flags]
public enum GridlyMenuItemKeys
{
    None = 0,
    FilterPopup = 1 << 0,
    FilterWindow = 1 << 1,
    ClearColumnFilter = 1 << 2,
    ClearAllFilters = 1 << 3,
    AdvancedFilter = 1 << 4,
    FilterPreset = 1 << 5,
    SortAscending = 1 << 6,
    SortDescending = 1 << 7,
    ClearSort = 1 << 8,
    FreezeColumn = 1 << 9,
    AutoSizeColumn = 1 << 10,
    AutoSizeAllColumns = 1 << 11,
    LayoutSaveLoad = 1 << 12,
    Grouping = 1 << 13,
    ColumnChooser = 1 << 14,
    ViewMode = 1 << 15,
    Theme = 1 << 16,
    State = 1 << 17,
    Scenario = 1 << 18,
    Clipboard = 1 << 19,
    Editing = 1 << 20,
    RowDetails = 1 << 21,
    Analytics = 1 << 22,
    ExportPrint = 1 << 23,
    All = FilterPopup | FilterWindow | ClearColumnFilter | ClearAllFilters | AdvancedFilter | FilterPreset | SortAscending | SortDescending | ClearSort | FreezeColumn | AutoSizeColumn | AutoSizeAllColumns | LayoutSaveLoad | Grouping | ColumnChooser | ViewMode | Theme | State | Scenario | Clipboard | Editing | RowDetails | Analytics | ExportPrint
}

[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class GridlyMenuCustomizationOptions
{
    private readonly GridlyView _owner;
    internal GridlyMenuCustomizationOptions(GridlyView owner) => _owner = owner;

    [DefaultValue(true)] public bool ShowBuiltInHeaderMenu { get => _owner.UseBuiltInHeaderMenu; set => _owner.UseBuiltInHeaderMenu = value; }
    [DefaultValue(true)] public bool ShowBuiltInBodyMenu { get => _owner.UseBuiltInBodyMenu; set => _owner.UseBuiltInBodyMenu = value; }
    [DefaultValue(true)] public bool MergeWithUserContextMenu { get => _owner.MergeBuiltInMenuWithUserContextMenu; set => _owner.MergeBuiltInMenuWithUserContextMenu = value; }

    [DefaultValue(GridlyMenuGroups.All)] public GridlyMenuGroups HeaderGroups { get => _owner.HeaderMenuGroups; set { _owner.HeaderMenuGroups = value; _owner.MenuProfile = GridlyMenuProfile.Custom; } }
    [DefaultValue(GridlyMenuGroups.All)] public GridlyMenuGroups BodyGroups { get => _owner.BodyMenuGroups; set { _owner.BodyMenuGroups = value; _owner.MenuProfile = GridlyMenuProfile.Custom; } }
    [DefaultValue(GridlyMenuGroups.All)] public GridlyMenuGroups MergedGroups { get => _owner.MergedMenuGroups; set { _owner.MergedMenuGroups = value; _owner.MenuProfile = GridlyMenuProfile.Custom; } }

    [DefaultValue(GridlyMenuItemKeys.None)] public GridlyMenuItemKeys HiddenItems { get => _owner.HiddenMenuItems; set => _owner.HiddenMenuItems = value; }
    [DefaultValue(GridlyMenuItemKeys.All)] public GridlyMenuItemKeys VisibleItems { get => _owner.VisibleMenuItems; set => _owner.VisibleMenuItems = value; }

    public override string ToString() => "Menu customization";

    public void ApplyFullProfile()
    {
        _owner.MenuProfile = GridlyMenuProfile.Full;
        HeaderGroups = GridlyMenuGroups.All;
        BodyGroups = GridlyMenuGroups.All;
        MergedGroups = GridlyMenuGroups.All;
        HiddenItems = GridlyMenuItemKeys.None;
        VisibleItems = GridlyMenuItemKeys.All;
    }

    public void ApplyMinimalProfile()
    {
        _owner.MenuProfile = GridlyMenuProfile.Custom;
        HeaderGroups = GridlyMenuGroups.Filter | GridlyMenuGroups.Sort | GridlyMenuGroups.ColumnChooser;
        BodyGroups = GridlyMenuGroups.Clipboard | GridlyMenuGroups.Filter;
        MergedGroups = HeaderGroups | BodyGroups;
    }

    public void ApplyReadOnlyProfile()
    {
        _owner.MenuProfile = GridlyMenuProfile.ReadOnly;
        HiddenItems |= GridlyMenuItemKeys.Editing;
    }
}

[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class GridlyMenuIconCustomizationOptions
{
    private readonly GridlyView _owner;
    internal GridlyMenuIconCustomizationOptions(GridlyView owner) => _owner = owner;

    [DefaultValue(GridlyMenuIconMode.BuiltInThenCustom)]
    public GridlyMenuIconMode Mode { get => _owner.MenuIconMode; set { _owner.MenuIconMode = value; _owner.ClearMenuIconCache(); } }

    [DefaultValue(GridlyMenuIconSize.Small16)]
    public GridlyMenuIconSize Size { get => _owner.MenuIconSize; set { _owner.MenuIconSize = value; _owner.ClearMenuIconCache(); } }

    [DefaultValue(GridlyMenuIconTheme.Auto)]
    public GridlyMenuIconTheme ThemeMode { get => _owner.MenuIconThemeMode; set { _owner.MenuIconThemeMode = value; _owner.ClearMenuIconCache(); } }

    [DefaultValue("")]
    public string CustomFolder { get => _owner.CustomMenuIconFolder; set => _owner.SetCustomMenuIconFolder(value, saveLayout: false); }

    [DefaultValue(null)]
    public ImageList? ImageList { get => _owner.CustomMenuImageList; set { _owner.CustomMenuImageList = value; _owner.ClearMenuIconCache(); } }

    [DefaultValue(null)]
    public ImageList? DarkImageList { get => _owner.CustomMenuDarkImageList; set { _owner.CustomMenuDarkImageList = value; _owner.ClearMenuIconCache(); } }

    [DefaultValue(null)]
    public ImageList? LightImageList { get => _owner.CustomMenuLightImageList; set { _owner.CustomMenuLightImageList = value; _owner.ClearMenuIconCache(); } }

    public override string ToString() => "Icon customization";
}

public partial class GridlyView
{
    private readonly GridlyMenuCustomizationOptions _menuOptions;
    private readonly GridlyMenuIconCustomizationOptions _menuIcons;

    [Category("Gridly - Context Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("v27.5: Header/body/merged menü gruplarını ve item bazlı görünürlüğü tek yerden yönetir.")]
    public GridlyMenuCustomizationOptions MenuOptions => _menuOptions;

    [Category("Gridly - Menu Icons")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("v27.5: Built-in veya kullanıcı ikonlarını folder/ImageList/light-dark ayrımıyla yönetir.")]
    public GridlyMenuIconCustomizationOptions MenuIcons => _menuIcons;

    [Category("Gridly - Context Menu")]
    [DefaultValue(GridlyMenuItemKeys.All)]
    [Description("v27.5: Menü item bazlı izin listesi. All varsayılan; örn. AdvancedFilter çıkarılabilir.")]
    public GridlyMenuItemKeys VisibleMenuItems { get; set; } = GridlyMenuItemKeys.All;

    [Category("Gridly - Context Menu")]
    [DefaultValue(GridlyMenuItemKeys.None)]
    [Description("v27.5: Menü item bazlı gizleme listesi. VisibleMenuItems ile birlikte değerlendirilir.")]
    public GridlyMenuItemKeys HiddenMenuItems { get; set; } = GridlyMenuItemKeys.None;

    [Category("Gridly - Menu Icons")]
    [DefaultValue(null)]
    [Description("v27.5: Menü ikonları için ImageList. Key isimleri: filter, sort_asc, advanced_filter, state, scenario, export...")]
    public ImageList? CustomMenuImageList { get; set; }

    [Category("Gridly - Menu Icons")]
    [DefaultValue(null)]
    public ImageList? CustomMenuDarkImageList { get; set; }

    [Category("Gridly - Menu Icons")]
    [DefaultValue(null)]
    public ImageList? CustomMenuLightImageList { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("v27.5: SVG veya özel ikon kaynakları için kullanıcı resolver. Key: filter, sort_asc, advanced_filter vb.")]
    public Func<string, Image?>? CustomMenuIconResolver { get; set; }

    [Category("Gridly - Menu Icons")]
    [DefaultValue(GridlyMenuIconTheme.Auto)]
    public GridlyMenuIconTheme MenuIconThemeMode { get; set; } = GridlyMenuIconTheme.Auto;

    public void SetMenuItemVisible(GridlyMenuItemKeys item, bool visible)
    {
        if (visible)
        {
            VisibleMenuItems |= item;
            HiddenMenuItems &= ~item;
        }
        else
        {
            HiddenMenuItems |= item;
        }
        MenuProfile = GridlyMenuProfile.Custom;
    }

    public void SetMenuGroupVisible(GridlyMenuGroups group, bool visible, bool header = true, bool body = true, bool merged = true)
    {
        ShowMenuGroup(group, visible, header, body);
        if (merged) MergedMenuGroups = visible ? MergedMenuGroups | group : MergedMenuGroups & ~group;
    }

    public void SetCustomMenuIconImageList(ImageList? imageList, ImageList? darkImageList = null, ImageList? lightImageList = null)
    {
        CustomMenuImageList = imageList;
        CustomMenuDarkImageList = darkImageList;
        CustomMenuLightImageList = lightImageList;
        MenuIconMode = GridlyMenuIconMode.BuiltInThenCustom;
        ClearMenuIconCache();
    }

    private bool IsMenuItemVisibleV275(GridlyMenuItemKeys item)
    {
        if (item == GridlyMenuItemKeys.None) return true;
        if ((HiddenMenuItems & item) != 0) return false;
        return (VisibleMenuItems & item) != 0;
    }

    private void ApplyMenuCustomizationV275(ContextMenuStrip menu)
    {
        ApplyMenuCustomizationV275(menu.Items);
        RemoveRedundantMenuSeparators(menu);
    }

    private void ApplyMenuCustomizationV275(ToolStripItemCollection items)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            ToolStripItem item = items[i];
            if (item is ToolStripMenuItem mi)
            {
                ApplyMenuCustomizationV275(mi.DropDownItems);
                GridlyMenuItemKeys key = ResolveMenuItemKeyV275(mi.Text);
                if (!IsMenuItemVisibleV275(key) || (mi.DropDownItems.Count == 0 && key == GridlyMenuItemKeys.None && mi.Text.StartsWith("__", StringComparison.Ordinal)))
                {
                    items.RemoveAt(i);
                    try { item.Dispose(); } catch { }
                }
            }
        }
        RemoveRedundantMenuSeparators(items);
    }

    private static GridlyMenuItemKeys ResolveMenuItemKeyV275(string? text)
    {
        string t = (text ?? string.Empty).ToLowerInvariant();
        if (t.Contains("gelişmiş filtre") || t.Contains("advanced filter")) return GridlyMenuItemKeys.AdvancedFilter;
        if (t.Contains("preset")) return GridlyMenuItemKeys.FilterPreset;
        if (t.Contains("ayrı filtre") || t.Contains("filter window") || t.Contains("pencere")) return GridlyMenuItemKeys.FilterWindow;
        if (t.Contains("filtre") || t.Contains("filter"))
        {
            if (t.Contains("temiz") || t.Contains("clear")) return t.Contains("kolon") || t.Contains("column") ? GridlyMenuItemKeys.ClearColumnFilter : GridlyMenuItemKeys.ClearAllFilters;
            return GridlyMenuItemKeys.FilterPopup;
        }
        if (t.Contains("artan") || t.Contains("ascending") || t.Contains("a-z")) return GridlyMenuItemKeys.SortAscending;
        if (t.Contains("azalan") || t.Contains("descending") || t.Contains("z-a")) return GridlyMenuItemKeys.SortDescending;
        if (t.Contains("sıralamayı temiz") || t.Contains("clear sort") || t.Contains("unsort")) return GridlyMenuItemKeys.ClearSort;
        if (t.Contains("sabitle") || t.Contains("frozen") || t.Contains("freeze")) return GridlyMenuItemKeys.FreezeColumn;
        if (t.Contains("bu kolonu sığdır") || t.Contains("auto size column")) return GridlyMenuItemKeys.AutoSizeColumn;
        if (t.Contains("sığdır") || t.Contains("width") || t.Contains("genişlik")) return GridlyMenuItemKeys.AutoSizeAllColumns;
        if (t.Contains("layout") || t.Contains("düzen") || t.Contains("profil")) return GridlyMenuItemKeys.LayoutSaveLoad;
        if (t.Contains("grup") || t.Contains("group")) return GridlyMenuItemKeys.Grouping;
        if (t.Contains("kolon") || t.Contains("column")) return GridlyMenuItemKeys.ColumnChooser;
        if (t.Contains("görün") || t.Contains("view")) return GridlyMenuItemKeys.ViewMode;
        if (t.Contains("tema") || t.Contains("theme")) return GridlyMenuItemKeys.Theme;
        if (t.Contains("state") || t.Contains("durum kaydet")) return GridlyMenuItemKeys.State;
        if (t.Contains("senaryo") || t.Contains("scenario")) return GridlyMenuItemKeys.Scenario;
        if (t.Contains("kopya") || t.Contains("copy") || t.Contains("paste") || t.Contains("yapıştır")) return GridlyMenuItemKeys.Clipboard;
        if (t.Contains("düzenle") || t.Contains("edit")) return GridlyMenuItemKeys.Editing;
        if (t.Contains("detay") || t.Contains("detail")) return GridlyMenuItemKeys.RowDetails;
        if (t.Contains("analiz") || t.Contains("analytics")) return GridlyMenuItemKeys.Analytics;
        if (t.Contains("export") || t.Contains("dışa aktar") || t.Contains("yazdır") || t.Contains("print")) return GridlyMenuItemKeys.ExportPrint;
        return GridlyMenuItemKeys.None;
    }
}
