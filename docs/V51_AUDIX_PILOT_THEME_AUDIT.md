# Gridly v51 - Audix Pilot / Theme Audit / Example Cleanup

Bu sürümde yeni bir görünüm modu eklemek yerine Gridly'nin mevcut medya, tema ve örnek merkezi yetenekleri gerçek kullanım senaryosuna yaklaştırıldı.

## Hedef

- Audix içinde albüm kapağı, play/pause state, video preview ve medya rozetlerini tak-çalıştır kullanmak.
- Koyu/açık/yüksek kontrast temalarda buton, label, combo, badge, kart ve medya overlay okunurluğunu korumak.
- Example Center kalabalığında özellikleri kategori + arama + hızlı erişim ile bulmayı kolaylaştırmak.
- v50.2 hardening ayarlarını koruyarak Gridly 5.x API kırılma riskini azaltmak.

## Yeni yardımcılar

```csharp
gridly.ApplyGridly51RealUsageDefaults();
gridly.ApplyAudix51MediaPilotDefaults();
gridly.ApplyTheme51AuditDefaults(GridlyThemeStudioPreset.AudixDark);
var checks = gridly.RunGridly51UsageChecks();
```

## Audix için temel kullanım

```csharp
gridly.ApplyAudix51MediaPilotDefaults();
gridly.MediaQualityBadgeGetter = row => ((TrackRow)row).Quality;
gridly.MediaKindGetter = row => ((TrackRow)row).IsVideo ? GridlyMediaKind.Video : GridlyMediaKind.Audio;
gridly.MediaPlaybackStateGetter = row => ((TrackRow)row).PlaybackState;
gridly.MediaPlayPauseClicked += (_, e) => TogglePlayback((TrackRow)e.RowObject);
gridly.SetViewMode(GridlyViewMode.Poster);
```

Önerilen Audix görünümleri:

- `Poster`: büyük albüm kapağı.
- `MediaTile`: Spotify tarzı kompakt kart.
- `Gallery`: kapak galerisi.
- `FilmStrip`: yatay video/kapak şeridi.

## Yeni örnek

Example Center içinde:

- `Gridly 5.1 Audix Pilot`
- `GridlyV51RealUsagePilotSampleForm`

Bu ekranda ses ve video kayıtları, eksik kapak placeholder'ı, FLAC/MP3/1080p/4K rozetleri, now-playing ve equalizer göstergesi birlikte gösterilir.
