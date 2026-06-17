# Design: Calculator History

## Storage
- Use ASP.NET Core Session (in-memory) to persist history across POST reloads.
- Session key: `CalculatorHistory`.
- Store JSON array of entries: `{ a, op, b, result }`.

## Server changes
- Enable session in `Program.cs`:
  - `AddDistributedMemoryCache()`
  - `AddSession()`
  - `UseSession()`
- In `CalculatorModel.OnPost`, after successful calculation:
  - Read list from session
  - Append new entry
  - Save list back to session

- Provide a handler to clear history (invoked by Clear):
  - Option A: add `OnPostClearHistory` handler
  - Option B: add dedicated endpoint

## UI changes
- Add a `History` button near calculator UI.
- Add Bootstrap Modal markup in `Pages/Calculator.cshtml`.
- Modal body scrollable (`modal-dialog-scrollable`).
- List entries, show placeholder if empty.

## Clear behavior
- Existing Clear is client-side. It must also clear server session history:
  - Option: make Clear a POST to `?handler=Clear` then re-render.
  - Minimal: add hidden field to indicate clear action.

Recommended: add a small form/button that posts to `OnPostClear`.
