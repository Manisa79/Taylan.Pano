using System.ComponentModel;

namespace Gridly.Core;

public partial class GridlyView
{
    [Category("Gridly - Media Smart Presets")]
    [DefaultValue(GridlyMediaSmartPreset.Music)]
    [Description("Runtime menüden uygulanan son medya akıllı preset türü.")]
    public GridlyMediaSmartPreset MediaSmartPreset { get; set; } = GridlyMediaSmartPreset.Music;

    public void ApplyMediaSmartPreset(GridlyMediaSmartPreset preset)
    {
        MediaSmartPreset = preset;
        ApplyAudix51MediaPilotDefaults();
        EnableMediaPro = true;
        EnableMediaLazyLoading = true;
        EnableMediaImageCache = true;
        MediaImageRoundedCorners = true;
        ShowMediaOverlayButton = true;
        ShowMediaQualityBadge = true;
        ShowMediaPlaybackState = preset is GridlyMediaSmartPreset.Music or GridlyMediaSmartPreset.Movie;
        ShowMediaNowPlayingBadge = preset == GridlyMediaSmartPreset.Music;
        ShowMediaEqualizerIndicator = preset == GridlyMediaSmartPreset.Music;
        ApplyV38PerformanceProfile(GridlyV38PerformancePreset.MediaLibrary);

        switch (preset)
        {
            case GridlyMediaSmartPreset.Music:
                MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                MediaQualityBadgeAspectName = string.IsNullOrWhiteSpace(MediaQualityBadgeAspectName) ? "Quality" : MediaQualityBadgeAspectName;
                DetailCardLayout = GridlyDetailCardLayout.Media;
                DetailCardMediaImageWidth = Math.Max(150, DetailCardMediaImageWidth);
                DetailCardMediaImageHeight = Math.Max(170, DetailCardMediaImageHeight);
                SetViewMode(GridlyViewMode.MediaTile);
                break;

            case GridlyMediaSmartPreset.Movie:
                MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                MediaQualityBadgeAspectName = string.IsNullOrWhiteSpace(MediaQualityBadgeAspectName) ? "Quality" : MediaQualityBadgeAspectName;
                DetailCardLayout = GridlyDetailCardLayout.PosterLeft;
                DetailCardMediaImageWidth = Math.Max(190, DetailCardMediaImageWidth);
                DetailCardMediaImageHeight = Math.Max(260, DetailCardMediaImageHeight);
                SetViewMode(GridlyViewMode.Poster);
                break;

            case GridlyMediaSmartPreset.Photo:
                MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
                DetailCardLayout = GridlyDetailCardLayout.Media;
                DetailCardMediaImageWidth = Math.Max(170, DetailCardMediaImageWidth);
                DetailCardMediaImageHeight = Math.Max(150, DetailCardMediaImageHeight);
                SetViewMode(GridlyViewMode.Gallery);
                break;

            case GridlyMediaSmartPreset.Document:
                MediaImageScaleMode = GridlyMediaImageScaleMode.Contain;
                ShowMediaPlaybackState = false;
                ShowMediaNowPlayingBadge = false;
                ShowMediaEqualizerIndicator = false;
                DetailCardLayout = GridlyDetailCardLayout.PosterLeft;
                DetailCardMediaImageWidth = Math.Max(160, DetailCardMediaImageWidth);
                DetailCardMediaImageHeight = Math.Max(220, DetailCardMediaImageHeight);
                SetViewMode(GridlyViewMode.DetailCard);
                break;
        }

        RefreshView();
    }
}
