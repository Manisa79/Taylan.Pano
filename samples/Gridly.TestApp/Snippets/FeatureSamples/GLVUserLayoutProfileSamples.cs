using Gridly;
using System.Windows.Forms;
using Gridly.Columns;
using Gridly.Core;
using Gridly.Filtering;

namespace Gridly.FeatureSamples;

public static class GLVUserLayoutProfileSamples
{
    public static FastGridlyView CreateUserProfileGrid()
    {
        var grid = new FastGridlyView
        {
            Dock = DockStyle.Fill,
            AutoLoadUserLayout = true,
            AutoSaveUserLayout = true,
            UserLayoutKey = "Samples.UserProfileGrid",
            FilterMenuMode = GridlyFilterMenuMode.ModalWindow,
            ShowFilterStyleSelectorInContextMenu = true,
            ShowQuickClearFilterInHeaderMenu = true,
            AsyncSortForLargeLists = true,
            CacheSortKeysForLargeLists = true,
            AsyncSortMaxRows = 0
        };

        grid.Columns.Add(new GLVColumn { Header = "Kod", AspectName = "Code", Width = 120, AllowFilter = true, AllowSort = true });
        grid.Columns.Add(new GLVColumn { Header = "Açıklama", AspectName = "Text", Width = 260, AllowFilter = true, AllowSort = true });
        grid.Columns.Add(new GLVColumn { Header = "Tarih", AspectName = "Date", Width = 140, AllowFilter = true, AllowSort = true });

        grid.SetObjects(Enumerable.Range(1, 10000).Select(i => new
        {
            Code = "K" + i.ToString("000000"),
            Text = "Demo satır " + i,
            Date = DateTime.Today.AddDays(-i % 365)
        }));

        return grid;
    }
}
