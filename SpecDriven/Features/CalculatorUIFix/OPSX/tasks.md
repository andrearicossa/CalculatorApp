# Implementation Tasks: Calculator UI Fix

## Task Breakdown

### TASK-01: Create Pages/Shared Directory
**Priority**: High  
**Estimated Time**: 2 minutes

**Steps**:
1. Open terminal/PowerShell
2. Run: `New-Item -ItemType Directory -Force -Path "Calcuator/Pages/Shared"`

**Acceptance Criteria**:
- Directory `Calcuator/Pages/Shared/` exists

---

### TASK-02: Copy Layout from Views to Pages
**Priority**: High  
**Estimated Time**: 15-20 minutes  
**Dependencies**: TASK-01

**Steps**:
1. Open `Calcuator/Views/Shared/_Layout.cshtml`
2. Select All (Ctrl+A) and Copy (Ctrl+C)
3. Create new file `Calcuator/Pages/Shared/_Layout.cshtml`
4. Paste content
5. **Verify Bootstrap references**:
   - CSS: `<link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />`
   - JS: `<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>`
6. **Verify sections**:
   - `@await RenderSectionAsync("Styles", required: false)` in <head>
   - `@await RenderSectionAsync("Scripts", required: false)` before </body>
7. **Verify navbar** includes:
   - Calculator, Home, Privacy links
   - `@functions` block with `GetActiveClass` method
8. Save file

**Acceptance Criteria**:
- File `Pages/Shared/_Layout.cshtml` exists
- Bootstrap references present and correct (local paths)
- Navbar includes Calculator link
- RenderSectionAsync for Styles and Scripts present
- GetActiveClass helper function present
- File compiles without errors

---

### TASK-03: Create Pages/_ViewStart.cshtml
**Priority**: High  
**Estimated Time**: 2 minutes  
**Dependencies**: TASK-02

**Steps**:
1. Create new file `Calcuator/Pages/_ViewStart.cshtml`
2. Add content:
   ```csharp
   @{
       Layout = "_Layout";
   }
   ```
3. Save file

**Acceptance Criteria**:
- File `Pages/_ViewStart.cshtml` exists
- Contains Layout assignment to "_Layout"
- File compiles without errors

---

### TASK-04: Verify Calculator.cshtml (Inspection Only)
**Priority**: Medium  
**Estimated Time**: 5 minutes  
**Dependencies**: None (can run in parallel)

**Steps**:
1. Open `Calcuator/Pages/Calculator.cshtml`
2. Verify `@section Scripts` exists at end of file
3. Verify JavaScript functions present:
   - `appendToDisplay`
   - `setOperation`
   - `clearForm`
   - `updateDisplay`
4. Verify `@section Styles` exists if custom CSS present
5. **No changes needed** - just verification

**Acceptance Criteria**:
- `@section Scripts` present with keypad JavaScript
- JavaScript code looks correct
- No syntax errors

---

### TASK-05: Build and Run Application
**Priority**: High  
**Estimated Time**: 3 minutes  
**Dependencies**: TASK-01, TASK-02, TASK-03

**Steps**:
1. Stop application if running (Shift+F5)
2. Clean solution (Build → Clean Solution)
3. Build solution (Ctrl+Shift+B)
4. Verify no build errors
5. Run application (F5)

**Acceptance Criteria**:
- Solution builds successfully
- No compilation errors
- Application starts without exceptions

---

### TASK-06: Manual Testing - Visual Verification
**Priority**: High  
**Estimated Time**: 5 minutes  
**Dependencies**: TASK-05

**Test Steps**:
1. Navigate to `https://localhost:<port>/Calculator`
2. **Verify Bootstrap styles**:
   - Buttons have color (primary blue, warning orange, etc.)
   - Grid layout properly spaced
   - Navbar visible with links
   - Typography formatted (not plain text)
3. **Verify navbar**:
   - Calculator, Home, Privacy links visible
   - Calculator link highlighted (active class)
4. Take screenshot if needed for documentation

**Acceptance Criteria**:
- Page has professional appearance (not "spartana")
- Bootstrap styles clearly applied
- Navbar functional and styled
- No layout issues

---

### TASK-07: Manual Testing - Keypad Functionality
**Priority**: High  
**Estimated Time**: 5 minutes  
**Dependencies**: TASK-06

**Test Steps**:
1. On Calculator page, click "7" button
   - **Expected**: Display shows "7"
2. Click "5" button
   - **Expected**: Display shows "75"
3. Click "." button
   - **Expected**: Display shows "75."
4. Click "3" button
   - **Expected**: Display shows "75.3"
