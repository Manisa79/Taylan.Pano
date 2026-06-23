using Gridly.Columns;
using Gridly.Core;
using Gridly.Filtering;
using Gridly.Theming;

namespace Gridly.TestApp;

public sealed class GridlyV277CardFilterUxForm : Form
{
    private readonly GridlyView _grid = new()
    {
        Dock = DockStyle.Fill,
        ViewMode = GridlyViewMode.DashboardCard,
        FilterMenuMode = GridlyFilterMenuMode.Both,
        ShowQuickFilterBar = true,
        ShowFloatingFilterButton = true,
        ShowActiveFilterChips = true,
        CardFilterUxOnlyInCardViews = true,
        CardFilterUxPlacement = GridlyCardFilterUxPlacement.TopBarAndFloatingButton,
        QuickFilterPlaceholderText = "Ticket, makine, SAP kodu veya açıklama ara...",
        FloatingFilterButtonText = "Filtre",
        FilterPopupResizable = true,
        FilterPopupRememberSize = true,
        FilterPopupShowValueTooltips = true,
        FilterPopupAutoWidthForLongValues = true,
        HeaderContextMenuBehavior = GridlyHeaderContextMenuBehavior.Full,
        EmptyListMessage = "Kart filtre UX örneği için kayıt yok"
    };

    private readonly ToolStrip _tool = new() { Dock = DockStyle.Top, GripStyle = ToolStripGripStyle.Hidden };

    public GridlyV277CardFilterUxForm()
    {
        Text = "Gridly v27.7 - Card / Large View Filter UX";
        Width = 1320;
        Height = 820;
        StartPosition = FormStartPosition.CenterScreen;

        ConfigureGrid();
        ConfigureToolbar();
        Controls.Add(_grid);
        Controls.Add(_tool);
        ApplyTheme(false);
        LoadRows();
    }

    private void ConfigureToolbar()
    {
        _tool.Items.Add(new ToolStripButton("Dashboard Kart", null, (_, __) => _grid.SetViewMode(GridlyViewMode.DashboardCard)));
        _tool.Items.Add(new ToolStripButton("Geniş Kart", null, (_, __) => _grid.SetViewMode(GridlyViewMode.LargeCard)));
        _tool.Items.Add(new ToolStripButton("Kanban", null, (_, __) => _grid.SetViewMode(GridlyViewMode.Kanban)));
        _tool.Items.Add(new ToolStripButton("Detay Liste", null, (_, __) => _grid.SetViewMode(GridlyViewMode.Details)));
        _tool.Items.Add(new ToolStripSeparator());
        _tool.Items.Add(new ToolStripButton("Top Bar Aç/Kapat", null, (_, __) => { _grid.ShowQuickFilterBar = !_grid.ShowQuickFilterBar; _grid.RefreshView(); }));
        _tool.Items.Add(new ToolStripButton("Floating Aç/Kapat", null, (_, __) => { _grid.ShowFloatingFilterButton = !_grid.ShowFloatingFilterButton; _grid.RefreshView(); }));
        _tool.Items.Add(new ToolStripButton("Chip Aç/Kapat", null, (_, __) => { _grid.ShowActiveFilterChips = !_grid.ShowActiveFilterChips; _grid.RefreshView(); }));
        _tool.Items.Add(new ToolStripButton("Filtreleri Temizle", null, (_, __) => _grid.ClearFilters()));
        _tool.Items.Add(new ToolStripSeparator());
        _tool.Items.Add(new ToolStripButton("Açık Tema", null, (_, __) => ApplyTheme(false)));
        _tool.Items.Add(new ToolStripButton("Koyu Tema", null, (_, __) => ApplyTheme(true)));
    }

    private void ConfigureGrid()
    {
        _grid.Columns.Clear();
        _grid.Columns.Add(new GridlyColumn("Seç", nameof(CardFilterRow.Selected), 50) { Kind = GridlyColumnKind.CheckBox });
        _grid.Columns.Add(new GridlyColumn("Durum", nameof(CardFilterRow.Status), 120) { Kind = GridlyColumnKind.Badge, AllowGroup = true });
        _grid.Columns.Add(new GridlyColumn("Ticket", nameof(CardFilterRow.Ticket), 140));
        _grid.Columns.Add(new GridlyColumn("Başlık", nameof(CardFilterRow.Title), 280) { FillFreeSpace = true, WordWrap = true, MaxTextLines = 3 });
        _grid.Columns.Add(new GridlyColumn("Makine", nameof(CardFilterRow.Machine), 140) { AllowGroup = true });
        _grid.Columns.Add(new GridlyColumn("SAP / Oto Kod", nameof(CardFilterRow.SapCode), 170));
        _grid.Columns.Add(new GridlyColumn("İlerleme", nameof(CardFilterRow.Progress), 100) { Kind = GridlyColumnKind.ProgressBar });
        _grid.Columns.Add(new GridlyColumn("Etiketler", nameof(CardFilterRow.Tags), 220) { Kind = GridlyColumnKind.Tags });
        _grid.Columns.Add(new GridlyColumn("Açıklama", nameof(CardFilterRow.Detail), 520) { FillFreeSpace = true, WordWrap = true, MaxTextLines = 5 });
    }

    private void LoadRows()
    {
        string[] statuses = { "Open", "In Progress", "Waiting", "Done", "Fail" };
        string[] machines = { "AOI-QX150", "AOI-FLEX", "LINE-03", "REWORK-01", "AXIAL-02" };
        var rows = new List<CardFilterRow>();
        for (int i = 1; i <= 80; i++)
        {
            rows.Add(new CardFilterRow
            {
                Selected = i % 9 == 0,
                Status = statuses[i % statuses.Length],
                Ticket = "TCK-" + i.ToString("0000"),
                Title = i % 3 == 0 ? "Uzun SAP malzeme açıklaması ve program yolu olan üretim ticket kartı" : "AOI destek / MasterData kart filtre örneği",
                Machine = machines[i % machines.Length],
                SapCode = "SAP-" + (100000 + i) + " / OTO-" + (9000 + i),
                Progress = (i * 7) % 101,
                Tags = i % 2 == 0 ? "BOM;SAP;Program" : "AOI;False Call;Rework",
                Detail = "CardView, DashboardCard, Kanban ve Poster gibi büyük görünümlerde header görünmese bile kullanıcı üst filtre barından arama yapabilir, kolon filtresini açabilir ve aktif filtreleri chip olarak görebilir. Uzun açıklamalar ve SAP yolları filtre popup içinde de büyütülebilir."
            });
        }
        _grid.SetObjects(rows);
    }

    private void ApplyTheme(bool dark)
    {
        GridlyTheme theme = dark ? GridlyTheme.DarkTheme() : GridlyTheme.LightTheme();
        BackColor = theme.BackColor;
        ForeColor = theme.ForeColor;
        _grid.ApplyTheme(theme);
        SmartMenuRenderer.ApplyTo(_tool, theme);
    }

    private sealed class CardFilterRow
    {
        public bool Selected { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Ticket { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Machine { get; set; } = string.Empty;
        public string SapCode { get; set; } = string.Empty;
        public int Progress { get; set; }
        public string Tags { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
