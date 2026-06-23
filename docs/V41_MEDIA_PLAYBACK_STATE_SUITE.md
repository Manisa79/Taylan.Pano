# Gridly v41 - Media Playback State Suite

Audix ve video arşivleri için medya kartlarına gerçek playback state desteği eklendi.

## Eklenenler

- `GridlyMediaPlaybackState`: `None`, `Loading`, `Playing`, `Paused`, `Error`
- `GridlyMediaKind`: `Audio`, `Video`, `Image`, `Document`
- `ShowMediaPlaybackState`
- `ShowMediaNowPlayingBadge`
- `ShowMediaEqualizerIndicator`
- `MediaPlaybackStateGetter`
- `MediaKindGetter`
- `MediaPlayPauseClicked`
- Video için `MediaVideoPreviewMode`

## Audix örnek kullanım

```csharp
gridly.ShowMediaOverlayButton = true;
gridly.ShowMediaPlaybackState = true;
gridly.ShowMediaNowPlayingBadge = true;
gridly.ShowMediaEqualizerIndicator = true;
gridly.MediaKindGetter = row => GridlyMediaKind.Audio;
gridly.MediaPlaybackStateGetter = row =>
{
    var track = row as TrackItem;
    if (track == null) return GridlyMediaPlaybackState.None;
    if (track.IsLoading) return GridlyMediaPlaybackState.Loading;
    if (track.IsPlaying) return GridlyMediaPlaybackState.Playing;
    if (track.IsPaused) return GridlyMediaPlaybackState.Paused;
    return GridlyMediaPlaybackState.None;
};

gridly.MediaPlayPauseClicked += (s, e) =>
{
    var track = e.RowObject as TrackItem;
    if (track == null) return;

    if (e.CurrentState == GridlyMediaPlaybackState.Playing)
        Pause(track);
    else
        Play(track);

    gridly.RefreshMediaPlayback();
};
```

## Video davranışı

Video dosyalarında aynı overlay sistemi çalışır. `MediaKindGetter` video döndürürse host uygulama `MediaPlayPauseClicked` içinde ister gömülü preview paneli, ister harici player açabilir.

```csharp
gridly.MediaVideoPreviewMode = true;
gridly.MediaKindGetter = row => ((MediaItem)row).IsVideo ? GridlyMediaKind.Video : GridlyMediaKind.Audio;
```

Gridly player motoru olmaya çalışmaz; doğru davranış, playback UI state ve tıklama olayını sağlamaktır. Gerçek oynatma Audix/video player tarafında yapılır.
