# Core / Examples Separation Report

Goal: keep `Gridly.dll` clean for product projects and move showcase/developer screens to `samples/Gridly.TestApp`.

## Current layout

- Core library: `src/Gridly`
- Sample application: `samples/Gridly.TestApp`
- Feature snippets and examples: `samples/Gridly.TestApp/Snippets`
- Documentation: `docs`

## Core policy

The core project must not contain:

- Example Center forms
- Developer Center forms
- TestApp startup forms
- Demo-only datasets
- Sample forms

All such files belong under `samples/Gridly.TestApp`.

## Static check performed

No `ExampleCenter`, `SampleForm`, `TestApp` or `FeatureSamples` references were found under `src/Gridly` during package preparation.
