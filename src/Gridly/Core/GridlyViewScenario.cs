namespace Gridly.Core;

/// <summary>
/// Gridly'nin aynı kontrolü farklı iş ekranlarında hızlı ve tutarlı kurabilmesi için hazır görünüm senaryoları.
/// Özellikle MasterData, AOI Support Desk ve üretim/SAP listeleri gibi projelerde aynı UX dilini korumak için kullanılır.
/// </summary>
public enum GridlyViewScenario
{
    /// <summary>Klasik SQL / SAP / BOM tablosu.</summary>
    DataTable,

    /// <summary>Çok fazla kayıt gösterilecek Excel benzeri yoğun tablo.</summary>
    DenseData,

    /// <summary>Ürün ağacı, malzeme ağacı veya reçete hiyerarşisi.</summary>
    ProductTree,

    /// <summary>BOM / komponent / pozisyon listesi.</summary>
    BomPositions,

    /// <summary>Dizgi programı, makine programı veya dosya çıktı listesi.</summary>
    ProgramFiles,

    /// <summary>Makine, hat, feeder, istasyon veya operatör seçim ekranı.</summary>
    MachineOrLinePicker,

    /// <summary>Ticket, mesaj veya işlem takip kartları.</summary>
    TicketBoard,

    /// <summary>Operasyon geçmişi, log veya karar akışı.</summary>
    Timeline,

    /// <summary>Üst kayıt + alt detay listesi kullanan bakım ekranları.</summary>
    MasterDetail,

    /// <summary>Albüm, film, fotoğraf ve doküman gibi genel medya kütüphanesi.</summary>
    MediaLibrary,

    /// <summary>Albüm kapağı ve sanatçı/şarkı metadata odaklı müzik arşivi.</summary>
    AlbumLibrary,

    /// <summary>Film afişi, video preview ve oynatma aksiyonu odaklı video arşivi.</summary>
    MovieLibrary,

    /// <summary>Fotoğraf/kapak galerisi ve thumbnail koleksiyonu.</summary>
    PhotoGallery,

    /// <summary>Spotify/Plex benzeri kompakt medya kartları.</summary>
    MediaTiles,

    /// <summary>Netflix/Plex benzeri yatay medya şeridi.</summary>
    FilmStrip,

    /// <summary>Kapak solda, tüm metadata sağda olan medya detay kartı.</summary>
    MediaDetailCard,

    /// <summary>Çalan medya, play/pause, now playing ve equalizer göstergeleri.</summary>
    NowPlaying,

    /// <summary>Video dosyaları için play overlay ve preview niyeti olan görünüm.</summary>
    VideoPreview,

    /// <summary>PDF, görsel, doküman veya dosya önizleme listeleri.</summary>
    DocumentPreview
}

