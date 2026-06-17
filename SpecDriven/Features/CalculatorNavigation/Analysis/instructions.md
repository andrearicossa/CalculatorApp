# HUMAN DECISION REQUIRED

## 📋 Refinement Summary

Il refinement ha identificato **8 issue** che richiedono attenzione:

- **ISS-01, ISS-04**: Ambiguità sulla nomenclatura menu (Home vs Calculator)
- **ISS-02**: Missing info su approccio tecnico per impostare homepage
- **ISS-03**: Missing info sul comportamento mobile del menu
- **ISS-05**: Ambiguità sul path del layout condiviso
- **ISS-06**: Ambiguità sull'approccio per evidenziare voce attiva
- **ISS-07**: Missing info sul destino della pagina Index esistente
- **ISS-08**: Missing info su comportamento click voce già attiva

---

## ✅ DECISION FORMAT

Indica quali issue devono essere applicate e quali saltate:

### APPLY:
- ISS-01: Mantenere "Calculator", "Home", "Privacy" nel menu (Calculator è homepage, Home punta a Index informativa)
- ISS-02: Usare redirect in Program.cs per impostare Calculator come homepage
- ISS-03: Navbar Bootstrap standard con toggler responsive (breakpoint lg)
- ISS-04: Risolto da ISS-01 (ordine: Calculator, Home, Privacy)
- ISS-05: Usare Pages/Shared/_Layout.cshtml (standard Razor Pages)
- ISS-06: Usare helper method con ViewContext per evidenziare voce attiva
- ISS-07: Mantenere HomeController.Index come pagina informativa (About)

### SKIP:
- ISS-08: Comportamento standard (nessuna azione su click voce già attiva, nice-to-have)

---

## 📊 Issue Overview

| ID | Type | Description | Impact |
|---|---|---|---|
| ISS-01 | ambiguity | Voce "Home" vs "Calculator" nel menu | US-01, US-02 |
| ISS-02 | missing_info | Approccio tecnico per homepage | US-03 |
| ISS-03 | missing_info | Comportamento menu mobile | US-02 |
| ISS-04 | inconsistency | Ordine menu con "Home" e "Calculator" | US-01, US-03 |
| ISS-05 | missing_info | Path layout condiviso (Views vs Pages) | US-01, US-02 |
| ISS-06 | ambiguity | Approccio evidenziazione voce attiva | US-04 |
| ISS-07 | missing_info | Destino pagina Index esistente | US-02, US-03 |
| ISS-08 | missing_info | Comportamento click voce già attiva | US-02 |

---

## 💡 Recommendation

Per un'**implementazione pulita e funzionante**, suggerisco:

**APPLY:**
- ISS-01 (decisione: mantenere Calculator, Home, Privacy)
- ISS-02 (decisione: usare redirect in Program.cs per chiarezza)
- ISS-03 (decisione: navbar Bootstrap standard con toggler responsive)
- ISS-04 (risolto applicando ISS-01)
- ISS-05 (decisione: usare Pages/Shared/_Layout.cshtml per Razor Pages)
- ISS-06 (decisione: usare ViewData con helper per robustezza)
- ISS-07 (decisione: mantenere Index come pagina informativa separata da Calculator)

**SKIP (o gestire separatamente):**
- ISS-08 (comportamento standard: nessuna azione, nice-to-have)

---

## 🎯 Proposed Solution (for APPLY decisions)

### ISS-01 & ISS-04: Menu Nomenclatura
**Decisione**: Menu avrà tre voci:
1. **Calculator** (homepage, evidenziata quando attiva)
2. **Home** (link a pagina Index informativa)
3. **Privacy**

**Razionale**: Mantiene Calculator come funzionalità principale, Home/Index diventa pagina di presentazione/info.

---

### ISS-02: Homepage Approach
**Decisione**: Redirect in Program.cs
```csharp
app.MapGet("/", () => Results.Redirect("/Calculator"));
```
**Razionale**: Chiarezza, flessibilità, non modifica struttura file esistente.

---

### ISS-03: Mobile Menu
**Decisione**: Navbar Bootstrap con collapse
```html
<button class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarNav">
```
**Breakpoint**: `navbar-expand-lg` (collapse su schermi < 992px)

---

### ISS-05: Layout Path
**Decisione**: `Pages/Shared/_Layout.cshtml`  
**Razionale**: Progetto usa Razor Pages, path standard per Razor Pages.

**NOTA**: Se non esiste, verificare Views/Shared/_Layout.cshtml e usare quello.

---

### ISS-06: Active State
**Decisione**: Helper method in layout
```csharp
@{
    string GetActiveClass(string page) 
    {
        var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
        return currentPage == page ? "active" : "";
    }
}
```

---

### ISS-07: Pagina Index
**Decisione**: Mantenere `HomeController.Index`, accessibile tramite menu "Home"  
**Contenuto**: Breve presentazione dell'applicazione, poi link a Calculator

---

⚠️ **DECISIONI APPLICATE - PROSEGUIRE CON STEP 4**
