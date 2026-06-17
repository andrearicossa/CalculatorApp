# Implementation Tasks: Calculator Navigation

## Task Breakdown

### Phase 1: Layout Preparation

#### TASK-01: Identify Active Layout File
**Priority**: High  
**Estimated Time**: 15 minutes

**Steps**:
1. Check if `Pages/Shared/_Layout.cshtml` exists
2. If not, check `Views/Shared/_Layout.cshtml`
3. Verify which layout is actually used by examining `_ViewStart.cshtml` in Pages or Views directory
4. Note the correct path for subsequent tasks

**Acceptance Criteria**:
- Active layout file identified and path documented
- Layout uses Bootstrap 5.x (verify version in layout or check NuGet packages)

**Output**: Path to layout file (e.g., `Views/Shared/_Layout.cshtml`)

---

### Phase 2: Program.cs Configuration

#### TASK-02: Add Root Redirect to Calculator
**Priority**: High  
**Estimated Time**: 10 minutes  
**Dependencies**: None

**Steps**:
1. Open `Program.cs`
2. Locate the line `app.MapRazorPages();` and `app.MapControllerRoute(...)`
3. **Before** these lines, add:
   ```csharp
   app.MapGet("/", () => Results.Redirect("/Calculator"));
   ```
4. Save file

**Acceptance Criteria**:
- Redirect added before MapRazorPages and MapControllerRoute
- Code compiles without errors
- Navigate to "/" redirects to "/Calculator"

**Test**:
- Run app, access `https://localhost:<port>/`
- Should redirect to `https://localhost:<port>/Calculator`

---

### Phase 3: Layout Navbar Implementation

#### TASK-03: Add GetActiveClass Helper Method to Layout
**Priority**: High  
**Estimated Time**: 20 minutes  
**Dependencies**: TASK-01

**Steps**:
1. Open the layout file identified in TASK-01
2. Locate the closing `</html>` tag or find existing `@functions` block if present
3. Add the following helper method (before `</html>` or inside existing `@functions`):
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

**Acceptance Criteria**:
- Helper method compiles without errors
- Method accessible from within layout markup
- Returns "active" when route matches, empty string otherwise

---

#### TASK-04: Update Navbar with Calculator, Home, Privacy Links
**Priority**: High  
**Estimated Time**: 30-45 minutes  
**Dependencies**: TASK-03

**Steps**:
1. Locate the existing `<nav>` element in the layout (likely already has Bootstrap navbar)
2. Find the `<ul class="navbar-nav">` or equivalent
3. Update/add menu items in this order:
   ```html
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
   ```
4. **Note**: If Privacy is a controller action, use `asp-controller="Home" asp-action="Privacy"` instead
5. Save file

**Acceptance Criteria**:
- Three menu items visible in navbar
- Order: Calculator, Home, Privacy
- Tag helpers render correct URLs
- Active class applied to current page link

**Test**:
- Navigate to each page (/Calculator, /Home/Index, /Privacy)
- Verify corresponding menu item has "active" class (highlighted)

---

#### TASK-05: Ensure Navbar is Responsive (Mobile Support)
**Priority**: Medium  
**Estimated Time**: 20 minutes  
**Dependencies**: TASK-04

**Steps**:
1. In the layout navbar, verify `<nav>` has class `navbar-expand-lg`
2. Verify presence of hamburger toggler button:
   ```html
   <button class="navbar-toggler" type="button" 
           data-bs-toggle="collapse" 
           data-bs-target="#navbarNav"
           aria-controls="navbarNav" 
           aria-expanded="false" 
           aria-label="Toggle navigation">
       <span class="navbar-toggler-icon"></span>
   </button>
   ```
3. Verify navbar menu is wrapped in collapse div:
   ```html
   <div class="collapse navbar-collapse" id="navbarNav">
       <ul class="navbar-nav">
           <!-- menu items here -->
       </ul>
   </div>
   ```
4. If missing, add toggler and wrap menu in collapse div

**Acceptance Criteria**:
- Navbar has `navbar-expand-lg` class
- Hamburger toggler present
- Menu wrapped in `collapse navbar-collapse`
- On screens < 992px: hamburger visible, menu collapsed
- Click hamburger: menu expands/collapses

**Test**:
- Resize browser window to < 992px (or use dev tools device emulation)
- Verify hamburger icon visible
- Click hamburger: menu should expand
- Click menu item: should navigate and menu should collapse

---

### Phase 4: Home Page Update

#### TASK-06: Update Home/Index View Content
**Priority**: Medium  
**Estimated Time**: 30 minutes  
**Dependencies**: None

**Steps**:
1. Open `Views/Home/Index.cshtml`
2. Replace or update content to present application information:
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
3. Save file

**Acceptance Criteria**:
- Page displays welcoming message
- "Go to Calculator" button prominent and functional
- Features list visible
- Link to Calculator works correctly

**Test**:
- Navigate to /Home/Index
- Verify content displays correctly
- Click "Go to Calculator" button: should navigate to /Calculator

---

### Phase 5: Testing & Verification

#### TASK-07: Manual Testing - Desktop Navigation
**Priority**: High  
**Estimated Time**: 30 minutes  
**Dependencies**: All previous tasks

