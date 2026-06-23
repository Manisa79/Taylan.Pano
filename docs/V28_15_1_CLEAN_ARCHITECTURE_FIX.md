# Gridly v28.15.1 - Clean Architecture Build Fix

- `GridlyColumnProfile` duplicate type conflict resolved.
- Data profiling model was renamed to `GridlyDataColumnProfile`.
- Existing column layout/profile API remains `GridlyColumnProfile` for compatibility.
- Ultimate data profiling APIs now return `GridlyDataColumnProfile`.
- No feature removal: Query, Expression, Event Bus, Change Tracking, Layout Package, Smart Suggestions and Data Profiling remain available.
