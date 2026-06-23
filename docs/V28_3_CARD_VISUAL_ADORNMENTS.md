# Gridly v28.3 - Card Visual Adornments

Bu sürümde Card/Tile/Dashboard/Kanban/Timeline görünümleri için genel amaçlı görsel eklenti altyapısı eklendi.
Amaç AOI Support Desk gibi tek bir projeye özel durum noktası çizmek değil; stok, üretim, sipariş, dosya listesi, mesaj/ticket ekranı gibi tüm kart senaryolarında aynı Gridly renderer altyapısını kullanabilmek.

## Yeni API

- `CardVisualAdornments`: Kart görsel eklentilerini aç/kapat.
- `CardVisualInfoGetter`: Satır bazlı `GridlyCardVisualInfo` döndürür.
- `CardDefaultAccentMode`: Varsayılan accent çizimi. `Auto`, `TopBar`, `LeftBar`, `BottomBar`, `Outline`, `Glow`, `None`.
- `CardAutoBadgesFromBadgeColumns`: `GridlyColumnKind.Badge` kolonlarını kart üzerinde otomatik rozet olarak gösterir.
- `CardBadgeSize`: Kart rozet boyutu.
- `CardBadgeMaxCount`: Kart üzerinde çizilecek maksimum rozet sayısı.

## Örnek

```csharp
gridly.CardVisualInfoGetter = row =>
{
    TicketRow ticket = (TicketRow)row;

    GridlyCardVisualInfo info = new GridlyCardVisualInfo
    {
        AccentColor = ticket.StatusColor,
        DotColor = ticket.StatusColor,
        AccentMode = GridlyCardAccentMode.TopBar
    };

    if (ticket.UnreadMessageCount > 0)
    {
        info.Badges.Add(new GridlyCardBadge
        {
            Text = ticket.UnreadMessageCount.ToString(),
            Glyph = GridlyCardGlyph.Message,
            BackColor = Color.Orange,
            Placement = GridlyCardBadgePlacement.TopRight
        });
    }

    if (ticket.IsCritical)
    {
        info.Badges.Add(new GridlyCardBadge
        {
            Glyph = GridlyCardGlyph.Warning,
            BackColor = Color.Firebrick,
            Placement = GridlyCardBadgePlacement.TopLeft
        });
    }

    return info;
};
```

## Tasarım Notu

Details görünümündeki hücre renderer artık tek kaynak değildir. Kart/dash görünümlerinde status dot, accent bar, ikon badge ve otomatik badge kolonları doğrudan kart renderer içinde çizilir.
