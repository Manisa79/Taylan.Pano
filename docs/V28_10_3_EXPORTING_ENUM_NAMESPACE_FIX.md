# Gridly v28.10.3 - Exporting Enum Namespace Fix

Bu paket v28.10.2 sonrasında PDF export örneğinde görülen namespace gölgeleme hatasını düzeltir.

## Düzeltilen hata

TestApp içinde bazı formlarda `Gridly` isimli instance alanı bulunduğu için şu kullanım yanlış çözülüyordu:

```csharp
Gridly.Exporting.GridlyPdfExportMode.Card
```

Derleyici bunu namespace olarak değil, `GridlyView.Exporting` facade property’si olarak yorumlayabiliyordu.

## Düzeltme

PDF örnekleri artık global namespace ile çağrılır:

```csharp
new global::Gridly.Exporting.GridlyPdfExportOptions
{
    Mode = global::Gridly.Exporting.GridlyPdfExportMode.Card,
    Orientation = global::Gridly.Exporting.GridlyPdfPageOrientation.Portrait
};
```

## Desteklenen kullanım

```csharp
grid.ExportVisiblePdf(path, options);
grid.Exporting.ExportVisiblePdf(path, options);
```

`GridlyPdfExportMode`, `GridlyPdfPageOrientation` ve `GridlyPdfExportOptions` tipleri `Gridly.Exporting` namespace’i altındadır.