**Test Scenarios**:
1. **Root Access**: Navigate to `/` → should redirect to `/Calculator`
2. **Calculator Page**: Menu shows "Calculator" highlighted
3. **Home Navigation**: Click "Home" → navigate to `/Home/Index`, "Home" highlighted
4. **Privacy Navigation**: Click "Privacy" → navigate to `/Privacy`, "Privacy" highlighted
5. **Calculator Return**: From Home, click "Calculator" → return to Calculator, highlighted
6. **Direct URL**: Type `/Privacy` in URL bar → Privacy page loads, menu shows "Privacy" active
7. **Browser Back**: Navigate Home → Calculator → back button → should return to Home with correct highlight

**Acceptance Criteria**:
- All navigation flows work correctly
- Active state always reflects current page
- No console errors in browser dev tools

---

#### TASK-08: Manual Testing - Mobile Navigation
**Priority**: High  
**Estimated Time**: 20 minutes  
**Dependencies**: TASK-05, TASK-07

**Test Scenarios** (Resize browser to < 992px or use mobile device emulation):
1. **Hamburger Visible**: Menu collapsed, hamburger icon visible
2. **Menu Expand**: Click hamburger → menu expands showing all items vertically
3. **Navigation**: Click menu item (e.g., "Home") → navigates correctly, menu collapses
4. **Active State**: Expanded menu shows correct active item highlighted
5. **Multiple Toggles**: Open → close → open menu → should work smoothly

**Acceptance Criteria**:
- Hamburger menu functional on screens < 992px
- Menu items accessible and navigable
- Active state visible in expanded menu
- No layout issues or overlaps

---

#### TASK-09: Edge Case Testing
**Priority**: Medium  
**Estimated Time**: 15 minutes  
**Dependencies**: TASK-07, TASK-08

**Test Scenarios**:
1. **JavaScript Disabled**: Disable JS in browser → menu links should still work (no collapse animation)
2. **404 Page**: Navigate to non-existent URL (e.g., `/NotFound`) → navbar should still be present
3. **Browser Compatibility**: Test on Chrome, Edge, Firefox → navbar should work consistently

**Acceptance Criteria**:
- Navigation functional without JavaScript
- Navbar present on error pages
- Cross-browser compatibility verified

---

### Phase 6: Code Review & Cleanup

#### TASK-10: Code Review and Documentation
**Priority**: Low  
**Estimated Time**: 20 minutes  
**Dependencies**: All previous tasks

**Steps**:
1. Review Program.cs changes: redirect logic clear and well-placed
2. Review layout changes: helper method clean, navbar markup semantic
3. Verify no duplicate code or unused markup
4. Add comment above redirect in Program.cs: `// Redirect root to Calculator as homepage`
5. Ensure consistent indentation and formatting

**Acceptance Criteria**:
- Code follows project conventions
- No code smells or duplications
- Comments added where helpful
- Layout markup semantically correct

---

#### TASK-11: Build Verification
**Priority**: High  
**Estimated Time**: 10 minutes  
**Dependencies**: All previous tasks

**Steps**:
1. Clean solution
2. Rebuild solution
3. Check for warnings or errors
4. Run application
5. Perform quick smoke test (access /, click each menu item once)

**Acceptance Criteria**:
- Solution builds without errors
- No critical warnings
- Application runs without crashes
- Basic navigation functional

---

## Task Summary

| Phase | Tasks | Estimated Time |
|-------|-------|----------------|
| Phase 1: Layout Prep | TASK-01 | 15 min |
| Phase 2: Program.cs | TASK-02 | 10 min |
| Phase 3: Layout Navbar | TASK-03 to TASK-05 | 1h 10min - 1h 25min |
| Phase 4: Home Page | TASK-06 | 30 min |
| Phase 5: Testing | TASK-07 to TASK-09 | 1h 5min |
| Phase 6: Review & Build | TASK-10 to TASK-11 | 30 min |
| **TOTAL** | **11 tasks** | **~3.5-4 hours** |

---

## Critical Path

```
TASK-01 (Identify Layout)
   ↓
TASK-02 (Redirect in Program.cs) — can run in parallel
   ↓
TASK-03 (Helper Method)
   ↓
TASK-04 (Navbar Links)
   ↓
TASK-05 (Responsive Navbar)
   ↓
TASK-06 (Home Page) — can run in parallel with TASK-05
   ↓
TASK-07 (Desktop Testing)
   ↓
TASK-08 (Mobile Testing)
   ↓
TASK-09 (Edge Case Testing)
   ↓
TASK-10 (Code Review)
   ↓
TASK-11 (Build Verification)
```

**Parallel Work Opportunities**:
- TASK-02 (Program.cs) can be done independently while working on layout
- TASK-06 (Home page) can be done alongside TASK-05 (responsive navbar)

---

## Risk Mitigation

| Risk | Mitigation |
|------|------------|
| Layout file in unexpected location | TASK-01 explicitly identifies active layout first |
| Bootstrap version incompatibility | Verify version in TASK-01, adjust syntax if v4 (data-toggle instead of data-bs-toggle) |
| Helper method not accessible | Place in @functions block, ensure correct scope |
| Navbar structure differs from expected | Adapt to existing structure, preserve existing functionality |

---

## Definition of Done

- [ ] All 11 tasks completed
- [ ] All acceptance criteria met
- [ ] Desktop navigation fully functional
- [ ] Mobile navigation fully functional with hamburger menu
- [ ] Active state correctly highlights current page
- [ ] Root URL redirects to Calculator
- [ ] Home page updated with informational content
- [ ] No build errors or warnings
- [ ] Manual testing passed (desktop + mobile + edge cases)
- [ ] Code reviewed for quality

---

**Status**: Ready for Implementation  
**Next Step**: Start with TASK-01 (Identify Active Layout File)
