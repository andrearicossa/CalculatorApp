# Technical Specification: Calculator Navigation

## Overview

**Feature ID**: FEAT-002  
**Feature Name**: CalculatorNavigation  
**Technology Stack**: ASP.NET Core Razor Pages + MVC (.NET 10), Bootstrap 5  
**Architecture**: Shared Layout with Navigation, Route Redirect

---

## System Architecture

### High-Level Architecture

```
┌───────────────────────────────────────────────────────────┐
│                      Presentation Layer                    │
│  ┌─────────────────────────────────────────────────────┐  │
│  │  Shared Layout (_Layout.cshtml)                     │  │
│  │  - Navbar (Bootstrap)                               │  │
│  │  - GetActiveClass Helper                            │  │
│  │  - @RenderBody() for page content                   │  │
│  └─────────────────────────────────────────────────────┘  │
│                          ▲                                 │
│                          │ Used by all pages              │
│  ┌──────────┬────────────┬────────────┐                  │
│  │Calculator│    Home    │  Privacy   │                  │
│  │   Page   │ (Index View)│    Page    │                  │
│  └──────────┴────────────┴────────────┘                  │
└───────────────────────────────────────────────────────────┘
                          │
                          │ HTTP GET
                          ▼
┌───────────────────────────────────────────────────────────┐
│                  Routing Layer (Program.cs)                │
│  ┌─────────────────────────────────────────────────────┐  │
│  │  app.MapGet("/", () => Results.Redirect("/Calculator"))│
│  │  app.MapRazorPages();                                │  │
│  │  app.MapControllerRoute(...);                        │  │
│  └─────────────────────────────────────────────────────┘  │
└───────────────────────────────────────────────────────────┘
```

---

## Component Specifications

### 1. Program.cs Route Configuration

**File**: `Program.cs`

**Addition**:
```csharp
// Redirect root to Calculator as homepage
app.MapGet("/", () => Results.Redirect("/Calculator"));
```

**Placement**: After `app.MapStaticAssets()`, before `app.MapRazorPages()` and `app.MapControllerRoute()`

**Behavior**:
- User accesses "/" → Server returns HTTP 302 redirect to "/Calculator"
- Browser follows redirect → Calculator page loads

**Exception Handling**: None needed (simple redirect, no business logic)

---

### 2. Shared Layout with Navbar

**File**: `Pages/Shared/_Layout.cshtml` (or `Views/Shared/_Layout.cshtml`)

**Structure**:
```html
<!DOCTYPE html>
<html lang="en">
<head>
    <!-- head content existing -->
</head>
<body>
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container-fluid">
            <a class="navbar-brand" href="/">Calculator App</a>
            
            <!-- Hamburger Toggler -->
            <button class="navbar-toggler" type="button" 
                    data-bs-toggle="collapse" 
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav" 
                    aria-expanded="false" 
                    aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            
            <!-- Navbar Links -->
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a asp-page="/Calculator" 
                           class="nav-link @GetActiveClass("/Calculator")">
                            Calculator
                        </a>
                    </li>
                    <li class="nav-item">
                        <a asp-controller="Home" 
                           asp-action="Index" 
                           class="nav-link @GetActiveClass(null, "Index")">
                            Home
                        </a>
                    </li>
                    <li class="nav-item">
                        <a asp-page="/Privacy" 
                           class="nav-link @GetActiveClass("/Privacy")">
                            Privacy
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    
    <!-- Page Content -->
    <main role="main" class="pb-3">
        @RenderBody()
    </main>
    
    <!-- footer existing -->
</body>
</html>

@functions {
    string GetActiveClass(string? page, string? action = null)
    {
        var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
        var currentAction = ViewContext.RouteData.Values["action"]?.ToString();
        
        if (!string.IsNullOrEmpty(page) && currentPage == page)
            return "active";
        
        if (!string.IsNullOrEmpty(action) && currentAction == action)
            return "active";
        
        return "";
    }
}
```

**Key Components**:
- **Navbar**: Bootstrap 5 navbar with `navbar-expand-lg` (responsive breakpoint)
- **Hamburger Toggler**: `navbar-toggler` button for mobile
- **Collapse Div**: `collapse navbar-collapse` wraps menu items
- **Tag Helpers**: `asp-page`, `asp-controller`, `asp-action` for URL generation
- **GetActiveClass**: Helper method to apply "active" class to current page link

---

### 3. GetActiveClass Helper Method

**Signature**:
```csharp
string GetActiveClass(string? page, string? action = null)
```

**Parameters**:
- `page`: Razor Page route (e.g., "/Calculator", "/Privacy")
- `action`: Controller action name (e.g., "Index")

**Returns**:
- `"active"`: If current route matches parameter
- `""`: Otherwise

