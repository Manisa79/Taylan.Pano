// Gridly v25 FeatureSamples.cs
// Bu dosya tek başına proje değildir; WinForms projenize ekleyip çağırabileceğiniz örnek kurulum metotları içerir.
// Namespace ve model adlarını kendi projenize göre değiştirebilirsiniz.

using Gridly.Core;
using Gridly.Columns;
using Gridly.Filtering;
using Gridly.Exporting;
using Gridly.Theming;
using Gridly.Analytics;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Gridly.FeatureSamples;

public sealed class CardVisualDemoRow
{
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public string Priority { get; set; } = "";
    public int Messages { get; set; }
    public bool HasAttachment { get; set; }
    public bool IsPinned { get; set; }
}

public sealed class DemoRow
{
    public bool Checked { get; set; }
    public string Barcode { get; set; } = "";
    public string MachineName { get; set; } = "";
    public string AssemblyName { get; set; } = "";
    public string State { get; set; } = "";
    public int Score { get; set; }
    public int Progress { get; set; }
    public string Description { get; set; } = "";
    public string Url { get; set; } = "";
}

public static class FeatureSamples
{
    public static List<DemoRow> CreateDemoRows(int count = 1000)
    {
        var states = new[] { "OK", "FAIL", "WAIT", "REPAIR" };
        var machines = new[] { "QX250i", "QX500", "Flex", "Flex Ultra" };
        var rows = new List<DemoRow>(count);
        for (int i = 0; i < count; i++)
        {
            rows.Add(new DemoRow
            {
                Checked = i % 5 == 0,
                Barcode = "BC" + i.ToString("000000"),
                MachineName = machines[i % machines.Length],
                AssemblyName = "BOARD-" + (i % 42).ToString("000"),
                State = states[i % states.Length],
                Score = i % 100,
                Progress = i % 101,
                Description = "Açıklama " + i,
                Url = "https://example.com/" + i
            });
        }
        return rows;
    }

    public static GridlyView CreateBasicAspectNameExample()
    {
        var grid = CreateBaseGrid();
        grid.Columns.Add(new GridlyColumn("Barkod", "Barcode", 160));
        grid.Columns.Add(new GridlyColumn("Makine", "MachineName", 140));
        grid.Columns.Add(new GridlyColumn { HeaderText = "Durum", FieldName = "State", Width = 100 });
        grid.SetObjects(CreateDemoRows(100));
        return grid;
    }

