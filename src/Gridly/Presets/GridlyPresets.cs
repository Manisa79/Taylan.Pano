using Gridly.Columns;
using Gridly.Core;
using Gridly.Editing;
using Gridly.Formatting;

namespace Gridly.Presets;

public static class GridlyPresets
{
    public static void ApplyAoiFailList(GridlyView grid)
    {
        grid.Columns.Clear();
        grid.Columns.Add(new GridlyColumn("Id", "TestResultId", 70) { ReadOnly = true });
        grid.Columns.Add(new GridlyColumn("Barkod", "Barcode", 150) { FillFreeSpace = true, Editable = true, MaxLength = 60 });
        grid.Columns.Add(new GridlyColumn("Makine", "MachineName", 120));
        grid.Columns.Add(new GridlyColumn("Program", "AssemblyName", 220) { FillFreeSpace = true });
        grid.Columns.Add(new GridlyColumn("AOI", "AoiResult", 80) { Kind = GridlyColumnKind.Badge });
        grid.Columns.Add(new GridlyColumn("AI", "AIResult", 80) { Kind = GridlyColumnKind.Badge });
        grid.Columns.Add(new GridlyColumn("Bekleyen", "WaitingDecision", 90) { Kind = GridlyColumnKind.Numeric, TextAlign = ContentAlignment.MiddleRight });
        grid.Columns.Add(new GridlyColumn("Tarih", "AoiTestTime", 155) { Kind = GridlyColumnKind.Date, EditorType = GridlyCellEditorKind.DateTime });
        grid.FastFilterMenuForHugeLists = true;
        grid.AsyncLoadFullFilterValues = true;
        grid.EnableGrouping = false;
        grid.FrozenColumnCount = 2;
        grid.RowColorAspectName = "AIResult";
        grid.EmptyListMessage = "AOI kaydı bulunamadı";
        grid.ConditionalFormats.Clear();
        grid.ConditionalFormats.Add(new GridlyConditionalFormat
        {
            Predicate = (row, col, value) => string.Equals(Convert.ToString(value), "FAIL", StringComparison.OrdinalIgnoreCase),
            BackColor = Color.FromArgb(255, 235, 235),
            ForeColor = Color.FromArgb(160, 20, 20)
        });
    }

    public static void ApplyDatabaseEditorDefaults(GridlyView grid, string primaryKey = "Id")
    {
        grid.AutoGenerateColumns = true;
        grid.PrimaryKey = primaryKey;
        grid.EnableCellEditing = true;
        grid.AllowEditAllCells = true;
        grid.EnableInlineDatabaseEditing = true;
        grid.ShowGridLines = true;
        grid.FastFilterMenuForHugeLists = true;
        grid.AsyncLoadFullFilterValues = true;
        grid.EnableModernEmptyState = true;
    }
}
