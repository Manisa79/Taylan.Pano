# Gridly v51.8 - Documentation Capture Scenario Matrix

Bu sürüm, TestApp içindeki Documentation Capture Mode'u sadece ana örnek ekranı yakalayan bir yapı olmaktan çıkarıp özellik/senaryo bazlı ekran görüntüsü üretim merkezine dönüştürür.

## Eklenenler

- Capture listesi kategori + özellik + senaryo formatına geçirildi.
- Aynı örnek formdan birden fazla senaryo yakalanabilir hale getirildi.
- Media Library için Poster, Gallery, MediaTile ve FilmStrip ayrı PNG olarak üretilebilir.
- Audix Pilot için Now Playing ve FilmStrip senaryoları ayrı yakalanır.
- Media Playback için Playing, Paused, Loading/Error senaryoları ayrı listelenir.
- Filter Popup UX için temel popup, uzun değerler ve resize senaryoları ayrıldı.
- Theme Audit için Dark, Light ve High Contrast/Accessibility senaryoları ayrıldı.
- Pro Experience için Layout, Performance ve Analytics senaryoları ayrıldı.
- `gridly-screenshots.md` artık kategori başlıklarıyla üretilir.
- `gridly-docx-insert-map.json` içine Category ve ScenarioName alanları eklendi.

## Davranış

Capture motoru, form içindeki sekme, combo, buton ve liste öğelerini metin veya isim üzerinden genel olarak arar. Uygun kontrol bulunursa ilgili senaryo seçilir; bulunamazsa ekran yine varsayılan haliyle yakalanır. Böylece örnek formlar değişse bile dokümantasyon capture akışı kırılmadan çalışır.

## Çıktı örnekleri

```text
docs/screenshots/
├─ gridly-view-details.png
├─ gridly-view-dashboard.png
├─ gridly-media-library-mediatile.png
├─ gridly-audix-now-playing.png
├─ gridly-playback-paused.png
├─ gridly-theme-audit-dark.png
└─ gridly-filter-popup-long-values.png
```

## Amaç

Gridly Professional Developer & User Guide dokümanında her özellik için gerçek ekran görüntüsü kullanılabilmesini sağlamak.
