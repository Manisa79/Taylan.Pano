using System.ComponentModel;
using Gridly.Exporting;

namespace Gridly.Core;

public sealed class GridlyViewExportingFacade
{
    private readonly GridlyView _owner;

    internal GridlyViewExportingFacade(GridlyView owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    public string ExportVisibleCsv(string path, char separator = ';') => _owner.ExportVisibleCsv(path, separator);
    public string ExportVisibleExcel(string path, string worksheetName = "GridlyView") => _owner.ExportVisibleExcel(path, worksheetName);
    public string ExportVisiblePdf(string path, string title = "GridlyView") => _owner.ExportVisiblePdf(path, title);
    public string ExportVisiblePdf(string path, GridlyPdfExportOptions options) => _owner.ExportVisiblePdf(path, options);
    public string ExportVisibleHtml(string path, string title = "GridlyView") => _owner.ExportVisibleHtml(path, title);

    public string ExportSelectedCsvWithDialog() => _owner.ExportSelectedCsvWithDialog();
    public string ExportVisibleCsvWithDialog() => _owner.ExportVisibleCsvWithDialog();
    public string ExportVisibleExcelWithDialog() => _owner.ExportVisibleExcelWithDialog();
}

public partial class GridlyView
{
    private GridlyViewExportingFacade? _exportingFacade;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridlyViewExportingFacade Exporting => _exportingFacade ??= new GridlyViewExportingFacade(this);
}
