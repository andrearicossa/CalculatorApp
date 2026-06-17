# Design: Calculator Navigation

## Architecture Overview

```
┌──────────────────────────────────────────────────────────┐
│                    Browser (Client)                       │
├──────────────────────────────────────────────────────────┤
│                                                           │
│  Any Page (Calculator, Home, Privacy)                    │
│  ┌────────────────────────────────────────────────────┐  │
│  │  Shared Layout (Pages/Shared/_Layout.cshtml)      │  │
│  │  ┌──────────────────────────────────────────────┐ │  │
│  │  │  Navbar (Bootstrap)                           │ │  │
│  │  │  - Calculator (active if on /Calculator)      │ │  │
│  │  │  - Home       (active if on /Home/Index)      │ │  │
│  │  │  - Privacy    (active if on /Privacy)         │ │  │
│  │  │  - Hamburger Toggler (visible < 992px)        │ │  │
│  │  └──────────────────────────────────────────────┘ │  │
│  │  ┌──────────────────────────────────────────────┐ │  │
│  │  │  @RenderBody() - Page Content                 │ │  │
│  │  └──────────────────────────────────────────────┘ │  │
│  └────────────────────────────────────────────────────┘  │
│                                                           │
│  User clicks menu link or types URL                      │
│                           │                               │
└───────────────────────────┼───────────────────────────────┘
                            │ HTTP GET
                            ▼
┌──────────────────────────────────────────────────────────┐
│                  Server (ASP.NET Core)                    │
├──────────────────────────────────────────────────────────┤
│                                                           │
│  Program.cs (Routing Configuration)                      │
│  ┌────────────────────────────────────────────────────┐  │
│  │  app.MapGet("/", () =>                            │  │
│  │      Results.Redirect("/Calculator"));            │  │
│  │                                                    │  │
│  │  app.MapRazorPages();     // /Calculator, /Privacy│  │
│  │  app.MapControllerRoute(...); // /Home/Index      │  │
│  └────────────────────────────────────────────────────┘  │
│                           │                               │
│                           ├─ "/" → Redirect to /Calculator
│                           ├─ "/Calculator" → Calculator.cshtml
│                           ├─ "/Home/Index" → HomeController.Index()
│                           └─ "/Privacy" → Privacy.cshtml
│                                                           │
│  Layout Helper (in _Layout.cshtml)                       │
│  ┌────────────────────────────────────────────────────┐  │
│  │  @functions {                                      │  │
│  │      string GetActiveClass(string page,            │  │
│  │                            string? action = null)  │  │
│  │      {                                             │  │
│  │          var currentPage =                         │  │
│  │              ViewContext.RouteData                 │  │
│  │                  .Values["page"]?.ToString();      │  │
│  │          var currentAction =                       │  │
│  │              ViewContext.RouteData                 │  │
│  │                  .Values["action"]?.ToString();    │  │
│  │                                                    │  │
│  │          if (page != null &&                       │  │
│  │              currentPage == page)                  │  │
│  │              return "active";                      │  │
│  │                                                    │  │
│  │          if (action != null &&                     │  │
│  │              currentAction == action)              │  │
│  │              return "active";                      │  │
│  │                                                    │  │
│  │          return "";                                │  │
│  │      }                                             │  │
│  │  }                                                 │  │
│  └────────────────────────────────────────────────────┘  │
│                                                           │
└──────────────────────────────────────────────────────────┘
```

---

## Component Breakdown

### 1. Shared Layout (_Layout.cshtml)

**File**: `Pages/Shared/_Layout.cshtml` (o `Views/Shared/_Layout.cshtml` se progetto è misto MVC)

**Responsibilities**:
- Render navbar con tre voci menu
- Applicare classe "active" alla voce corrente
- Gestire responsive behavior (hamburger menu)
- Includere Bootstrap navbar markup

**Key Elements**:
```html
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <div class="container-fluid">
    <a class="navbar-brand" href="/">Calculator App</a>
    
    <!-- Hamburger Toggler -->
    <button class="navbar-toggler" type="button" 
            data-bs-toggle="collapse" 
            data-bs-target="#navbarNav">
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

@RenderBody()

@functions {
    string GetActiveClass(string? page, string? action = null)
    {
        var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
        var currentAction = ViewContext.RouteData.Values["action"]?.ToString();
        
        if (page != null && currentPage == page)
            return "active";
        
        if (action != null && currentAction == action)
            return "active";
        
        return "";
    }
}
```

**Design Decisions**:
- **navbar-expand-lg**: Menu collapses below 992px (tablets e mobile)
- **Tag Helpers**: `asp-page` per Razor Pages, `asp-controller/asp-action` per MVC
- **Helper Function**: Gestisce sia Razor Pages che Controller-based pages
- **Bootstrap Classes**: `navbar-light bg-light` per tema chiaro (personalizzabile)

---

### 2. Program.cs Route Configuration

**File**: `Program.cs`

**Responsibilities**:
- Aggiungere redirect da "/" a "/Calculator"
- Mantenere configurazione esistente MapRazorPages e MapControllerRoute

**Code**:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Services configuration (existing)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

var app = builder.Build();

// Middleware configuration (existing)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

// **NEW: Redirect root to Calculator**
app.MapGet("/", () => Results.Redirect("/Calculator"));

