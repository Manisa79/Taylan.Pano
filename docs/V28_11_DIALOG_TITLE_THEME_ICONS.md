# Gridly v28.11 - Dialog Title Theme & Icons

Gridly owned helper windows now use a shared dialog chrome and theme-aware title icons.

## Added

- Dark/light native title bar synchronization for Gridly dialogs.
- Theme-aware generated icons, no external PNG/ICO dependency.
- Dialog icon kinds: Grid, Filter, Column, Search, Export, Designer, Command, Info, Warning, Success, Error.
- Applied to filter window, column chooser, command palette, modern search panel, card layout designer, advanced filter builder, column designer and toast notifications.
- Existing windows still use `GridlyDialogChrome.ConfigureStandardDialog`; a new optional `iconKind` parameter controls the title icon.

## Usage

```csharp
GridlyDialogChrome.ConfigureStandardDialog(
    form,
    theme,
    new Size(480, 320),
    sizeable: true,
    iconKind: GridlyDialogIconKind.Filter);
```

The icon follows the active theme accent color and remains readable in dark and light modes.
