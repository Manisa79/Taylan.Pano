# Gridly 5.0 Foundation / Stability

Bu paket yeni görünüm eklemekten çok Gridly'nin büyüyen mimarisini sağlamlaştırmak için hazırlandı.

## Amaç

Gridly artık sadece ListView/Grid alternatifi değil; medya, dashboard, timeline, kanban, tema, command palette, layout ve analytics özellikleri olan büyük bir görsel veri platformu haline geldi. Bu yüzden v5.0 Foundation paketi şu hedefleri toplar:

- Fazlar arası API/property çakışmalarını azaltmak.
- Audix, AOI Support Desk, FactoryOS, MasterData ve Bilge Defter için profil bazlı kullanım sağlamak.
- Koyu/açık tema okunurluğunu varsayılan olarak korumak.
- Audio/video playback state görünürlüğünü güvenli varsayılan haline getirmek.
- Example Center içinde yeni özellikleri daha kolay buldurmak.

## Yeni ana API'ler

```csharp
gridly.ApplyGridly5FoundationDefaults();
gridly.ApplyAudixMediaProfile();
gridly.ApplyAoiSupportDeskProfile();
gridly.ApplyFactoryIntelligenceProfile();
```

## Modül profilleri

```csharp
var profiles = gridly.GetGridly5ModuleProfiles();
```

Profiller:

- Audix Media
- AOI Support Desk
- Factory Intelligence
- MasterData
- Bilge Defter

## Runtime stability check

```csharp
var checks = gridly.RunGridly5RuntimeChecks();
```

Kontrol edilenler:

- Theme Accessibility
- Media Playback state
- Command Palette
- Aktif modül profili

## Audix için önerilen kullanım

```csharp
gridly.ApplyAudixMediaProfile();
gridly.SetViewMode(GridlyViewMode.Poster);
gridly.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
gridly.ShowMediaPlaybackState = true;
gridly.ShowMediaNowPlayingBadge = true;
gridly.ShowMediaEqualizerIndicator = true;
```

Video dosyalarında `MediaVideoPreviewMode = true` bırakılır. Gerçek video player/preview açma işi host uygulamada `MediaPlayPauseClicked` event'i içinde yapılmalıdır.

## Example Center

Yeni örnek:

- `Gridly 5.0 Foundation / Stability`

Bu ekranda profil butonları, runtime check ve koyu/açık tema hızlı testi bulunur.
