using Gridly;
using Gridly.Columns;
using Gridly.Core;

namespace Gridly.FeatureSamples;

public static class GLVColumnChooserProfileSamples
{
    public static FastGridlyView CreateColumnChooserProfileGrid()
    {
        var grid = new FastGridlyView
        {
            Dock = DockStyle.Fill,
            AutoLoadUserLayout = true,
            AutoSaveUserLayout = true,
            UserLayoutKey = "Samples.ColumnChooser.ProfileGrid",

            // Kullanıcı ister popup menüden, ister ayrı pencereden, ister ikisini birlikte kullanabilir.
            ColumnChooserMenuMode = GridlyColumnChooserMenuMode.Both,
            ShowColumnChooserInHeaderMenu = true,
            ShowColumnChooserWindowInHeaderMenu = true,
            AutoSaveLayoutOnColumnVisibilityChange = true
        };

        grid.Columns.Add(new GLVColumn { Header = "Barkod", AspectName = "Barcode", Width = 160 });
        grid.Columns.Add(new GLVColumn { Header = "Makine", AspectName = "Machine", Width = 140 });
        grid.Columns.Add(new GLVColumn { Header = "Sonuç", AspectName = "Result", Width = 100 });
        grid.Columns.Add(new GLVColumn { Header = "Operatör", AspectName = "Operator", Width = 140 });
        grid.Columns.Add(new GLVColumn { Header = "Teknik Gizli", AspectName = "InternalNote", Width = 180, AllowColumnChooser = true });

        // Bu kolon kullanıcı tarafından kolon seçicide gizlenemez/gösterilemez.
        grid.Columns[0].AllowColumnChooser = false;

        grid.SetObjects(Enumerable.Range(1, 500).Select(i => new
        {
            Barcode = "BC" + i.ToString("000000"),
            Machine = "AOI-" + (i % 4 + 1),
            Result = i % 2 == 0 ? "PASS" : "FAIL",
            Operator = "OP" + (i % 8 + 1),
            InternalNote = "Not " + i
        }).ToList());

        return grid;
    }
}