**Logic**:
```csharp
var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
var currentAction = ViewContext.RouteData.Values["action"]?.ToString();

if (!string.IsNullOrEmpty(page) && currentPage == page)
    return "active";

if (!string.IsNullOrEmpty(action) && currentAction == action)
    return "active";

return "";
```

**Usage Examples**:
```html
<!-- Razor Page -->
<a asp-page="/Calculator" class="nav-link @GetActiveClass("/Calculator")">

<!-- Controller Action -->
<a asp-controller="Home" asp-action="Index" class="nav-link @GetActiveClass(null, "Index")">
```

**Thread Safety**: Safe (reads from ViewContext per request, no shared state)

---

### 4. Home Page (Index View)

**File**: `Views/Home/Index.cshtml`

**Content**:
```html
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome to Calculator App</h1>
    <p class="lead">A simple web-based calculator for basic mathematical operations.</p>
    
    <div class="mt-4">
        <a asp-page="/Calculator" class="btn btn-primary btn-lg">
            Go to Calculator
        </a>
    </div>
    
    <div class="mt-5">
        <h3>Features</h3>
        <ul class="list-unstyled">
            <li>✓ Addition, Subtraction, Multiplication, Division</li>
            <li>✓ Numeric keypad interface</li>
            <li>✓ Error handling (division by zero, input validation)</li>
            <li>✓ Responsive design for mobile and desktop</li>
        </ul>
    </div>
</div>
```

**Purpose**: Informational landing page, separate from Calculator functionality

---

## Data Flow Specifications

### Flow 1: Root Access → Calculator

```
1. User: Navigate to https://localhost/
2. Browser: HTTP GET /
3. Server (Program.cs): MapGet("/", ...) matches
4. Server: Results.Redirect("/Calculator")
5. Server → Browser: HTTP 302 with Location: /Calculator
6. Browser: Follow redirect
7. Browser: HTTP GET /Calculator
8. Server: Render Calculator.cshtml with _Layout.cshtml
9. Layout: GetActiveClass("/Calculator") called → returns "active"
10. Server → Browser: HTML with navbar (Calculator highlighted)
```

---

### Flow 2: Menu Navigation (Calculator → Home)

```
1. User: On Calculator page, click "Home" in navbar
2. Browser: HTTP GET /Home/Index
3. Server: HomeController.Index() executes
4. Server: Render Index.cshtml with _Layout.cshtml
5. Layout: GetActiveClass(null, "Index") called → returns "active"
6. Server → Browser: HTML with navbar (Home highlighted)
```

---

### Flow 3: Mobile Menu Interaction

```
1. User: Access on mobile device (screen width < 992px)
2. Browser: Render layout with navbar-expand-lg
3. CSS: Navbar collapses, hamburger toggler visible
4. User: Click hamburger toggler
5. JavaScript (Bootstrap): Toggle collapse (data-bs-toggle="collapse")
6. Menu: Expands showing all links vertically
7. User: Click "Privacy" link
8. Browser: HTTP GET /Privacy
9. Menu (Bootstrap): Collapses automatically after navigation
10. Server: Render Privacy page with navbar (Privacy highlighted)
```

---

## Bootstrap Specifications

### Navbar Classes

| Class | Purpose |
|-------|---------|
| `navbar` | Base navbar component |
| `navbar-expand-lg` | Expand above 992px, collapse below |
| `navbar-light` | Light theme (text dark, background light) |
| `bg-light` | Light background color |
| `navbar-toggler` | Hamburger button for mobile |
| `navbar-collapse` | Collapsible container for menu |
| `collapse` | Bootstrap collapse behavior |
| `navbar-nav` | Navigation list |
| `nav-item` | Individual menu item |
| `nav-link` | Link inside nav-item |
| `active` | Highlight current page link |

### Responsive Breakpoints

| Breakpoint | Screen Width | Navbar Behavior |
|------------|-------------|-----------------|
| `< 992px` | Mobile/Tablet | Menu collapsed, hamburger visible |
| `≥ 992px` | Desktop | Menu expanded, hamburger hidden |

**Class**: `navbar-expand-lg` (expand at **lg** breakpoint = 992px)

---

## Validation Rules

### GetActiveClass Method
- **Input Validation**: Null-safe (uses `?.ToString()` and `!string.IsNullOrEmpty()`)
- **Output**: Always returns string (never null)
- **Case Sensitivity**: Case-sensitive comparison (matches ASP.NET Core routing)

### Redirect Configuration
- **Route Priority**: Must be before generic routes (`MapGet` before `MapControllerRoute`)
- **HTTP Method**: GET only (stateless redirect)
- **Redirect Type**: HTTP 302 (temporary redirect, standard for SPA-like behavior)

---

## Error Handling Matrix

