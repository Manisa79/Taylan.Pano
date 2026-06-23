# Gridly v28.7 - PDF Export Suite

Bu sürüm Gridly export altyapısına gerçek PDF çıktısı ekler.

## Eklenenler

- Details/Table PDF export
- Card/Dashboard PDF export
- Header / footer / sayfa numarası
- Kayıt sayısı ve tarih bilgisi
- Fit-to-page kolon ölçekleme
- Zebra row desteği
- Grid line aç/kapat
- Print-friendly tema
- Card accent bar, status dot ve badge çıktısı
- Card layout definition ile PDF kart alan sıralaması
- Büyük veri için MaxRows limiti
- Dış paket bağımlılığı olmadan PDF üretimi

## Kullanım

```csharp
grid.ExportVisiblePdf(@"C:\Temp\tickets.pdf", new GridlyPdfExportOptions
{
    Title = "AOI Support Desk Tickets",
    Mode = GridlyPdfExportMode.Table,
    Orientation = GridlyPdfPageOrientation.Landscape,
    FitToPageWidth = true,
    ShowGridLines = true,
    ZebraRows = true
});
```

Card/Dashboard çıktısı:

```csharp
grid.ExportVisiblePdf(@"C:\Temp\dashboard.pdf", new GridlyPdfExportOptions
{
    Title = "Dashboard",
    Mode = GridlyPdfExportMode.Card,
    CardColumns = 2,
    CardMinHeight = 105,
    CardVisualInfoResolver = row => new GridlyCardVisualInfo
    {
        AccentColor = Color.DodgerBlue,
        DotColor = Color.DodgerBlue,
        Badges = { new GridlyCardBadge { Text = "SAP", BackColor = Color.SeaGreen } }
    }
});
```

Not: PDF içindeki metin Helvetica tabanlıdır; Türkçe karakterler güvenli ASCII karşılıklarına normalize edilir.
