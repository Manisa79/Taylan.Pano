# Contributing to Gridly

Thanks for trying Gridly. This repository is published as a Community Preview, so bug reports, screenshots and real-world usage feedback are very valuable.

## Before opening an issue

Please include:

- Gridly version
- .NET SDK / Visual Studio version
- Windows version
- Theme mode: Light, Dark, System or High Contrast
- View mode: List, Dashboard, DetailCard, Poster, Gallery, MediaTile, FilmStrip, etc.
- A small code sample or screenshot when possible

## Local build

```bash
dotnet restore Gridly.sln
dotnet build Gridly.sln -c Release
```

The core library lives under `src/Gridly`. All demo and showcase screens must stay under `samples/Gridly.TestApp`.

## Pull request rule

Please do not add sample/demo forms to `src/Gridly`. The DLL should stay clean and product-ready. Put all experiments under `samples/Gridly.TestApp`.
