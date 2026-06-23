# Gridly v51.2 - Core / Examples Separation

This update prepares Gridly for a cleaner GitHub/community preview structure.

## What changed

- Gridly core remains in `src/Gridly` as a reusable WinForms control library.
- Example/demo screens remain in `samples/Gridly.TestApp` only.
- The old detached `samples/Gridly.FeatureSamples` folder was moved under `samples/Gridly.TestApp/Snippets/FeatureSamples`.
- Detached productization sample notes were moved under `samples/Gridly.TestApp/Snippets/ProductizationSamples`.
- Legacy snippet files are excluded from TestApp compilation because they are copy/paste recipes and contain repeated demo model names.
- Core descriptions no longer reference Example Center cleanup or developer menus.

## Expected repository layout

```text
Gridly
├─ src
│  └─ Gridly                 # production DLL
├─ samples
│  └─ Gridly.TestApp         # example/developer app only
├─ docs
└─ assets
```

## Rule

Anything that is only for demonstration belongs in `Gridly.TestApp`, not in `Gridly.dll`.

`Gridly.dll` may still contain reusable runtime UI such as filter dialogs, command palette, column chooser and theme helpers because those are product features, not examples.
