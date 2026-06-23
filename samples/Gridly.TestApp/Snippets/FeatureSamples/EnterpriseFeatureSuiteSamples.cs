using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gridly.Columns;
using Gridly.Core;
using Gridly.Layout;

namespace Gridly.FeatureSamples;

public sealed class EnterpriseDemoRow
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Machine { get; set; } = string.Empty;
    public int Progress { get; set; }
    public int Messages { get; set; }
    public bool Locked { get; set; }
}

public static class EnterpriseFeatureSuiteSamples
{
    public static GridlyView CreateEnterpriseDashboardSample()
    {
        GridlyView grid = new GridlyView
        {
            Dock = DockStyle.Fill,
            ViewMode = GridlyViewMode.DashboardCard,
            CardVisualAdornments = true,
            CardDefaultAccentMode = GridlyCardAccentMode.TopBar,
            UltraFastMode = true,
            FuzzySearchEnabled = true,
            ColumnQualifiedSearchEnabled = true,
            LiveUpdateMode = GridlyLiveUpdateMode.Off,
            PersistColumnFilters = false,
            UserLayoutKey = "Gridly.Samples.EnterpriseDashboard"
        };

        grid.Columns.Add(new GridlyColumn("Id", nameof(EnterpriseDemoRow.Id), 70)
        {
            Frozen = true,
            Aggregate = GridlyAggregateMode.Count,
            AutoFilterMode = GridlyAutoFilterMode.Number,
            SearchAlias = "id"
        });
        grid.Columns.Add(new GridlyColumn("Ad", nameof(EnterpriseDemoRow.Name), 180)
        {
            FillFreeSpace = true,
            AutoFilterMode = GridlyAutoFilterMode.Text,
            SearchAlias = "name"
        });
        grid.Columns.Add(new GridlyColumn("Durum", nameof(EnterpriseDemoRow.Status), 110)
        {
            Kind = GridlyColumnKind.Badge,
            AutoFilterMode = GridlyAutoFilterMode.ValueList,
            ShowTopValuesInFilter = true,
            SearchAlias = "status"
        });
        grid.Columns.Add(new GridlyColumn("Makine", nameof(EnterpriseDemoRow.Machine), 130)
        {
            AutoFilterMode = GridlyAutoFilterMode.Text,
            SearchAlias = "machine"
        });
        grid.Columns.Add(new GridlyColumn("İlerleme", nameof(EnterpriseDemoRow.Progress), 90)
        {
            Kind = GridlyColumnKind.ProgressBar,
            Aggregate = GridlyAggregateMode.Average,
            AutoFilterMode = GridlyAutoFilterMode.Number,
            SearchAlias = "progress"
        });

        grid.CardVisualInfoGetter = rowObj =>
        {
            EnterpriseDemoRow row = (EnterpriseDemoRow)rowObj;
            Color color = row.Status switch
            {
                "Yeni" => Color.Goldenrod,
                "Bakılıyor" => Color.DodgerBlue,
                "Tamamlandı" => Color.SeaGreen,
                "Kapalı" => Color.Gray,
                _ => Color.SlateGray
            };

            GridlyCardVisualInfo info = new GridlyCardVisualInfo
            {
                AccentColor = color,
                DotColor = color,
                AccentMode = GridlyCardAccentMode.TopBar
            };

            if (row.Messages > 0)
            {
                info.Badges.Add(new GridlyCardBadge
                {
                    Text = row.Messages.ToString(),
                    Glyph = GridlyCardGlyph.Message,
                    BackColor = Color.Orange,
                    Placement = GridlyCardBadgePlacement.TopRight
                });
            }

            if (row.Locked)
            {
                info.Badges.Add(new GridlyCardBadge
                {
                    Glyph = GridlyCardGlyph.Lock,
                    BackColor = Color.DimGray,
                    Placement = GridlyCardBadgePlacement.TopLeft
                });
            }

            info.Actions.Add(new GridlyCardAction
            {
                Key = "view",
                Glyph = GridlyCardGlyph.Info,
                ToolTipText = "Detay aç",
                Placement = GridlyCardActionPlacement.BottomRight
            });

            info.Actions.Add(new GridlyCardAction
            {
                Key = "pin",
                Glyph = GridlyCardGlyph.Pin,
                ToolTipText = "Sabitle",
                Placement = GridlyCardActionPlacement.BottomRight
            });

            return info;
        };

        grid.CardActionClick += (sender, e) =>
        {
            MessageBox.Show($"{e.Action.Key} -> {((EnterpriseDemoRow)e.RowObject).Name}", "Gridly Card Action");
        };

        grid.AddConditionalRule(new GridlyConditionalRule
        {
            Name = "Gecikmiş / düşük ilerleme",
            Priority = 10,
            Condition = (row, column) => row is EnterpriseDemoRow r && r.Progress < 30,
            BackColor = Color.FromArgb(70, Color.OrangeRed),
            ForeColor = Color.White
        });

        grid.SetObjects(CreateRows());
        return grid;
    }

    public static void ConfigureProfiles(GridlyView grid)
    {
        GridlyLayoutProfile technician = grid.CaptureLayoutProfile("Technician");
        technician.DisplayName = "Teknisyen görünümü";
        grid.ApplyLayoutProfile(technician);
        grid.SaveLayoutProfile("Technician");
    }

    private static List<EnterpriseDemoRow> CreateRows()
    {
        string[] statuses = { "Yeni", "Bakılıyor", "Tamamlandı", "Kapalı" };
        List<EnterpriseDemoRow> rows = new List<EnterpriseDemoRow>();
        for (int i = 1; i <= 250; i++)
        {
            rows.Add(new EnterpriseDemoRow
            {
                Id = i,
                Name = "AOI kayıt " + i,
                Status = statuses[i % statuses.Length],
                Machine = "LINE" + (10 + i % 35) + "REW",
                Progress = (i * 7) % 101,
                Messages = i % 9 == 0 ? i % 5 + 1 : 0,
                Locked = i % 17 == 0
            });
        }
        return rows;
    }
}