5. Click "+" button
   - **Expected**: Operation stored (may not be visible, but no error)
6. Click "2" button
   - **Expected**: Display shows "2"

**Acceptance Criteria**:
- All numeric buttons (0-9) update display
- Decimal point button works
- Operation buttons work without error
- No JavaScript console errors

---

### TASK-08: Manual Testing - Calculation
**Priority**: High  
**Estimated Time**: 5 minutes  
**Dependencies**: TASK-07

**Test Steps**:
1. Clear display if needed (Clear button)
2. Enter: 10 + 5 (using keypad)
3. Click "=" button
4. **Expected Result**: Display shows "15.00"
5. Test other operations:
   - 10 - 5 → "5.00"
   - 10 * 5 → "50.00"
   - 10 / 5 → "2.00"
   - 10 / 3 → "3.33" (rounded)
6. Test division by zero:
   - 10 / 0 → Error message "Cannot divide by zero"

**Acceptance Criteria**:
- "=" button submits form without error
- Calculations return correct results
- Results formatted to 2 decimals
- Error messages displayed correctly
- No server-side exceptions

---

### TASK-09: Browser Console Verification
**Priority**: Medium  
**Estimated Time**: 3 minutes  
**Dependencies**: TASK-06

**Steps**:
1. Open Browser DevTools (F12)
2. Go to Console tab
3. Refresh /Calculator page
4. **Check for errors**:
   - No "ReferenceError: appendToDisplay is not defined"
   - No 404 errors for Bootstrap files
   - No JavaScript exceptions
5. Go to Network tab
6. Verify Bootstrap files load:
   - bootstrap.min.css (Status 200)
   - bootstrap.bundle.min.js (Status 200)

**Acceptance Criteria**:
- Console clean (no errors)
- Bootstrap files loaded successfully (200 status)
- No 404 errors

---

### TASK-10: Test Other Pages (Optional)
**Priority**: Low  
**Estimated Time**: 5 minutes  
**Dependencies**: TASK-05

**Steps**:
1. Navigate to /Home/Index
   - Verify still works (uses Views layout)
2. Navigate to /Home/Privacy
   - Verify still works
3. Navigate back to /Calculator
   - Verify navbar highlights Calculator
4. Use navbar to navigate between pages
   - Verify navigation works smoothly

**Acceptance Criteria**:
- No breaking changes to existing pages
- Navbar navigation functional
- Active state updates correctly

---

### TASK-11: Documentation Update (Optional)
**Priority**: Low  
**Estimated Time**: 5 minutes  
**Dependencies**: All previous tasks

**Steps**:
1. Open `SpecDriven/Features/Calculator.md` or relevant doc
2. Add note about layout fix:
   ```markdown
   ## 🔄 EVOLUZIONI (FEAT-003 - CalculatorUIFix)
   
   **Feature Aggiunta**: Layout Razor Pages
   - Creato Pages/Shared/_Layout.cshtml per supporto Bootstrap
   - Creato Pages/_ViewStart.cshtml per configurazione automatica
   - Risolto bug: tastierino non funzionante
   - Risolto bug: stili Bootstrap non applicati
   ```
3. Save changes

**Acceptance Criteria**:
- Documentation updated with fix notes

---

## Task Summary

| Phase | Tasks | Estimated Time |
|-------|-------|----------------|
| Implementation | TASK-01 to TASK-03 | ~20 minutes |
| Build | TASK-04 to TASK-05 | ~8 minutes |
| Testing | TASK-06 to TASK-09 | ~18 minutes |
| Verification | TASK-10 to TASK-11 | ~10 minutes (optional) |
| **TOTAL** | **11 tasks** | **~30-45 minutes** |

---

## Critical Path

```
TASK-01 (Create directory)
   ↓
TASK-02 (Copy layout) ← Most important
   ↓
TASK-03 (Create _ViewStart)
   ↓
TASK-05 (Build & Run)
   ↓
TASK-06 (Visual test)
   ↓
TASK-07 (Keypad test)
   ↓
TASK-08 (Calculation test)
```

**TASK-04** (Verify Calculator.cshtml) can run in parallel with TASK-01/02

---

## Definition of Done

- [ ] Pages/Shared/_Layout.cshtml exists with Bootstrap and navbar
- [ ] Pages/_ViewStart.cshtml exists with Layout config
- [ ] Application builds without errors
- [ ] /Calculator page has Bootstrap styles
- [ ] Keypad buttons update display
- [ ] Calculations execute without errors
- [ ] No JavaScript console errors
- [ ] No browser network errors (404s)

---

**Status**: Ready for Implementation  
**Next Step**: Start with TASK-01
