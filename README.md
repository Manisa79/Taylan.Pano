# Gridly

**Gridly** is a modern WinForms grid/list/card control for .NET Windows desktop apps. It started as an ObjectListView-style replacement and grew into a themed, virtualized, multi-view data presentation control.

> Current release: **1.0.52.2 Community Preview**

Gridly is still in Community Preview. The API is usable, but feedback from real projects may still shape naming, defaults and module boundaries.

## Highlights

- Classic grid/list view
- Dashboard and DetailCard views
- Poster, Gallery, MediaTile and FilmStrip media views
- Audix-style album cover and media playback state support
- Dark/Light/System theme support
- Theme accessibility and contrast hardening
- Column filtering, sorting, icons and badges
- Layout/profile helpers
- Example/TestApp separated from the core DLL
- Language selector in TestApp

## Repository layout

```text
Gridly/
├─ src/
│  └─ Gridly/                 # Core DLL only
├─ samples/
│  └─ Gridly.TestApp/         # Showcase, examples and developer center
├─ docs/                      # API notes, migration docs and feature history
├─ release/                   # Release notes
├─ assets/                    # Logo/assets
├─ tools/                     # Quality/stress helper tools
└─ .github/                   # CI and issue templates
```

## Getting started

```csharp
using Gridly.Core;

var gridly = new GridlyView();
gridly.Dock = DockStyle.Fill;
gridly.ViewMode = GridlyViewMode.Details;
gridly.SetObjects(items);
Controls.Add(gridly);
```

## Media view example

```csharp
gridly.ApplyAudix51MediaPilotDefaults();
gridly.ViewMode = GridlyViewMode.Poster;
gridly.SetObjects(trackList);
```

A media item can expose fields such as title, artist, album, duration and cover image. The TestApp includes Poster, Gallery, MediaTile, FilmStrip and playback-state examples.

## Localization

TestApp includes a startup language selection screen. Host apps can apply localization globally:

```csharp
GridlyLocalization.Use("tr-TR");
```

## Documentation

The full user/developer guide is available in both languages:

- [Turkish guide](docs/Gridly_Professional_Developer_User_Guide_v1.0.52.2_TR.docx)
- [English guide](docs/Gridly_Professional_Developer_User_Guide_v1.0.52.2_EN.docx)
- [Documentation index](docs/USER_GUIDE_INDEX.md)
- [Screenshot placement audit](docs/Gridly_Full_Image_Audit_v1.0.52.2.md)
- [Publish readiness report](GITHUB_PUBLISH_READY_REPORT.md)

These documents use inline screenshots that were reviewed against their target headings.

## Build

```bash
dotnet restore Gridly.sln
dotnet build Gridly.sln -c Release
```

Requirements:

- Windows
- .NET SDK with Windows Forms support
- Visual Studio 2022 or newer recommended

## Samples

All samples live in `samples/Gridly.TestApp`. The core DLL intentionally does not contain Example Center or Developer Center forms.

Run the TestApp to explore:

- Basic list/grid views
- Media views
- Theme Lab
- Audix pilot sample
- Developer samples
- Performance/stress helpers

## Status

This is a **Community Preview**. Recommended release label:

```text
Gridly 1.0.52.2 Community Preview
Tag: v1.0.52.2-preview
```

## Contributing

Please open issues for bugs, theme/accessibility problems, performance reports and feature suggestions. See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

MIT License. See [LICENSE](LICENSE).

## Documentation Capture Mode

Gridly v1.0.52.2 includes a documentation screenshot workflow inside `samples/Gridly.TestApp`.

Open:

```text
Documentation / Documentation Capture Mode
```

Then select the Example Center screens to capture. The tool generates:

```text
docs/screenshots/*.png
docs/screenshots/gridly-screenshot-manifest.json
docs/screenshots/gridly-screenshots.md
docs/screenshots/gridly-docx-insert-map.json
```

To append generated screenshots into the Word guide:

```bash
python tools/docs/insert_screenshots_into_docx.py \
  --docx docs/Gridly_Professional_Developer_User_Guide.docx \
  --screenshots docs/screenshots \
  --output docs/Gridly_Professional_Developer_User_Guide_with_Screenshots.docx
```

## GitHub publish readiness

This repository is prepared as a source-first Community Preview package:

- generated Visual Studio and build outputs are excluded from the release source package,
- the core library remains under `src/Gridly`,
- showcase and developer examples remain under `samples/Gridly.TestApp`,
- release metadata is aligned to `1.0.52.2`,
- CI builds on Windows because WinForms requires Windows targeting.

Before creating a public GitHub release, run the local release checklist in `RELEASE_CHECKLIST.md`.
For NuGet packaging, pass your final repository URL as an MSBuild property:

```powershell
dotnet pack src/Gridly/Gridly.csproj -c Release -o artifacts/nuget /p:GridlyRepositoryUrl=https://github.com/Manisa79/Gridly
```
