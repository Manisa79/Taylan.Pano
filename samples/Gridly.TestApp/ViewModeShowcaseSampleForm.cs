using Gridly.Columns;
using Gridly.Core;
using Gridly.Filtering;
using Gridly.Theming;

namespace Gridly.TestApp;

public sealed class ViewModeShowcaseSampleForm : Form
{
    private readonly GridlyView _grid = new()
    {
        Dock = DockStyle.Fill,
        Mode = GridlyMode.Object,
        ViewMode = GridlyViewMode.DashboardCard,
        HeaderContextMenuBehavior = GridlyHeaderContextMenuBehavior.Full,
        FilterMenuMode = GridlyFilterMenuMode.Both,
        TilePreferredWidth = 250,
        TilePreferredHeight = 104,
        TileMaxTextLines = 4,
        LargeCardPreferredWidth = 560,
        LargeCardPreferredHeight = 170,
        LargeCardMaxTextLines = 8,
        EnforceTilePreferredHeight = true,
        EmptyListMessage = "Görünüm vitrini için kayıt yok"
    };

    private readonly ToolStrip _tool = new() { Dock = DockStyle.Top, GripStyle = ToolStripGripStyle.Hidden };
    private readonly Label _info = new()
    {
        Dock = DockStyle.Top,
        Height = 66,
        Padding = new Padding(12),
        TextAlign = ContentAlignment.MiddleLeft,
        Text = "Gridly görünüm vitrini: AOI ticket, makine, operatör, mesaj, SAP pozisyon ve dashboard senaryoları için adlandırılmış görünüm seçenekleri. Sağ tuş menüsünde de aynı isimlerle görünür."
    };

    private readonly List<ShowcaseTicket> _rows = new();

    public ViewModeShowcaseSampleForm()
    {
        Text = "Gridly Gelişmiş Görünüm Vitrini";
        Width = 1240;
        Height = 760;
        StartPosition = FormStartPosition.CenterScreen;

        _grid.Columns.Add(new GridlyColumn("Ticket", nameof(ShowcaseTicket.TicketNo), 120));
        _grid.Columns.Add(new GridlyColumn("Başlık", nameof(ShowcaseTicket.Title), 260) { FillFreeSpace = true, WordWrap = true, MaxTextLines = 2 });
        _grid.Columns.Add(new GridlyColumn("Makine", nameof(ShowcaseTicket.Machine), 150));
        _grid.Columns.Add(new GridlyColumn("Durum", nameof(ShowcaseTicket.Status), 100) { Kind = GridlyColumnKind.Badge });
        _grid.Columns.Add(new GridlyColumn("Öncelik", nameof(ShowcaseTicket.Priority), 95));
        _grid.Columns.Add(new GridlyColumn("Oto Kod", nameof(ShowcaseTicket.OtoCode), 105));
        _grid.Columns.Add(new GridlyColumn("Son Mesaj", nameof(ShowcaseTicket.LastMessage), 420) { FillFreeSpace = true, WordWrap = true, MaxTextLines = 5 });
        _grid.Columns.Add(new GridlyColumn("Zaman", nameof(ShowcaseTicket.TimeText), 140));

        AddModeButton(GridlyViewMode.Details);
        AddModeButton(GridlyViewMode.DenseList);
        AddModeButton(GridlyViewMode.Tile);
        AddModeButton(GridlyViewMode.LargeCard);
        AddModeButton(GridlyViewMode.DashboardCard);
        AddModeButton(GridlyViewMode.RowCard);
        AddModeButton(GridlyViewMode.RowPreview);
        AddModeButton(GridlyViewMode.DetailCard);
        AddModeButton(GridlyViewMode.PropertyCard);
        _tool.Items.Add(new ToolStripSeparator());
        AddModeButton(GridlyViewMode.Poster);
        AddModeButton(GridlyViewMode.MediaTile);
        AddModeButton(GridlyViewMode.Gallery);
        AddModeButton(GridlyViewMode.FilmStrip);
        AddModeButton(GridlyViewMode.IconGrid);
        _tool.Items.Add(new ToolStripSeparator());
        AddModeButton(GridlyViewMode.KpiDashboard);
        AddModeButton(GridlyViewMode.HeatMap);
        AddModeButton(GridlyViewMode.MiniChart);
        AddModeButton(GridlyViewMode.GroupCard);
        AddModeButton(GridlyViewMode.GroupedList);
        AddModeButton(GridlyViewMode.Kanban);
        AddModeButton(GridlyViewMode.Timeline);
        AddModeButton(GridlyViewMode.MasterDetail);
        _tool.Items.Add(new ToolStripSeparator());
        _tool.Items.Add(new ToolStripButton("Gruplamayı temizle", null, (_, __) => _grid.ClearGrouping()));
        var detailHeaderToggle = new ToolStripButton("DetailCard başlıkları")
        {
            CheckOnClick = true,
            Checked = _grid.ShowDetailCardColumnHeaders
        };
        detailHeaderToggle.CheckedChanged += (_, __) => _grid.ShowDetailCardColumnHeaders = detailHeaderToggle.Checked;
        _tool.Items.Add(detailHeaderToggle);

        var fixedFreeToggle = new ToolStripButton("FixedFree resize")
        {
            CheckOnClick = true,
            Checked = _grid.AbsorbColumnResizeOverflowFromFreeSpace
        };
        fixedFreeToggle.CheckedChanged += (_, __) => _grid.AbsorbColumnResizeOverflowFromFreeSpace = fixedFreeToggle.Checked;
        _tool.Items.Add(fixedFreeToggle);

        _tool.Items.Add(new ToolStripButton("Açık tema", null, (_, __) => ApplyTheme(false)));
        _tool.Items.Add(new ToolStripButton("Koyu tema", null, (_, __) => ApplyTheme(true)));

        Controls.Add(_grid);
        Controls.Add(_info);
        Controls.Add(_tool);

        LoadRows();
        ApplyTheme(Program.AppTheme.IsDark);
        ApplyMode(GridlyViewMode.DashboardCard);
    }