public static class GridlyViewScenarioExtensions
{
    public static void ApplyScenario(this GridlyView grid, GridlyViewScenario scenario)
    {
        if (grid == null) throw new ArgumentNullException(nameof(grid));

        grid.AutoSizeTileWidthToContent = false;
        grid.TilePosterMode = false;
        grid.EnforceTilePreferredHeight = true;
        grid.AllowMultilineCells = false;
        grid.MaxCellTextLines = 1;

        switch (scenario)
        {
            case GridlyViewScenario.DataTable:
                grid.ShowHeader = true;
                grid.RowHeight = 28;
                grid.SetViewMode(GridlyViewMode.Details);
                break;

            case GridlyViewScenario.DenseData:
                grid.ShowHeader = true;
                grid.RowHeight = 22;
                grid.SetViewMode(GridlyViewMode.DenseList);
                break;

            case GridlyViewScenario.ProductTree:
                grid.ShowHeader = true;
                grid.RowHeight = 30;
                grid.AllowMultilineCells = true;
                grid.MaxCellTextLines = 2;
                grid.SetViewMode(GridlyViewMode.GroupedList);
                break;

            case GridlyViewScenario.BomPositions:
                grid.ShowHeader = true;
                grid.RowHeight = 26;
                grid.SetViewMode(GridlyViewMode.DenseList);
                break;

            case GridlyViewScenario.ProgramFiles:
                grid.TilePreferredWidth = 320;
                grid.TilePreferredHeight = 110;
                grid.TileMaxTextLines = 4;
                grid.SetViewMode(GridlyViewMode.Tile);
                break;

            case GridlyViewScenario.MachineOrLinePicker:
                grid.TilePreferredWidth = 185;
                grid.TilePreferredHeight = 98;
                grid.TileMaxTextLines = 2;
                grid.SetViewMode(GridlyViewMode.IconGrid);
                break;

            case GridlyViewScenario.TicketBoard:
                grid.TilePreferredWidth = 360;
                grid.LargeCardPreferredWidth = 560;
                grid.LargeCardPreferredHeight = 160;
                grid.LargeCardMaxTextLines = 6;
                grid.SetViewMode(GridlyViewMode.DashboardCard);
                break;

            case GridlyViewScenario.Timeline:
                grid.LargeCardPreferredWidth = 900;
                grid.TilePreferredHeight = 136;
                grid.LargeCardMaxTextLines = 8;
                grid.AllowMultilineCells = true;
                grid.MaxCellTextLines = 4;
                grid.SetViewMode(GridlyViewMode.Timeline);
                break;

            case GridlyViewScenario.MasterDetail:
                grid.ShowHeader = true;
                grid.AllowMultilineCells = true;
                grid.MaxCellTextLines = 3;
                grid.RowHeight = 34;
                grid.SetViewMode(GridlyViewMode.MasterDetail);
                break;

            case GridlyViewScenario.MediaLibrary:
                ApplyMediaDefaults(grid);
                grid.TilePreferredWidth = 260;
                grid.TilePreferredHeight = 260;
                grid.TilePosterImageHeight = 160;
                grid.SetViewMode(GridlyViewMode.MediaTile);
                break;

            case GridlyViewScenario.AlbumLibrary:
                ApplyMediaDefaults(grid);
                grid.TilePreferredWidth = 245;
                grid.TilePreferredHeight = 265;
                grid.TilePosterImageHeight = 170;
                grid.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                grid.SetViewMode(GridlyViewMode.MediaTile);
                break;

            case GridlyViewScenario.MovieLibrary:
                ApplyMediaDefaults(grid);
                grid.PosterPreferredWidth = 190;
                grid.PosterPreferredHeight = 310;
                grid.PosterImageHeight = 235;
                grid.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                grid.MediaVideoPreviewMode = true;
                grid.SetViewMode(GridlyViewMode.Poster);
                break;

            case GridlyViewScenario.PhotoGallery:
                ApplyMediaDefaults(grid);
                grid.TilePreferredWidth = 220;
                grid.TilePreferredHeight = 220;
                grid.TilePosterImageHeight = 160;
                grid.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                grid.SetViewMode(GridlyViewMode.Gallery);
                break;

            case GridlyViewScenario.MediaTiles:
                ApplyMediaDefaults(grid);
                grid.TilePreferredWidth = 230;
                grid.TilePreferredHeight = 250;
                grid.TilePosterImageHeight = 156;
                grid.SetViewMode(GridlyViewMode.MediaTile);
                break;

            case GridlyViewScenario.FilmStrip:
                ApplyMediaDefaults(grid);
                grid.TilePreferredWidth = 560;
                grid.TilePreferredHeight = 170;
                grid.TilePosterImageHeight = 126;
                grid.SetViewMode(GridlyViewMode.FilmStrip);
                break;

            case GridlyViewScenario.MediaDetailCard:
                ApplyMediaDefaults(grid);
                grid.ShowHeader = false;
                grid.DetailCardLayout = GridlyDetailCardLayout.Media;
                grid.DetailCardMediaImageWidth = 158;
                grid.DetailCardMediaImageHeight = 178;
                grid.ShowDetailCardColumnHeaders = true;
                grid.SetViewMode(GridlyViewMode.DetailCard);
                break;

            case GridlyViewScenario.NowPlaying:
                ApplyMediaDefaults(grid);
                grid.ShowMediaPlaybackState = true;
                grid.ShowMediaNowPlayingBadge = true;
                grid.ShowMediaEqualizerIndicator = true;
                grid.TilePreferredWidth = 245;
                grid.TilePreferredHeight = 265;
                grid.TilePosterImageHeight = 170;
                grid.SetViewMode(GridlyViewMode.MediaTile);
                break;

            case GridlyViewScenario.VideoPreview:
                ApplyMediaDefaults(grid);
                grid.MediaVideoPreviewMode = true;
                grid.TilePreferredWidth = 560;
                grid.TilePreferredHeight = 178;
                grid.TilePosterImageHeight = 132;
                grid.SetViewMode(GridlyViewMode.FilmStrip);
                break;

            case GridlyViewScenario.DocumentPreview:
                ApplyMediaDefaults(grid);
                grid.PosterPreferredWidth = 180;
                grid.PosterPreferredHeight = 260;
                grid.PosterImageHeight = 190;
                grid.MediaImageScaleMode = GridlyMediaImageScaleMode.Contain;
                grid.SetViewMode(GridlyViewMode.Poster);
                break;
        }
    }

