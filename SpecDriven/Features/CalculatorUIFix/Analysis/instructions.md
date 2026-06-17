# HUMAN DECISION REQUIRED

## 📋 Refinement Summary

Il refinement ha identificato **6 issue** che richiedono attenzione:

- **ISS-01**: Missing info su Bootstrap (locale vs CDN) → **RISOLTO**: Bootstrap è in wwwroot/lib/bootstrap (verificato)
- **ISS-02**: Missing info su Pages/_ViewStart.cshtml → **RISOLTO**: NON esiste, va creato
- **ISS-03**: Ambiguity su quale opzione scegliere per risolvere il problema → **DECISIONE NECESSARIA**
- **ISS-04**: Missing info su stato layout MVC → **RISOLTO**: Views/Shared/_Layout.cshtml è aggiornato con navbar FEAT-002
- **ISS-05**: Missing info su supporto @section Styles → **DECISIONE NECESSARIA**
- **ISS-06**: Inconsistency su tipo di errore JavaScript → **LOW PRIORITY**, può essere clarificato in implementazione

---

## ✅ DECISION FORMAT

Indica quali issue devono essere applicate e quali saltate:

### APPLY:
- ISS-01: Usare Bootstrap LOCALE (wwwroot/lib/bootstrap) nel layout
- ISS-02: Creare Pages/_ViewStart.cshtml con `Layout = "_Layout"`
- ISS-03: **OPZIONE 1** - Creare Pages/Shared/_Layout.cshtml copiando e adattando da Views/Shared/_Layout.cshtml
- ISS-04: Copiare navbar aggiornato da Views/Shared/_Layout.cshtml nel nuovo layout Razor Pages
- ISS-05: Aggiungere supporto per @section Styles nel nuovo layout (oltre a @section Scripts)

### SKIP:
- ISS-06: Chiarimento tecnico non bloccante, gestito in implementation notes

---

## 📊 Issue Overview

| ID | Type | Description | Impact | Verifica |
|---|---|---|---|---|
| ISS-01 | missing_info | Bootstrap locale vs CDN | US-01 | ✅ LOCALE (wwwroot/lib/bootstrap) |
| ISS-02 | missing_info | Esistenza Pages/_ViewStart.cshtml | US-01 | ✅ NON ESISTE, creare |
| ISS-03 | ambiguity | Quale opzione scegliere (3 proposte) | US-01, US-02, US-03 | **DECISIONE: Opzione 1** |
| ISS-04 | missing_info | Stato layout MVC aggiornato | US-01 | ✅ AGGIORNATO con navbar |
| ISS-05 | missing_info | Supporto @section Styles | US-01 | **DECISIONE: Includere** |
| ISS-06 | inconsistency | Tipo errore JavaScript | US-03 | ⚠️ SKIP (low priority) |

---

## 💡 Proposed Solution (APPLY decisions)

### ISS-01: Bootstrap Source
**Decisione**: Usare riferimenti LOCALI a Bootstrap
```html
<link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
```
**Razionale**: Bootstrap già presente in wwwroot/lib (verificato), no dipendenza CDN esterno

---

### ISS-02: Pages/_ViewStart.cshtml
**Decisione**: Creare file `Pages/_ViewStart.cshtml` con contenuto:
```csharp
@{
    Layout = "_Layout";
}
```
**Razionale**: Applica automaticamente il layout a tutte le Razor Pages

---

### ISS-03: Approccio Risolutivo
**Decisione**: **OPZIONE 1** - Creare `Pages/Shared/_Layout.cshtml`

**Razionale**:
- ✅ Separazione pulita tra Razor Pages e MVC Views
- ✅ Manutenibilità (modifiche a layout Razor Pages non impattano MVC e viceversa)
- ✅ Standard ASP.NET Core (convenzione path separati)
- ✅ Flessibilità futura (layout Razor Pages può divergere da MVC se necessario)

**Contro Opzione 2** (cross-reference): Coupling tra Pages e Views, meno manutenibile
**Contro Opzione 3** (convertire a MVC View): Refactoring eccessivo, contro principio Razor Pages

---

### ISS-04: Navbar nel Nuovo Layout
**Decisione**: Copiare navbar completo da `Views/Shared/_Layout.cshtml` (incluso helper GetActiveClass)

**Contenuto da copiare**:
- Navbar HTML (linee 14-33 circa)
- @functions block con GetActiveClass (alla fine del file)

**Razionale**: Mantenere coerenza UI tra tutte le pagine (MVC e Razor Pages)

---

### ISS-05: Supporto @section Styles
**Decisione**: Includere nel layout Pages/Shared/_Layout.cshtml:
```csharp
@await RenderSectionAsync("Styles", required: false)
```
Posizionato in <head> dopo i link CSS di Bootstrap

**Razionale**: Permette a pagine Razor Pages di aggiungere CSS custom (es. Calculator.cshtml ha custom styles)

---

### ISS-06: Tipo Errore JavaScript
**Decisione**: SKIP (documentare in technical notes durante implementazione)

**Chiarimento**: Errore console "ReferenceError: appendToDisplay is not defined" quando JavaScript non caricato

---

## 🎯 Implementation Plan

### Step 1: Creare Directory
1. Creare `Calcuator/Pages/Shared/`

### Step 2: Creare Layout Razor Pages
1. Copiare `Views/Shared/_Layout.cshtml` → `Pages/Shared/_Layout.cshtml`
2. Verificare riferimenti Bootstrap (già locali)
3. Assicurarsi che includa:
   - `@await RenderSectionAsync("Styles", required: false)` in <head>
   - `@await RenderSectionAsync("Scripts", required: false)` prima di </body>
   - Navbar con Calculator/Home/Privacy
   - @functions block con GetActiveClass

### Step 3: Creare _ViewStart
1. Creare `Calcuator/Pages/_ViewStart.cshtml`
2. Contenuto: `@{ Layout = "_Layout"; }`

### Step 4: Verificare Calculator.cshtml
1. Verificare che Calculator.cshtml abbia `@section Scripts` con JavaScript tastierino
2. Verificare che gli stili custom siano in `@section Styles` (se presenti inline, spostarli)

### Step 5: Test
1. Avviare applicazione
2. Navigare a /Calculator
3. Verificare:
   - Stili Bootstrap applicati ✓
   - Tastierino funzionante (click su numeri aggiorna display) ✓
   - Calcolo funzionante (= esegue senza errori) ✓

---

## 🚨 Critical Dependencies

- ✅ Bootstrap presente in wwwroot/lib/bootstrap (verificato)
- ✅ Views/Shared/_Layout.cshtml aggiornato con navbar (verificato)
- ❌ Pages/Shared/ directory NON esiste (va creata)
- ❌ Pages/_ViewStart.cshtml NON esiste (va creato)

---

⚠️ **DECISIONI APPLICATE - READY TO PROCEED**

Tutte le verifiche tecniche sono completate. Le decisioni proposte sono basate sullo stato attuale del progetto verificato tramite file system.

---

**APPLY SUMMARY**:
- ISS-01: Bootstrap locale ✓
- ISS-02: Creare Pages/_ViewStart.cshtml ✓
- ISS-03: Opzione 1 (Pages/Shared/_Layout.cshtml) ✓
- ISS-04: Copiare navbar da Views layout ✓
- ISS-05: Supporto @section Styles ✓

**SKIP**:
- ISS-06: Non bloccante

**READY FOR STEP 4 (APPLY REFINEMENT)**
