using Gridly;
using Gridly.Columns;
using Gridly.Core;

namespace Gridly.FeatureSamples;

public static class GLVDefaultEditingBehaviorSamples
{
    public static FastGridlyView CreateDefaultNoDoubleClickEditSample()
    {
        var grid = new FastGridlyView
        {
            Dock = DockStyle.Fill,
            EnableCellEditing = true,

            // v25.32 default: çift tık edit açmaz.
            CellEditActivationOnDoubleClick = false,
            CellEditActivation = GLVCellEditActivateMode.F2Only,
        };

        grid.Columns.AddRange(
            new GLVColumn { Header = "Kod", AspectName = "Code", Width = 120 },
            new GLVColumn { Header = "Açıklama", AspectName = "Name", Width = 220 },
            new GLVColumn { Header = "Durum", AspectName = "Status", Width = 120 }
        );

        grid.SetObjects(new[]
        {
            new DemoRow("P001", "Çift tık edit açmaz", "F2 ile düzenle"),
            new DemoRow("P002", "Designer/kod ile açılabilir", "Güvenli default"),
        });

        return grid;
    }

    public static FastGridlyView CreateDoubleClickEditEnabledSample()
    {
        var grid = CreateDefaultNoDoubleClickEditSample();
        grid.CellEditActivationOnDoubleClick = true;
        grid.CellEditActivation = GLVCellEditActivateMode.DoubleClick;
        return grid;
    }

    private sealed record DemoRow(string Code, string Name, string Status);
}
