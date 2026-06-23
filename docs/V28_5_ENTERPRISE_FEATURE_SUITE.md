# Gridly v28.5 Enterprise Feature Suite

Bu sürüm Gridly'ye tek projeye özel olmayan, ileride farklı uygulamalarda da kullanılabilecek genel özellik katmanı ekler.

## Eklenen başlıklar

- Smart column metadata: `AutoFilterMode`, `DefaultFilterOperator`, `ShowTopValuesInFilter`, `SearchAlias`
- Column profiles: `CaptureColumnProfile`, `ApplyColumnProfile`, `SaveProfile`, `LoadProfile`, `ResetProfile`
- Conditional rule engine: `GridlyConditionalRule`, `AddConditionalRule`, `ClearConditionalRules`
- Card inline actions: `GridlyCardAction`, `CardActionClick`, card action glyph drawing
- Card visual model genişletmesi: `GridlyCardVisualInfo.Actions`
- Live mode hazırlığı: `LiveUpdateMode`, `LiveRefreshInterval`, `MarkRowChanged`
- Ultra fast profile flag: `UltraFastMode`
- Smart search parser: `status:open machine:LINE1 -closed` benzeri token parsing
- Column aggregate metadata: `GridlyAggregateMode`, `CalculateAggregate`
- Plugin altyapısı: `IGridlyPlugin`, `GridlyPluginCollection`
- Frozen column metadata: `GridlyColumn.Frozen`

## Örnek

`samples/Gridly.FeatureSamples/EnterpriseFeatureSuiteSamples.cs` içinde tüm yeni özellikleri aynı ekranda kullanan örnek eklendi.

## Not

Bu ortamda .NET SDK bulunmadığı için build alınamadı. Değişiklikler kaynak düzeyinde hazırlandı.
