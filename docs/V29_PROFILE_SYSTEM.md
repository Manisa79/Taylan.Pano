# Gridly v29 Profile System

v29 ile profil sistemi tek modele indirildi:

- `GridlyColumnProfile` kaldırıldı.
- Tek kaynak: `GridlyLayoutProfile`.
- Dosya uzantısı: `.gridlyprofile`.
- `ProfileVersion = 29`.
- Kullanıcı / makine / rol kapsamı desteklenir.
- Eski `.json` kolon layout profilleri `GridlyProfileMigrator` ile otomatik dönüştürülebilir.

## Kullanım

```csharp
grid.SaveLayoutProfile("Technician", roleName: "Technician");
grid.LoadLayoutProfile("Technician", roleName: "Technician");
grid.ExportLayoutProfile(@"C:\Temp\Technician.gridlyprofile", "Technician");
grid.ImportLayoutProfile(@"C:\Temp\Technician.gridlyprofile", apply: true);
int migrated = grid.MigrateLegacyProfiles();
```

## Geriye uyumluluk

`SaveProfile`, `LoadProfile`, `ResetProfile` artık v29 layout profile API'sine yönlenir.
`GridlyColumnProfile` sınıfı bilinçli olarak kaldırıldı; yeni kod `GridlyLayoutProfile` kullanmalıdır.

## Build fix

Ultimate paketindeki iki ayrı `GridlyCellChange` modeli ayrıldı:

- Undo sistemi: `Gridly.Undo.GridlyCellChange`
- Change tracking: `GridlyTrackedCellChange`

Bu sayede `RowObject` / `Column` compile hatası giderildi.
