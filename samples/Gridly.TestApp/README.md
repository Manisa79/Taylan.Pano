# Gridly.TestApp

Gridly.TestApp is the only place where showcase screens, demo data, developer samples and legacy copy/paste snippets live.

The production DLL project (`src/Gridly/Gridly.csproj`) should remain a clean control/API package:

- no Example Center forms,
- no demo menus,
- no application-specific sample screens,
- no test-only navigation shell.

## Folder policy

- `samples/Gridly.TestApp/*.cs`: compiled demo forms and the developer/example center.
- `samples/Gridly.TestApp/Samples`: compiled helper samples used by TestApp.
- `samples/Gridly.TestApp/Snippets/FeatureSamples`: legacy reference snippets moved from the old detached `samples/Gridly.FeatureSamples` folder. These files are intentionally excluded from compilation to avoid duplicate demo model names.
- `samples/Gridly.TestApp/Snippets/ProductizationSamples`: markdown productization examples moved from the old detached samples folder.

Host applications such as Audix, AOI Support Desk, Line Workspace, MasterData and Bilge Defter should reference only `Gridly.dll` and should not show any Gridly developer/sample menu unless they explicitly add their own developer screen.