    public static GridlyView CreateGLVMigrationExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.ClearObjects();
        grid.AddObjects(CreateDemoRows(25));
        grid.TextFilter = "BC00001";
        grid.CheckAll();
        grid.UncheckAll();
        return grid;
    }

    public static GridlyView CreateAspectGetterPutterExample()
    {
        var grid = CreateBaseGrid();
        grid.Columns.Add(new GridlyColumn("Seç", "Checked", 70)
        {
            Kind = GridlyColumnKind.CheckBox,
            AspectGetter = row => ((DemoRow)row).Checked,
            AspectPutter = (row, value) => ((DemoRow)row).Checked = Convert.ToBoolean(value)
        });
        grid.Columns.Add(new GridlyColumn("Özet", "", 260)
        {
            AspectGetter = row =>
            {
                var r = (DemoRow)row;
                return $"{r.Barcode} / {r.MachineName} / {r.State}";
            }
        });
        grid.SetObjects(CreateDemoRows(100));
        return grid;
    }

    public static GridlyView CreateFilterHighlightExample(TextBox searchBox)
    {
        var grid = CreateBasicAspectNameExample();
        grid.EnableHighlightEngine = true;
        grid.HighlightGlobalFilterText = true;
        grid.FastFilterMenuForHugeLists = true;
        grid.AsyncLoadFullFilterValues = true;
        grid.TypedFilterSearchesAllRows = true;
        searchBox.TextChanged += (_, _) =>
        {
            grid.SetGlobalFilter(searchBox.Text);
            if (!string.IsNullOrWhiteSpace(searchBox.Text))
                grid.JumpToFirstMatch(searchBox.Text);
        };
        return grid;
    }

    public static GridlyView CreateProgressButtonHyperlinkExample()
    {
        var grid = CreateBaseGrid();
        grid.EnableModernProgressBar = true;
        grid.ProgressBarShowText = true;
        grid.ProgressBarUseGradient = true;
        grid.Columns.Add(new GridlyColumn("Barkod", "Barcode", 150));
        grid.Columns.Add(new GridlyColumn("Progress", "Progress", 130) { Kind = GridlyColumnKind.ProgressBar });
        grid.Columns.Add(new GridlyColumn("Aç", "", 80) { Kind = GridlyColumnKind.Button, AspectGetter = _ => "Aç" });
        grid.Columns.Add(new GridlyColumn("Link", "Url", 180) { Kind = GridlyColumnKind.Hyperlink });
        grid.ButtonClick += (_, e) => MessageBox.Show($"Buton: {((DemoRow)e.RowObject).Barcode}");
        grid.HyperlinkClick += (_, e) => MessageBox.Show($"Link: {((DemoRow)e.RowObject).Url}");
        grid.SetObjects(CreateDemoRows(100));
        return grid;
    }

    public static GridlyView CreateEditingExample()
    {
        var grid = CreateBaseGrid();
        grid.EnableCellEditing = true;
        grid.CellEditActivationOnDoubleClick = true;
        grid.CellEditActivationKey = Keys.F2;
        grid.Columns.Add(new GridlyColumn("Barkod", "Barcode", 150));
        grid.Columns.Add(new GridlyColumn("Açıklama", "Description", 260)
        {
            Editable = true,
            AspectPutter = (row, value) => ((DemoRow)row).Description = Convert.ToString(value) ?? ""
        });
        grid.CellValueChanged += (_, e) => MessageBox.Show($"Güncellendi: {e.Column.Header} = {e.NewValue}");
        grid.SetObjects(CreateDemoRows(100));
        return grid;
    }

    public static GridlyView CreateGroupingExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.EnableGrouping = true;
        grid.SetGroupBy("MachineName");
        grid.AllowGroupCollapse = true;
        grid.DrawGroupCollapseGlyph = true;
        return grid;
    }

    public static GridlyView CreateLayoutManagerExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.AllowColumnReorder = true;
        grid.EnableColumnAutoResizeOnDoubleClick = true;
        grid.AutoSaveColumnLayout = true;
        grid.ColumnLayoutStorageKey = "Gridly.FeatureSamples.Layout";
        grid.SaveColumnLayoutProfile("Default");
        return grid;
    }

    public static GridlyView CreateViewModesExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.TileMinWidth = 220;
        grid.TilePreferredHeight = 90;
        grid.SetViewMode(GridlyViewMode.Tile);
        return grid;
    }


    public static GridlyView CreateCardVisualAdornmentsExample()
    {
        var grid = CreateBaseGrid();
        grid.SetViewMode(GridlyViewMode.DashboardCard);
        grid.TilePreferredWidth = 340;
        grid.TilePreferredHeight = 150;
        grid.CardVisualAdornments = true;
        grid.CardDefaultAccentMode = GridlyCardAccentMode.TopBar;

        grid.Columns.Add(new GridlyColumn("Başlık", nameof(CardVisualDemoRow.Title), 220));
        grid.Columns.Add(new GridlyColumn("Durum", nameof(CardVisualDemoRow.Status), 110));
        grid.Columns.Add(new GridlyColumn("Öncelik", nameof(CardVisualDemoRow.Priority), 90) { Kind = GridlyColumnKind.Badge });
        grid.Columns.Add(new GridlyColumn("Mesaj", nameof(CardVisualDemoRow.Messages), 80));

        grid.CardVisualInfoGetter = row =>
        {
            var r = (CardVisualDemoRow)row;
            Color statusColor = r.Status switch
            {
                "Yeni" => Color.FromArgb(240, 190, 55),
                "Bakılıyor" => Color.FromArgb(54, 158, 245),
                "Tamamlandı" => Color.FromArgb(74, 190, 118),
                _ => Color.FromArgb(112, 122, 135)
            };

            var info = new GridlyCardVisualInfo
            {
                AccentColor = statusColor,
                DotColor = statusColor,
                AccentMode = GridlyCardAccentMode.TopBar
            };

            if (r.Messages > 0)
            {
                info.Badges.Add(new GridlyCardBadge
                {
                    Text = r.Messages.ToString(),
                    Glyph = GridlyCardGlyph.Message,
                    BackColor = Color.FromArgb(245, 158, 11),
                    Placement = GridlyCardBadgePlacement.TopRight
                });
            }

            if (r.HasAttachment)
            {
                info.Badges.Add(new GridlyCardBadge
                {
                    Glyph = GridlyCardGlyph.Attachment,
                    BackColor = Color.FromArgb(99, 102, 241),
                    Placement = GridlyCardBadgePlacement.BottomRight
                });
            }

            if (r.IsPinned)
            {
                info.Badges.Add(new GridlyCardBadge
                {
                    Glyph = GridlyCardGlyph.Pin,
                    BackColor = Color.FromArgb(100, 116, 139),
                    Placement = GridlyCardBadgePlacement.TopLeft
                });
            }

            return info;
        };

        grid.SetObjects(new List<CardVisualDemoRow>
        {
            new() { Title = "AOI durdu", Status = "Yeni", Priority = "Acil", Messages = 3, HasAttachment = true, IsPinned = true },
            new() { Title = "Program isteği", Status = "Bakılıyor", Priority = "Normal", Messages = 1 },
            new() { Title = "False call kontrol", Status = "Tamamlandı", Priority = "Düşük", Messages = 0, HasAttachment = true },
            new() { Title = "Kapatılmış kayıt", Status = "Kapalı", Priority = "Arşiv", Messages = 0 }
        });

        return grid;
    }

    public static GridlyView CreateThemeExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.FollowWindowsTheme = true;
        grid.EnableAnimatedSelection = true;
        grid.EnableRoundedCells = true;
        grid.EnableSoftShadows = true;
        grid.ApplyThemePreset(GridlyThemePreset.Dark);
        return grid;
    }

    public static GridlyView CreateRowColorExample()
    {
        var grid = CreateBasicAspectNameExample();
        grid.RowColorAspectName = "State";
        grid.RowColorStrength = 0.18;
        grid.RowBackColorGetter = row => ((DemoRow)row).State == "FAIL" ? Color.FromArgb(50, Color.Red) : null;
        return grid;
    }

    public static void ExportVisibleRows(GridlyView grid, string folder)
    {
        Directory.CreateDirectory(folder);
        var rows = grid.GetVisibleObjects();
        GridlyExporter.SaveCsv(Path.Combine(folder, "gridly.csv"), grid.Columns, rows);
        GridlyExporter.SaveJson(Path.Combine(folder, "gridly.json"), grid.Columns, rows);
        GridlyExporter.SaveExcelWorkbook(Path.Combine(folder, "gridly.xlsx"), grid.Columns, rows);
        GridlyExporter.SavePdf(Path.Combine(folder, "gridly.pdf"), grid.Columns, rows, "Gridly Export");
    }

    public static void ConfigurePrint(GridlyView grid)
    {
        grid.PrintTitle = "Gridly v25 Print";
        grid.PrintOnlyVisibleColumns = true;
        grid.PrintSelectedRowsOnly = false;
        grid.PrintMaxRows = 5000;
        grid.PrintFitToPageWidth = true;
        grid.PrintShowGrid = true;
        grid.PrintZebraRows = true;
    }

    public static void ShowMiniAnalytics(GridlyView grid)
    {
        var stateColumn = grid.Columns["State"];
        if (stateColumn == null) return;
        var analytics = GridlyColumnAnalytics.From(stateColumn, grid.GetVisibleObjects());
        MessageBox.Show($"Satır: {analytics.RowCount}\nDistinct: {analytics.DistinctCount}\nBoş: {analytics.BlankCount}");
    }

    private static GridlyView CreateBaseGrid()
    {
        return new GridlyView
        {
            Dock = DockStyle.Fill,
            FullRowSelect = true,
            MultiSelect = true,
            ShowHeader = true,
            ShowGridLines = true,
            EmptyListMessage = "Kayıt bulunamadı",
            EnableModernEmptyState = true,
            EnableIncrementalSearch = true,
            EnableClipboard = true
        };
    }
}