    private void AddModeButton(GridlyViewMode mode)
    {
        _tool.Items.Add(new ToolStripButton(GridlyView.GetViewModeDisplayName(mode), null, (_, __) => ApplyMode(mode)));
    }

    private void ApplyMode(GridlyViewMode mode)
    {
        if (mode != GridlyViewMode.GroupedList)
            _grid.ClearGrouping();

        _grid.TilePosterMode = false;
        _grid.AutoSizeTileWidthToContent = false;

        switch (mode)
        {
            case GridlyViewMode.DenseList:
                _grid.AllowMultilineCells = false;
                _grid.RowHeight = 22;
                break;

            case GridlyViewMode.Tile:
                _grid.TilePreferredWidth = 250;
                _grid.TilePreferredHeight = 104;
                _grid.TileMaxTextLines = 4;
                break;

            case GridlyViewMode.LargeCard:
                _grid.LargeCardPreferredWidth = 560;
                _grid.LargeCardPreferredHeight = 172;
                _grid.LargeCardMaxTextLines = 8;
                break;

            case GridlyViewMode.DashboardCard:
                _grid.TilePreferredWidth = 360;
                _grid.LargeCardPreferredHeight = 158;
                _grid.LargeCardMaxTextLines = 6;
                break;

            case GridlyViewMode.RowCard:
                _grid.LargeCardPreferredWidth = 900;
                _grid.TilePreferredHeight = 128;
                _grid.LargeCardMaxTextLines = 7;
                break;

            case GridlyViewMode.MediaTile:
                _grid.TilePosterMode = true;
                _grid.TilePreferredWidth = 190;
                _grid.TilePreferredHeight = 236;
                _grid.TilePosterImageHeight = 128;
                _grid.TileMaxTextLines = 4;
                break;

            case GridlyViewMode.FilmStrip:
                _grid.TilePosterMode = true;
                _grid.TilePreferredWidth = 900;
                _grid.TilePreferredHeight = 164;
                _grid.TilePosterImageHeight = 116;
                _grid.TileMaxTextLines = 7;
                break;

            case GridlyViewMode.DetailCard:
                _grid.LargeCardPreferredWidth = 980;
                _grid.LargeCardMaxTextLines = 12;
                break;

            case GridlyViewMode.IconGrid:
                _grid.TilePreferredWidth = 180;
                _grid.TilePreferredHeight = 96;
                _grid.TileMaxTextLines = 2;
                break;

            case GridlyViewMode.GroupedList:
                _grid.SetGroupBy(nameof(ShowcaseTicket.Status));
                break;

            case GridlyViewMode.Kanban:
                _grid.TilePreferredWidth = 330;
                _grid.LargeCardPreferredHeight = 174;
                _grid.LargeCardMaxTextLines = 6;
                _grid.SetGroupBy(nameof(ShowcaseTicket.Status));
                break;

            case GridlyViewMode.Timeline:
                _grid.LargeCardPreferredWidth = 900;
                _grid.TilePreferredHeight = 136;
                _grid.LargeCardMaxTextLines = 8;
                break;

            case GridlyViewMode.MasterDetail:
                _grid.AllowMultilineCells = true;
                _grid.MaxCellTextLines = 3;
                break;
        }

        _grid.SetViewMode(mode);
        _info.Text = GetModeDescription(mode);
    }

