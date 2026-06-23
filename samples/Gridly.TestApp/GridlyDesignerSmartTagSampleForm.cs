using Gridly.Theming;

namespace Gridly.TestApp;

public sealed partial class GridlyDesignerSmartTagSampleForm : Form
{
    public GridlyDesignerSmartTagSampleForm()
    {
        InitializeComponent();

        gridly.Columns.Add(new Gridly.Columns.GridlyColumn("Person", nameof(SmartTagRow.Person), 160));
        gridly.Columns.Add(new Gridly.Columns.GridlyColumn("Occupation", nameof(SmartTagRow.Occupation), 160));
        gridly.Columns.Add(new Gridly.Columns.GridlyColumn("Status", nameof(SmartTagRow.Status), 120));
        gridly.Columns.Add(new Gridly.Columns.GridlyColumn("Notes", nameof(SmartTagRow.Notes), 260) { FillsFreeSpace = true });

        gridly.SetObjects(new[]
        {
            new SmartTagRow("Person 1", "Technician", "Active", "Smart Tag içinde kolon düzenleme ve tema seçeneklerini kontrol et."),
            new SmartTagRow("Person 2", "Operator", "Waiting", "FastGridlyView/DataListView/TreeGridlyView de aynı designer altyapısını kullanır."),
            new SmartTagRow("Person 3", "Quality", "Done", "Menüde Gridly Görevleri görünür ve tüm hızlı işlemler Gridly adıyla sunulur.")
        });

        ApplyTheme(WindowsThemeService.CurrentTheme());
    }

    private void ApplyTheme(GridlyTheme theme)
    {
        BackColor = theme.BackColor;
        ForeColor = theme.ForeColor;
        lblInfo.BackColor = theme.HeaderBackColor;
        lblInfo.ForeColor = theme.ForeColor;
        gridly.ApplyTheme(theme);
    }

    private sealed record SmartTagRow(string Person, string Occupation, string Status, string Notes);
}
