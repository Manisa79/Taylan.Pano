# Gridly v29.6 - Media Library Views

Bu güncelleme Gridly kart/poster altyapısını medya listeleri için güçlendirir.

## Eklenenler

- `GridlyViewMode.MediaTile`: Albüm kapağı, film afişi, öğrenci/makine fotoğrafı gibi kompakt medya kutuları.
- `GridlyViewMode.FilmStrip`: Büyük görsel + sağ tarafta metin alanı olan yatay medya şeridi.
- `GridlyMediaImageScaleMode`: `Contain`, `Cover`, `Stretch` görsel yerleşim seçenekleri.
- `MediaImageScaleMode` ve `MediaImageRoundedCorners` propertyleri.
- Poster çiziminde yuvarlatılmış görsel kırpma ve sınır çizgisi.
- Example Center içine `Media Library / Albüm-Film-Fotoğraf` örneği.

## Kullanım

```csharp
gridly.Columns.Add(new GridlyColumn("Kapak", nameof(Row.Cover))
{
    Kind = GridlyColumnKind.Image,
    ImageGetter = row => ((Row)row).CoverImage
});

gridly.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
gridly.SetViewMode(GridlyViewMode.Poster);
// veya
gridly.SetViewMode(GridlyViewMode.MediaTile);
gridly.SetViewMode(GridlyViewMode.FilmStrip);
```

## Örnek senaryolar

- Audix: albüm kapağı + şarkı/sanatçı/albüm/süre.
- Film arşivi: poster + başlık/tür/yıl/rating.
- AOI Workspace: hata fotoğrafı veya komponent karesi + ticket bilgisi.
- Factory Navigator: makine fotoğrafı + hat/durum.
- Bilge Defter: öğrenci fotoğrafı + sınıf/durum.