    private static string GetModeDescription(GridlyViewMode mode)
    {
        return mode switch
        {
            GridlyViewMode.Details => "Detay Liste: klasik tablo; SAP pozisyonları, BOM, SQL kayıtları ve kolon bazlı filtreleme için en net görünüm.",
            GridlyViewMode.DenseList => "Yoğun Liste: çok fazla satırı aynı anda görmek için Excel benzeri sıkı satır yüksekliği.",
            GridlyViewMode.Tile => "Kart Görünümü: ticket, operatör veya makine kayıtlarını yan yana özet kartlarla gösterir.",
            GridlyViewMode.LargeCard => "Geniş Kart: 4-5 satır okunabilir açıklama isteyen ticket/mesaj ekranları için.",
            GridlyViewMode.DashboardCard => "Dashboard Kart: durum/öncelik odaklı, üst vurgu çizgili modern kart görünümü.",
            GridlyViewMode.RowCard => "Satır Kart: her kaydı tam genişlikte kart olarak gösterir; destek mesajları ve son cevap akışında rahat okunur.",
            GridlyViewMode.MediaTile => "MediaTile: albüm kapağı, film afişi, öğrenci/makine fotoğrafı gibi kompakt medya katalogları için.",
            GridlyViewMode.FilmStrip => "FilmStrip: solda büyük görsel, sağda açıklama olan yatay medya şeridi; ticket eki, sahne karesi ve arşiv listeleri için.",
            GridlyViewMode.DetailCard => "DetailCard: her kaydı tam genişlikte kart yapar ve tüm görünür kolonları etiket/değer olarak satır satır gösterir.",
            GridlyViewMode.IconGrid => "İkon Grid: hat, makine ve operatör seçimi gibi görsel seçim ekranları için.",
            GridlyViewMode.GroupedList => "Gruplu Liste: durum, makine veya hat bazlı bölümleyerek yönetim ekranlarını okunur hale getirir.",
            GridlyViewMode.Kanban => "Kanban: açık/bekliyor/çözüldü gibi durumlara göre ticket takibi için kart + grup mantığı.",
            GridlyViewMode.Timeline => "Zaman Akışı: mesaj, olay ve ticket geçmişini kronolojik takip etmek için.",
            GridlyViewMode.MasterDetail => "Master-Detail: üstte kayıt, altta/yan tarafta detay paneli kullanacak formlar için liste odaklı temel görünüm.",
            _ => GridlyView.GetViewModeDisplayName(mode)
        };
    }

    private void LoadRows()
    {
        string[] statuses = { "Açık", "Bekliyor", "İşlemde", "Çözüldü" };
        string[] priorities = { "Info", "Warning", "Action" };
        string[] machines = { "LINE01YASREW", "LINE02YASREW", "VEMRKD15061", "AOIQX250I" };

        _rows.Clear();
        for (int i = 1; i <= 64; i++)
        {
            string status = statuses[i % statuses.Length];
            _rows.Add(new ShowcaseTicket(
                $"AOI-{i:000000}",
                i % 4 == 0 ? "AOI durdu, destek gerekiyor" : i % 3 == 0 ? "Çok hata var, kontrol eder misiniz?" : "Program / faskal isteği",
                machines[i % machines.Length],
                status,
                priorities[i % priorities.Length],
                (24022000 + i * 37).ToString(),
                "Operatörden gelen mesaj, makine bilgisi, son teknisyen cevabı ve SAP/OtoKod bilgisi bu alanda görünür. Pencere genişliğine göre kart/liste görünümü daha okunabilir olur.",
                DateTime.Now.AddMinutes(-i * 9).ToString("dd.MM HH:mm")));
        }
        _grid.SetObjects(_rows);
    }

    private void ApplyTheme(bool dark)
    {
        BackColor = dark ? Color.FromArgb(28, 29, 33) : Color.FromArgb(246, 248, 252);
        ForeColor = dark ? Color.White : Color.FromArgb(28, 31, 36);
        _info.BackColor = BackColor;
        _info.ForeColor = ForeColor;
        var theme = GridlyTheme.FromParentColor(BackColor, ForeColor);
        _grid.ApplyTheme(theme);
        SmartMenuRenderer.ApplyTo(_tool, theme);
    }

    private sealed record ShowcaseTicket(string TicketNo, string Title, string Machine, string Status, string Priority, string OtoCode, string LastMessage, string TimeText);
}
