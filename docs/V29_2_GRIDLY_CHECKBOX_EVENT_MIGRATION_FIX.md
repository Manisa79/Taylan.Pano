# V29.2 Gridly checkbox event migration fix

Bu paket, OLV/ListView tarafındaki `ItemCheck` kullanımının `FastGridlyView` üzerinde doğru event modeliyle karşılanması için migration örneğini günceller.

## Net karar

`FastGridlyView` içinde `ItemCheck` yoktur ve eklenmemelidir. Gridly tarafında checkbox değişimi için önerilen yol:

- `CellValueChanged`: checkbox/toggle değeri değiştiğinde kullanılır.
- `CellClick`: sadece tıklama koordinasyonu gerekiyorsa kullanılır.
- `ItemChecked` / `ObjectChecked`: GLV compatibility event'i olarak satır checked state değişimini bildirir.

## Eklenen güvenli yardımcılar

`GridlyCellClickEventArgs` içine:

```csharp
bool IsCheckBoxColumn { get; }
```

`GridlyCellEditEventArgs` içine:

```csharp
bool? NewValueAsBoolean { get; }
```

Böylece uygulama tarafında kolon türü ve yeni değer güvenli şekilde kontrol edilir.

## MasterData/AOI gibi projelerde önerilen kullanım

```csharp
glvPanel.CellValueChanged += GlvPanel_CellValueChanged;

private void GlvPanel_CellValueChanged(object? sender, GridlyCellEditEventArgs e)
{
    if (!e.IsCheckBoxColumn) return;

    bool isChecked = e.NewValueAsBoolean == true;

    if (isChecked)
    {
        // Tek seçim mantığı: diğer model satırlarının checked alanını false yap.
        // Sonra grid.RefreshObjects(...) ile görseli yenile.
    }
}
```

## Kontrol edilen benzer durumlar

- Kaynakta `FastGridlyView.ItemCheck` veya `glvPanel.ItemCheck` benzeri hatalı kullanım bulunmadı.
- Kalan `.ItemCheck +=` kullanımları `CheckedListBox` tabanlı popup/chooser listelerine aittir; bunlar Gridly event'i değildir ve doğrudur.
- `(MethodInvoker)delegate` kullanımı kaynakta bulunmadı. Ambiguous riskli yerlerde uygulama tarafında `System.Windows.Forms.MethodInvoker` veya alias kullanılmalıdır.