| Error Scenario | Handling |
|----------------|----------|
| Layout file not found | ASP.NET Core exception, 500 error |
| Tag helper invalid route | ASP.NET Core warning, link renders as "#" |
| Bootstrap JS not loaded | Hamburger toggler non-functional, links still work |
| JavaScript disabled | Collapse animation broken, links functional |
| GetActiveClass exception | Method returns "" (empty), no active class applied |

---

## Testing Specifications

### Unit Tests (Not Implemented for MVP)

**Potential Test Cases**:
```csharp
[Fact]
public void GetActiveClass_RazorPage_ReturnsActive()
{
    // Mock ViewContext with page route
    // Call GetActiveClass("/Calculator")
    // Assert returns "active"
}

[Fact]
public void GetActiveClass_ControllerAction_ReturnsActive()
{
    // Mock ViewContext with controller/action route
    // Call GetActiveClass(null, "Index")
    // Assert returns "active"
}

[Fact]
public void GetActiveClass_NoMatch_ReturnsEmpty()
{
    // Mock ViewContext with different route
    // Call GetActiveClass("/Calculator")
    // Assert returns ""
}
```

### Integration Tests (Not Implemented for MVP)

**Potential Test Cases**:
```csharp
[Fact]
public async Task RootUrl_RedirectsToCalculator()
{
    var client = _factory.CreateClient();
    var response = await client.GetAsync("/");
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/Calculator", response.Headers.Location.ToString());
}

[Fact]
public async Task CalculatorPage_NavbarShowsActiveClass()
{
    var client = _factory.CreateClient();
    var response = await client.GetAsync("/Calculator");
    var html = await response.Content.ReadAsStringAsync();
    Assert.Contains("Calculator</a>", html);
    Assert.Contains("active", html); // Simplified check
}
```

---

## Performance Specifications

- **Redirect Latency**: < 5ms (single MapGet evaluation)
- **GetActiveClass Execution**: < 1ms (dictionary lookup + string comparison)
- **Page Load Impact**: Negligible (helper method O(1), navbar HTML minimal)
- **Memory Footprint**: No additional memory (no caching, stateless)

---

## Security Specifications

- **XSS Protection**: Tag helpers auto-escape output (no injection risk)
- **CSRF Protection**: Not applicable (GET navigation, no forms)
- **Authorization**: Not required (public navigation)
- **Redirect Open Redirect**: No risk (hardcoded redirect to internal route)

---

## Browser Compatibility

**Supported Browsers**:
- Chrome 90+
- Edge 90+
- Firefox 88+
- Safari 14+

**Bootstrap 5 Compatibility**: Requires modern browser with ES6 support

**Degradation Strategy**:
- JavaScript disabled: Menu links work, collapse animation absent
- Old browsers: Navbar may render incorrectly, core navigation functional

---

## Deployment Requirements

**Runtime**:
- .NET 10 SDK
- ASP.NET Core Runtime 10.x

**Configuration**:
- No app settings changes
- No database changes
- No environment-specific config

**Dependencies**:
- Bootstrap 5.x (already present via CDN or NuGet)

---

## Future Enhancements (Out of Scope)

- Menu item icons (Font Awesome or Bootstrap Icons)
- Sticky navbar (fixed on scroll)
- Dropdown submenus for future sections
- Localization (menu labels in multiple languages)
- Dark mode toggle
- Breadcrumb navigation
- Active page highlight animation
- Keyboard shortcuts for menu (Alt+H for Home, etc.)

---

## Configuration Reference

### Program.cs Snippet

```csharp
// ... existing configuration ...

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

// **NEW: Redirect root to Calculator**
app.MapGet("/", () => Results.Redirect("/Calculator"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
```

---

### Layout Helper Method Snippet

```csharp
@functions {
    string GetActiveClass(string? page, string? action = null)
    {
        var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
        var currentAction = ViewContext.RouteData.Values["action"]?.ToString();
        
        if (!string.IsNullOrEmpty(page) && currentPage == page)
            return "active";
        
        if (!string.IsNullOrEmpty(action) && currentAction == action)
            return "active";
        
        return "";
    }
}
```

---

### Navbar Markup Snippet

```html
<nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
        <a class="navbar-brand" href="/">Calculator App</a>
        <button class="navbar-toggler" type="button" 
                data-bs-toggle="collapse" data-bs-target="#navbarNav">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a asp-page="/Calculator" 
                       class="nav-link @GetActiveClass("/Calculator")">Calculator</a>
                </li>
                <li class="nav-item">
                    <a asp-controller="Home" asp-action="Index" 
                       class="nav-link @GetActiveClass(null, "Index")">Home</a>
                </li>
                <li class="nav-item">
                    <a asp-page="/Privacy" 
                       class="nav-link @GetActiveClass("/Privacy")">Privacy</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
```

---

**Status**: Specification Complete  
**Version**: 1.0  
**Date**: 2026-06-04  
**Author**: AI Spec-Driven Pipeline
