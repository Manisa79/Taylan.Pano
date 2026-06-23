# Gridly API Reference

Bu dosya Gridly'nin ana ürün API yüzeyini tek yerde toplar.

## Veri Yükleme

```csharp
grid.SetObjects(rows);
await grid.SetObjectsAsync(async ct => await LoadRowsAsync(ct));
```

## Görünüm Modları

```csharp
grid.ViewMode = GridlyViewMode.Details;
grid.ViewMode = GridlyViewMode.Card;
grid.ViewMode = GridlyViewMode.Dashboard;
grid.ViewMode = GridlyViewMode.Poster;
```

## Profil Sistemi v29

Tek profil modeli `GridlyLayoutProfile` kabul edilir. Eski `GridlyColumnProfile` kaldırılmıştır.

```csharp
grid.Profiles.Save("Technician");
grid.Profiles.Load("Technician");
grid.Profiles.Export("Technician.gridlyprofile");
grid.Profiles.Import("Technician.gridlyprofile");
grid.Profiles.MigrateLegacyProfiles();
```

## Filtreleme

- Header popup filtre
- Card/Dashboard kolon seçerek filtreleme
- Advanced filter builder
- Preset filtreler
- Quick filter bar

## Export

```csharp
grid.ExportVisiblePdf(path, options);
grid.Exporting.ExportVisiblePdf(path, options);
```

## Ürünleşme Notu

Her release öncesi `build/Build-Gridly.ps1 -Configuration Release -Pack` çalıştırılmalıdır.
