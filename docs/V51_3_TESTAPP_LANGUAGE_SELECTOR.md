# Gridly v1.0.51.3 - TestApp Language Selector

## Amaç

GitHub / Community Preview öncesinde `Gridly.TestApp` başlangıcına dil seçimi eklendi.
Bu seçim Gridly'nin dahili menüleri, filtre pencereleri, kolon seçici, layout menüleri ve built-in dialog metinleri için tek merkezden uygulanır.

## Eklenenler

- `Gridly.Localization.GridlyLocalization` facade sınıfı
- `GridlyLocalization.SupportedLanguages`
- `GridlyLocalization.Use(GridlyLanguage language)`
- `GridlyLocalization.DisplayName(...)`
- `GridlyLocalization.FromName(...)`
- `samples/Gridly.TestApp/StartupLanguageForm`
- TestApp başlangıcında dil seçim ekranı
- Dil seçiminin `settings/gridly-testapp-language.json` içinde saklanması

## Desteklenen diller

- Auto / System
- Türkçe
- English
- Deutsch
- Français
- Español
- Italiano
- Русский
- العربية
- 中文
- 日本語

## Uygulamalarda kullanım

```csharp
using Gridly.Localization;

GridlyLocalization.Use(GridlyLanguage.Turkish);
```

veya tek bir GridlyView üzerinden:

```csharp
gridly.Language = GridlyLanguage.English;
```

Not: `GridlyView.Language` mevcut API ile uyumlu kalması için korunmuştur. Arka planda global Gridly localization dilini değiştirir.

## Kapsam

Bu seçim Gridly çekirdeğinin kendi ürettiği metinleri etkiler:

- Sağ tık / header menüleri
- Filtre penceresi metinleri
- Kolon seçici
- Layout ve grouping menü metinleri
- Built-in dialog metinleri

Uygulamanın kendi menüleri ve özel formları için aynı dili kullanmak istenirse uygulama tarafında `GridlyLocalization.T("Key")` veya kendi resource sistemi kullanılmalıdır.
