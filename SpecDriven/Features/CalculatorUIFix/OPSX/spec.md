# Technical Specification: Calculator UI Fix

## Overview

**Feature ID**: FEAT-003  
**Feature Name**: CalculatorUIFix  
**Target**: Fix UI rendering and keypad behavior for `Pages/Calculator.cshtml`  
**Runtime**: .NET 10  
**UI Framework**: Bootstrap (local files under `wwwroot/lib/bootstrap`)  

---

## Problem

`Calcuator/Pages/Calculator.cshtml` is a Razor Page. The project currently has an MVC layout at `Calcuator/Views/Shared/_Layout.cshtml`, but there is:
- no `Calcuator/Pages/Shared/_Layout.cshtml`
- no `Calcuator/Pages/_ViewStart.cshtml`

As a result, the Calculator page can be rendered without a layout, causing:
- Bootstrap CSS/JS not loaded → UI looks "spartana"
- `@section Scripts` not rendered → keypad JavaScript functions not available
- Clicking keypad buttons does not update the display
- Clicking `=` may lead to broken client-side flow and invalid form post

---

## Goals

1. Ensure Razor Pages have a proper shared layout.
2. Load Bootstrap locally in the Razor Pages layout.
3. Ensure Razor Pages layout renders `@section Scripts` and `@section Styles`.
4. Make keypad JavaScript run (so display updates on button clicks).
5. Preserve navbar behavior (Calculator/Home/Privacy) consistently between MVC and Razor Pages.

---

## Non-Goals

- Rewriting calculator business logic (`CalculatorService`).
- Converting Calculator page to MVC.
- Adding new features to calculator UI.

---

## Implementation Requirements

### RQ-01: Create Razor Pages layout
Create `Calcuator/Pages/Shared/_Layout.cshtml`.

**Source**: copy from `Calcuator/Views/Shared/_Layout.cshtml` to ensure consistency.

**Must include**:
- Bootstrap CSS reference: `~/lib/bootstrap/dist/css/bootstrap.min.css`
- Bootstrap JS reference: `~/lib/bootstrap/dist/js/bootstrap.bundle.min.js`
- `@RenderBody()`
- `@await RenderSectionAsync("Styles", required: false)` in `<head>`
- `@await RenderSectionAsync("Scripts", required: false)` before `</body>`
- navbar links to Calculator (Razor Page), Home/Privacy (MVC)
- `GetActiveClass` helper function

### RQ-02: Configure Razor Pages to use the layout
Create `Calcuator/Pages/_ViewStart.cshtml` with:
```cshtml
@{
    Layout = "_Layout";
}
```

### RQ-03: No changes required to Calculator page logic
`Calcuator/Pages/Calculator.cshtml` already contains `@section Scripts`.
This spec assumes the keypad logic is correct once `@section Scripts` is actually rendered by the layout.

---

## Acceptance Criteria

- AC-01: Navigating to `/Calculator` loads Bootstrap styles (buttons/grids styled).
- AC-02: No console error like `ReferenceError: appendToDisplay is not defined`.
- AC-03: Clicking numeric keypad buttons updates the display.
- AC-04: Clicking `=` posts to server and returns a result (or a valid error message).
- AC-05: Navbar is visible on `/Calculator` and works correctly.

---

## Files to Create/Modify

### Create
- `Calcuator/Pages/Shared/_Layout.cshtml`
- `Calcuator/Pages/_ViewStart.cshtml`

### No change expected
- `Calcuator/Pages/Calculator.cshtml`
- `Calcuator/Pages/Calculator.cshtml.cs`

---

## Validation Steps

1. Build solution.
2. Run app.
3. Open `/Calculator`.
4. Verify UI styling (Bootstrap).
5. Verify keypad updates display.
6. Verify `=` works and shows result.

---

## Risks

- If another `Pages/_ViewStart.cshtml` existed with different layout settings, it could conflict; current state verified as missing.
- If Bootstrap local path differs, references must match actual `wwwroot` structure.

---

**Status**: Ready for apply