    private static void ApplyMediaDefaults(GridlyView grid)
    {
        grid.ShowHeader = false;
        grid.TilePosterMode = true;
        grid.AutoSizeTileWidthToContent = false;
        grid.EnforceTilePreferredHeight = true;
        grid.AllowMultilineCells = true;
        grid.MaxCellTextLines = 2;
        grid.TileMaxTextLines = 4;
        grid.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
        grid.MediaImageRoundedCorners = true;
        grid.ShowMediaOverlayButton = true;
        grid.ShowMediaQualityBadge = true;
        grid.ShowMediaPlaybackState = true;
        grid.ShowMediaNowPlayingBadge = true;
        grid.ShowMediaEqualizerIndicator = true;
    }

    public static bool IsMediaScenario(this GridlyViewScenario scenario)
        => scenario is GridlyViewScenario.MediaLibrary
            or GridlyViewScenario.AlbumLibrary
            or GridlyViewScenario.MovieLibrary
            or GridlyViewScenario.PhotoGallery
            or GridlyViewScenario.MediaTiles
            or GridlyViewScenario.FilmStrip
            or GridlyViewScenario.MediaDetailCard
            or GridlyViewScenario.NowPlaying
            or GridlyViewScenario.VideoPreview
            or GridlyViewScenario.DocumentPreview;

    public static string GetDisplayName(this GridlyViewScenario scenario)
        => scenario switch
        {
            GridlyViewScenario.DataTable => "Standart Tablo",
            GridlyViewScenario.DenseData => "Yoğun Veri Tablosu",
            GridlyViewScenario.ProductTree => "Ürün Ağacı",
            GridlyViewScenario.BomPositions => "BOM / Pozisyon Listesi",
            GridlyViewScenario.ProgramFiles => "Program Dosyaları",
            GridlyViewScenario.MachineOrLinePicker => "Makine / Hat Seçimi",
            GridlyViewScenario.TicketBoard => "Ticket Dashboard",
            GridlyViewScenario.Timeline => "İşlem Geçmişi",
            GridlyViewScenario.MasterDetail => "MasterData Detay",
            GridlyViewScenario.MediaLibrary => "Medya Kütüphanesi",
            GridlyViewScenario.AlbumLibrary => "Albüm / Müzik Arşivi",
            GridlyViewScenario.MovieLibrary => "Film / Video Afişleri",
            GridlyViewScenario.PhotoGallery => "Fotoğraf Galerisi",
            GridlyViewScenario.MediaTiles => "MediaTile Kartları",
            GridlyViewScenario.FilmStrip => "FilmStrip / Yatay Şerit",
            GridlyViewScenario.MediaDetailCard => "Media DetailCard",
            GridlyViewScenario.NowPlaying => "Now Playing / Çalan Medya",
            GridlyViewScenario.VideoPreview => "Video Preview",
            GridlyViewScenario.DocumentPreview => "Doküman Önizleme",
            _ => scenario.ToString()
        };
}
