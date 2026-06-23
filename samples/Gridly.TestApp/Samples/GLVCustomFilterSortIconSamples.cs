using Gridly;
using System.Drawing;
using Gridly.Columns;
using Gridly.Core;

namespace Gridly.TestApp.Samples;

public static class GLVCustomFilterSortIconSamples
{
    public static void Apply(FastGridlyView grid, GLVColumn barcodeColumn)
    {
        // Eski nokta benzeri filtre simgesi
        grid.FilterIconStyle = GridlyFilterIconStyle.Dot;

        // Yeni sort simgesi
        grid.ShowColumnSortGlyphs = true;
        grid.SortGlyphStyle = GridlySortGlyphStyle.Chevron;

        // Büyük listelerde UI donmasını azaltır
        grid.AsyncSortForLargeLists = true;
        grid.AsyncSortThreshold = 50000;
        grid.ShowSortBusyIndicator = true;

        // Kolon bazlı override
        barcodeColumn.FilterIconStyle = GridlyFilterIconStyle.Funnel;
        barcodeColumn.SortGlyphStyle = GridlySortGlyphStyle.Triangle;

        // Kendi ikonunu vermek istersen:
        // grid.FilterIconStyle = GridlyFilterIconStyle.CustomImage;
        // grid.CustomFilterIcon = Image.FromFile("filter.png");
        // grid.SortGlyphStyle = GridlySortGlyphStyle.CustomImage;
        // grid.CustomSortAscendingIcon = Image.FromFile("sort_asc.png");
        // grid.CustomSortDescendingIcon = Image.FromFile("sort_desc.png");
    }
}
