# Gridly v1.0.52.2 - Media Scenarios + Compile Cleanup

Bu paket `Gridly_v51_9_MediaScenariosDetailCard.zip` referans alınarak hazırlanmıştır.

## Uygulanan son değişiklikler

- v51.9 Media Scenarios / DetailCard içerikleri korundu.
- `GridlyView.ApplyScenario(GridlyViewScenario)` tek parametreli overload yeniden eklendi.
- `GridlyView.ApplyScenario(GridlyViewScenario, bool updateActiveScenario)` davranışı düzeltildi.
  - `updateActiveScenario = true` ise `ActiveScenario` güncellenir.
  - `updateActiveScenario = false` ise aktif senaryo property değeri korunur.
- Eski Test.App örneklerindeki `_grid.ApplyScenario(scenario)` çağrılarıyla uyumluluk sağlandı.
- Gridly ve Test.App sürümü `1.0.52.2` yapıldı.

## Not

Bu çalışma ortamında .NET SDK yüklü olmadığı için gerçek `dotnet build` çalıştırılamadı. Kaynak üzerinde statik derleme temizliği ve sürüm güncellemesi uygulanmıştır.