// Existing route mappings
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
```

**Design Decisions**:
- **MapGet before MapControllerRoute**: Priorità al redirect specifico
- **Results.Redirect**: HTTP 302 redirect (temporaneo ma standard)
- **Order matters**: MapGet deve essere prima delle route generiche

---

### 3. Home Page (Index View)

**File**: `Views/Home/Index.cshtml`

**Responsibilities**:
- Presentare l'applicazione all'utente
- Fornire link prominente a Calculator
- Spiegare brevemente le funzionalità

**Content Suggestion**:
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

**Design Decisions**:
- **Informational**: Non funzionale, solo presentazione
- **CTA Button**: "Go to Calculator" prominente
- **Features List**: Descrive capabilities della calcolatrice

---

## Data Flow

### Scenario 1: User Accesses Root URL

```
User → Browser: Navigate to https://localhost/
Browser → Server: GET /
Server (Program.cs): MapGet("/", ...) matches
Server: Execute Results.Redirect("/Calculator")
Server → Browser: HTTP 302 Redirect to /Calculator
Browser: Follow redirect
Browser → Server: GET /Calculator
Server: Render Calculator.cshtml with Layout
Layout: GetActiveClass("/Calculator") returns "active"
Server → Browser: HTML with navbar (Calculator highlighted)
```

---

### Scenario 2: User Clicks "Home" Menu Link

```
User: On Calculator page
User: Click "Home" in navbar
Browser → Server: GET /Home/Index
Server: HomeController.Index() executes
Server: Render Index.cshtml with Layout
Layout: GetActiveClass(null, "Index") returns "active"
Server → Browser: HTML with navbar (Home highlighted)
```

---

### Scenario 3: Mobile Navigation

```
User: On mobile device (< 992px)
Browser: Renders navbar with hamburger toggler visible
User: Click hamburger icon
Browser (JavaScript): Toggle collapse (data-bs-toggle="collapse")
Navbar: Expands showing menu items vertically
User: Click "Privacy"
Browser → Server: GET /Privacy
Server: Render Privacy.cshtml with Layout
Layout: GetActiveClass("/Privacy") returns "active"
Server → Browser: HTML with navbar collapsed by default
```

---

## Bootstrap Navbar Structure

### Desktop (≥ 992px)
```
┌──────────────────────────────────────────────────┐
│ Calculator App  │ Calculator │ Home │ Privacy    │
└──────────────────────────────────────────────────┘
```

### Mobile (< 992px)
```
┌──────────────────────────────────────────────────┐
│ Calculator App                            [☰]    │
└──────────────────────────────────────────────────┘

When toggler clicked:
┌──────────────────────────────────────────────────┐
│ Calculator App                            [☰]    │
├──────────────────────────────────────────────────┤
│ Calculator                                       │
│ Home                                             │
│ Privacy                                          │
└──────────────────────────────────────────────────┘
```

---

## Testing Strategy

### Manual Testing Checklist

**Desktop Navigation**:
1. ✅ Access "/" → redirect to "/Calculator" ✓
2. ✅ Calculator page shows navbar with "Calculator" active ✓
3. ✅ Click "Home" → navigate to /Home/Index, "Home" active ✓
4. ✅ Click "Privacy" → navigate to /Privacy, "Privacy" active ✓
5. ✅ Click "Calculator" from Home → return to Calculator, active state updates ✓

**Mobile Navigation** (resize browser < 992px):
6. ✅ Hamburger toggler visible ✓
7. ✅ Menu collapsed by default ✓
8. ✅ Click toggler → menu expands ✓
9. ✅ Click menu item → navigate correctly, menu collapses ✓
10. ✅ Active state visible in expanded menu ✓

**Edge Cases**:
11. ✅ Direct access to /Calculator → navbar shows, Calculator active ✓
12. ✅ Browser back/forward → active state updates correctly ✓
13. ✅ JavaScript disabled → menu items still clickable (no collapse animation) ✓
14. ✅ 404 page (if accessed) → navbar still present ✓

---

## File Changes Summary

| File | Action | Description |
|------|--------|-------------|
| `Program.cs` | **MODIFY** | Add `app.MapGet("/", ...)` redirect |
| `Pages/Shared/_Layout.cshtml` | **MODIFY** | Add navbar with GetActiveClass helper |
| `Views/Shared/_Layout.cshtml` | **VERIFY/MODIFY** | Check if exists, modify if this is the active layout |
| `Views/Home/Index.cshtml` | **MODIFY** | Update content to informational page |

**Note**: Se il progetto usa solo Razor Pages senza Controllers, potrebbe non esistere Views/Shared/_Layout.cshtml. Priorità a Pages/Shared/_Layout.cshtml.

---

## Security & Performance

**Security**:
- **No XSS Risk**: Tag helpers auto-escape output
- **No CSRF Risk**: GET navigation, no sensitive operations
- **No SQL Injection**: No database interaction

**Performance**:
- **Redirect Overhead**: Minimal (one extra HTTP round-trip on root access)
- **Layout Render**: No impact (already rendered on every page)
- **Helper Method**: O(1) complexity, negligible overhead

---

## Accessibility

- **Keyboard Navigation**: All menu links keyboard-accessible (tab order)
- **Screen Reader**: Navbar landmarks recognized (`<nav>` element)
- **Focus Indicators**: Bootstrap default focus styles applied
- **ARIA**: Bootstrap navbar includes appropriate ARIA attributes for toggler

---

## Future Enhancements (Out of Scope)

- Icons for menu items (e.g., calculator icon, home icon)
- Dropdown submenus for future sections
- Sticky navbar (fixed on scroll)
- Dark mode toggle
- Localization (EN/IT language switcher)
- Breadcrumb below navbar
- Footer navigation mirroring header menu

---

**Status**: Design Complete  
**Ready for Implementation**: Yes  
**Next Steps**: Proceed to Tasks for step-by-step implementation
