# Gridly v30.0 Visual Experience Suite

Bu sürüm Gridly'yi klasik tablo kontrolü çizgisinden çıkarıp medya, katalog, dashboard ve üretim ekranları için çok amaçlı görsel veri deneyimi kontrolüne taşır.

## Yeni / güçlendirilen görünüm tipleri

- `Poster`: albüm kapağı, film afişi, öğrenci fotoğrafı, makine görseli.
- `Gallery`: Windows Explorer / fotoğraf galerisi tarzı çoklu görsel katalog.
- `MediaTile`: kompakt albüm/şarkı/ekran görüntüsü kartları.
- `FilmStrip`: geniş yatay medya şeridi; ticket eki, AOI hata görseli, video kareleri.
- `DetailCard`: tüm görünür kolonları satır satır gösteren tam genişlik kart.
- `PropertyCard`: Visual Studio PropertyGrid benzeri etiket/değer kartı.
- `KpiDashboard`: Power BI tarzı metrik kartları.
- `HeatMap`: üretim, risk, doluluk veya performans yüzdesini ısı haritası kartı olarak gösterir.
- `MiniChart`: satır içinde küçük trend/sparkline hissi veren kompakt kart.
- `RowPreview`: Outlook benzeri başlık + açıklama önizleme satırı.
- `GroupCard`: hat/makine/grup katalogları için geniş kart.
- `Kanban`, `Timeline`, `MasterDetail`, `GroupedList`: önceki çekirdekle birlikte korunup genişletildi.

## Medya özellikleri

- `MediaImageScaleMode`: `Contain`, `Cover`, `Stretch`.
- `MediaImageRoundedCorners`: kart içi görsel köşelerini yuvarlatır.
- Poster/MediaTile/FilmStrip/Gallery aynı `ImageGetter` altyapısını kullanır.

## Kullanım örnekleri

```csharp
gridly.Columns.Add(new GridlyColumn("Kapak", "Cover", 120)
{
    Kind = GridlyColumnKind.Image,
    ImageGetter = row => ((MediaRow)row).CoverImage
});

gridly.Columns.Add(new GridlyColumn("Başlık", "Title", 220) { Editable = true });
gridly.Columns.Add(new GridlyColumn("Açıklama", "Description", 260));

gridly.TilePosterMode = true;
gridly.MediaImageScaleMode = GridlyMediaImageScaleMode.Cover;
gridly.SetViewMode(GridlyViewMode.Gallery);
```

## Önerilen proje eşleşmeleri

- Audix: Poster, Gallery, MediaTile, FilmStrip.
- AOI Support Desk: RowPreview, Kanban, Timeline, FilmStrip, DetailCard.
- Factory Navigator: GroupCard, HeatMap, KpiDashboard, MiniChart.
- Bilge Defter: Gallery, PropertyCard, RowPreview.
- Line Workspace: KpiDashboard, HeatMap, GroupCard, MasterDetail.

## Not

Bu paket içinde `dotnet` olmadığı için build bu ortamda çalıştırılamadı. `int` → `System.Drawing.Size` tip dönüşümüne sebep olabilecek medya boyutlandırma tarafındaki overload kullanımları kontrol edildi; yeni eklenen çizimlerde `Rectangle`, `Size.Empty` ve açık tipli ölçüm kullanıldı.
