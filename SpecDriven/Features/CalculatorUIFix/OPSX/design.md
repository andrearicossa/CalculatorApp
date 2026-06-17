# Design: Calculator UI Fix

## Problem Statement

Calculator.cshtml è una Razor Page ma non ha layout → no Bootstrap, JavaScript non caricato, interfaccia non funzionante.

## Solution Architecture

```
BEFORE (Broken):
┌─────────────────────────────────────┐
│ Browser GET /Calculator             │
│   ↓                                 │
│ Pages/Calculator.cshtml renderized  │
│ WITHOUT layout (missing!)           │
│   ↓                                 │
│ No Bootstrap CSS/JS loaded          │
│ No @section Scripts rendered        │
│ Tastierino JavaScript not executed  │
│   ↓                                 │
│ ❌ Broken UI                         │
└─────────────────────────────────────┘

AFTER (Fixed):
┌─────────────────────────────────────┐
│ Browser GET /Calculator             │
│   ↓                                 │
│ Pages/_ViewStart.cshtml             │
│ Sets Layout = "_Layout"             │
│   ↓                                 │
│ Pages/Shared/_Layout.cshtml         │
│ - Loads Bootstrap CSS/JS (local)    │
│ - Renders navbar                    │
│ - Renders @RenderBody()             │
│ - Renders @section Scripts          │
│   ↓                                 │
│ Pages/Calculator.cshtml content     │
│ + JavaScript in @section Scripts    │
│   ↓                                 │
│ ✅ Functional UI with Bootstrap      │
└─────────────────────────────────────┘
```

## Implementation Steps

### 1. Create Pages/Shared/_Layout.cshtml

**Source**: Copy from `Views/Shared/_Layout.cshtml`

**Key Components to Verify**:
```html
<!DOCTYPE html>
<html>
<head>
    <!-- Bootstrap CSS LOCAL -->
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    
    <!-- Custom styles support -->
    @await RenderSectionAsync("Styles", required: false)
</head>
<body>
    <header>
        <nav class="navbar...">
            <!-- Navbar with Calculator, Home, Privacy -->
        </nav>
    </header>
    
    <div class="container">
        <main>
            @RenderBody()
        </main>
    </div>
    
    <footer>...</footer>
    
    <!-- Bootstrap JS LOCAL -->
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    
    <!-- Custom scripts support -->
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>

@functions {
    string GetActiveClass(string? page, string? action = null)
    {
        // Helper for active menu item
    }
}
```

### 2. Create Pages/_ViewStart.cshtml

**Content**:
```csharp
@{
    Layout = "_Layout";
}
```

**Purpose**: Applies layout automatically to all Razor Pages

### 3. Verify Calculator.cshtml (No Changes Needed)

Calculator.cshtml already has:
- ✅ `@section Scripts` with keypad JavaScript
- ✅ Correct HTML structure
- ✅ Form binding properties

**No modification required** - just needs layout to render it correctly.

---

## File Changes Summary

| File | Action | Description |
|------|--------|-------------|
| `Pages/Shared/_Layout.cshtml` | **CREATE** | Copy from Views/Shared/_Layout.cshtml |
| `Pages/_ViewStart.cshtml` | **CREATE** | Set Layout = "_Layout" |
| `Pages/Calculator.cshtml` | **VERIFY** | No changes (already correct) |

---

## Testing Strategy

### Manual Test Checklist

1. ✅ **Visual Test**: Navigate to /Calculator
   - Verify Bootstrap styles applied (buttons colored, grid layout)
   - Verify navbar visible (Calculator, Home, Privacy)
   - Verify Calculator highlighted in menu

2. ✅ **Keypad Test**: Click numeric buttons
   - Click "7" → display shows "7"
   - Click "5" → display shows "75"
   - Click "." → display shows "75."
   - Click "3" → display shows "75.3"

3. ✅ **Operation Test**: Click operation buttons
   - Click "+" → operation stored
   - Enter second number → display updates

4. ✅ **Calculation Test**: Click "="
   - Form submits without error
   - Result calculated correctly
   - Display shows result with 2 decimals

5. ✅ **Error Test**: Test error scenarios
   - Click "=" without numbers → error message shown
   - Division by zero → specific error message

6. ✅ **Console Test**: Open browser DevTools
   - No JavaScript errors
   - No 404 errors for Bootstrap files

---

## Rollback Plan

If issues arise:
1. Delete `Pages/_ViewStart.cshtml`
2. Delete `Pages/Shared/_Layout.cshtml`
3. Calculator returns to current (broken) state
4. Investigate issue before retry

---

**Status**: Design Complete  
**Implementation Time**: 30-45 minutes  
**Risk Level**: LOW (copy/paste operation, no logic changes)
