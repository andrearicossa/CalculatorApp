# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-002  
**Name:** CalculatorNavigation

---

### 🧭 Business Context

**Description:**  
Miglioramento dell'esperienza utente dell'applicazione web SimpleWebCalculator attraverso l'integrazione della pagina Calculator nel menu di navigazione principale e la sua impostazione come pagina di default (homepage).

**Goal:**  
- Rendere la pagina Calculator facilmente accessibile tramite il menu di navigazione principale (header)
- Impostare Calculator come landing page predefinita dell'applicazione tramite redirect automatico
- Uniformare l'esperienza di navigazione con le altre pagine esistenti (Home/Index informativa, Privacy)
- Garantire un menu responsive funzionante su desktop e mobile

---

### 👥 Actors

- **ACT-01:** Utente web — utente che accede all'applicazione e naviga tra le pagine tramite menu o URL diretti

---

### 🔎 Functional View

L'utente accede all'applicazione web digitando l'URL root e viene automaticamente reindirizzato alla pagina Calculator (homepage). Nel menu di navigazione presente nell'header della pagina (implementato in layout condiviso), l'utente visualizza tre voci: "Calculator", "Home", e "Privacy". La voce "Calculator" è evidenziata visivamente poiché corrisponde alla pagina corrente. L'utente può cliccare su queste voci per navigare: "Calculator" porta alla calcolatrice, "Home" porta a una pagina informativa di presentazione (Index), "Privacy" alla pagina privacy. Il menu è presente in tutte le pagine, è responsive (si adatta a mobile con hamburger menu), e mantiene sempre evidenziata la voce corrispondente alla pagina corrente per chiarezza di navigazione.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Menu Navigation**
  - **Description:** Rappresenta il sistema di navigazione dell'applicazione web
  - **Key Attributes:**
    - Voci di menu (lista di link): Calculator, Home, Privacy
    - Pagina attiva corrente (per highlight)
    - Layout del menu (header/top navigation, navbar Bootstrap)
    - Comportamento responsive (hamburger menu su mobile)
  - **Related User Stories:** US-01, US-02, US-04

- **ENT-02 — Route Configuration**
  - **Description:** Configurazione delle route dell'applicazione
  - **Key Attributes:**
    - Default route (homepage): "/" → redirect a "/Calculator"
    - Named routes: /Calculator, /Home (o azione Index), /Privacy
    - Redirect rule in Program.cs
  - **Related User Stories:** US-03

---

### 🔗 Relationships

- **REL-01 — Menu Navigation → Route Configuration**
  - **Description:** Ogni voce di menu punta a una route configurata nell'applicazione (Calculator → /Calculator, Home → /Home o Index, Privacy → /Privacy)

---

### 📜 Constraints

- **CON-01:** Il menu di navigazione deve essere presente in tutte le pagine dell'applicazione (implementato in layout condiviso)
- **CON-02:** La pagina Calculator deve essere accessibile tramite route root ("/") tramite redirect automatico (ISS-02)
- **CON-03:** Le voci di menu devono includere nell'ordine: Calculator, Home, Privacy (ISS-01, ISS-04)
- **CON-04:** La voce di menu corrispondente alla pagina corrente deve essere visivamente evidenziata con classe CSS "active" (ISS-06)
- **CON-05:** Il menu deve essere responsive: navbar-expand-lg con collapse e hamburger toggler su schermi < 992px (ISS-03)
- **CON-06:** Il layout condiviso deve essere in Pages/Shared/_Layout.cshtml (standard Razor Pages) (ISS-05)
- **CON-07:** La voce "Home" punta alla pagina Index (HomeController) che presenta informazioni sull'applicazione (ISS-07)

---

## 🧩 USER STORIES

### US-01 — Aggiunta Calculator al menu di navigazione

**Descrizione:**  
Come utente web voglio vedere la voce "Calculator" come prima voce nel menu di navigazione principale per poter accedere facilmente alla calcolatrice da qualsiasi pagina dell'applicazione.

**Related Flow:** MF-02, MF-03

**Updated by:** ISS-01, ISS-04 (ordine definito: Calculator prima)

---

### US-02 — Navigazione tra pagine tramite menu

**Descrizione:**  
Come utente web voglio poter cliccare sulle voci del menu (Calculator, Home, Privacy) per navigare tra le diverse sezioni dell'applicazione in modo intuitivo, sia su desktop che su mobile tramite hamburger menu.

**Related Flow:** MF-04, MF-05, MF-06, AF-02

**Updated by:** ISS-03 (comportamento mobile specificato)

---

### US-03 — Calculator come pagina di default

**Descrizione:**  
Come utente web voglio essere reindirizzato automaticamente alla pagina Calculator quando accedo all'applicazione digitando l'URL root ("/") per avere accesso immediato alla funzionalità principale senza dover navigare manualmente.

**Related Flow:** MF-01, MF-02

**Updated by:** ISS-02 (approccio redirect in Program.cs specificato)

---

### US-04 — Evidenziazione pagina attiva nel menu

**Descrizione:**  
Come utente web voglio vedere visivamente evidenziata (con classe CSS "active") la voce di menu corrispondente alla pagina in cui mi trovo per capire chiaramente la mia posizione nell'applicazione durante la navigazione.

**Related Flow:** MF-03, MF-06, AF-01

**Updated by:** ISS-06 (approccio helper method specificato)

---

### US-05 — Accesso pagina informativa Home

**Descrizione:**  
Come utente web voglio poter cliccare sulla voce "Home" nel menu per accedere a una pagina informativa di presentazione dell'applicazione (Index) che mi fornisce contesto e descrizione della web app.

**Related Flow:** MF-04, MF-05

**Added by:** ISS-07 (chiarimento destino pagina Index)

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede all'applicazione digitando l'URL root (es. https://localhost/)
- **MF-02:** Il sistema esegue un redirect HTTP a /Calculator (configurato in Program.cs)
- **MF-03:** Il sistema visualizza la pagina Calculator con il menu di navigazione nell'header (da layout condiviso Pages/Shared/_Layout.cshtml)
- **MF-04:** L'utente visualizza nel menu tre voci nell'ordine: "Calculator" (evidenziata con classe "active"), "Home", "Privacy"
- **MF-05:** L'utente clicca su una voce di menu (es. "Privacy" o "Home")
- **MF-06:** Il sistema naviga alla pagina selezionata
- **MF-07:** Il menu aggiorna lo stato attivo evidenziando con classe "active" la nuova pagina corrente (tramite helper method che controlla ViewContext)

**Updated by:** ISS-02 (MF-02 aggiunto per redirect), ISS-01/ISS-04 (MF-04 ordine specificato), ISS-06 (MF-07 meccanismo active specificato)

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Accesso diretto a una pagina specifica

- **AF-01.1:** L'utente accede direttamente a una pagina specifica digitando l'URL completo (es. /Privacy, /Calculator, /Home)
- **AF-01.2:** Il sistema mostra la pagina richiesta con il menu di navigazione (da layout condiviso)
- **AF-01.3:** Il menu evidenzia con classe "active" la voce corrispondente alla pagina corrente (tramite helper method)

**Updated by:** ISS-06 (AF-01.3 meccanismo specificato)

---

### AF-02 — Navigazione su dispositivo mobile

- **AF-02.1:** L'utente accede all'applicazione da dispositivo mobile (schermo < 992px)
- **AF-02.2:** Il menu di navigazione si adatta al layout mobile: navbar collassa e mostra hamburger toggler (navbar-toggler di Bootstrap)
- **AF-02.3:** L'utente clicca sul pulsante hamburger per aprire il menu
- **AF-02.4:** Il menu si espande mostrando le tre voci (Calculator, Home, Privacy) in verticale
- **AF-02.5:** L'utente clicca su una voce per navigare
- **AF-02.6:** Il menu si chiude automaticamente e la navigazione avviene alla pagina selezionata

**Updated by:** ISS-03 (AF-02.2, AF-02.3, AF-02.4 dettagli responsive specificati)

---

### AF-03 — Navigazione dalla pagina Home a Calculator

- **AF-03.1:** L'utente è sulla pagina Home (Index informativa)
- **AF-03.2:** La pagina Home presenta un link o pulsante "Vai alla Calcolatrice" oltre alla voce nel menu
- **AF-03.3:** L'utente clicca sul link o sulla voce "Calculator" nel menu
- **AF-03.4:** Il sistema naviga a /Calculator
- **AF-03.5:** Il menu evidenzia "Calculator" come voce attiva

**Added by:** ISS-07 (flusso per collegamento Home → Calculator)

---

## 📜 BUSINESS RULES

- **BR-01:** La route root ("/") deve eseguire un redirect HTTP (302 o 307) alla route /Calculator tramite configurazione in Program.cs: `app.MapGet("/", () => Results.Redirect("/Calculator"))` (ISS-02)

- **BR-02:** Il menu di navigazione deve essere implementato nel layout condiviso Pages/Shared/_Layout.cshtml (se non esiste, usare Views/Shared/_Layout.cshtml) per essere presente in tutte le pagine (ISS-05)

- **BR-03:** Le voci di menu devono utilizzare tag helper ASP.NET Core per generare URL corretti:
  - Calculator: `<a asp-page="/Calculator">` (Razor Page)
  - Home: `<a asp-controller="Home" asp-action="Index">` (Controller-based)
  - Privacy: verificare se è Razor Page o Controller-based e usare tag appropriato

- **BR-04:** La voce di menu attiva deve avere una classe CSS "active" per l'evidenziazione visiva, applicata tramite helper method che controlla `ViewContext.RouteData.Values["page"]` o `ViewContext.RouteData.Values["action"]` (ISS-06)

- **BR-05:** L'ordine delle voci nel menu deve essere: Calculator (prima), Home (seconda), Privacy (terza) (ISS-01, ISS-04)

- **BR-06:** Il menu deve usare componente navbar Bootstrap con classe `navbar-expand-lg` per rendere il menu responsive con collapse su schermi < 992px (ISS-03)

- **BR-07:** Il navbar deve includere un pulsante hamburger toggler (`.navbar-toggler`) che controlla il collapse del menu su dispositivi mobili (ISS-03)

- **BR-08:** La pagina Home (HomeController.Index) deve essere mantenuta come pagina informativa separata da Calculator, contenente presentazione dell'applicazione e link a Calculator (ISS-07)

---

## ⚠️ EDGE CASES

- **EC-01:** Utente accede a una route non esistente → gestito dal sistema (404 error page) — non modificato

- **EC-02:** JavaScript disabilitato su browser → menu deve comunque funzionare (link HTML standard, collapse potrebbe non funzionare ma voci menu rimangono accessibili) — non modificato

- **EC-03:** Utente naviga usando pulsanti back/forward del browser → lo stato attivo del menu deve aggiornarsi correttamente tramite helper method che legge ViewContext a ogni render — non modificato

- **EC-04:** Utente su mobile clicca voce menu con menu collapse aperto → menu si chiude automaticamente dopo navigazione (comportamento standard Bootstrap) (ISS-03)

**Updated by:** ISS-03 (EC-04 aggiunto)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Il progetto utilizza ASP.NET Core Razor Pages, quindi:
  - Calculator è una Razor Page (Pages/Calculator.cshtml)
  - Il menu va aggiunto in **Pages/Shared/_Layout.cshtml** (path standard Razor Pages) — se non esiste, verificare Views/Shared/_Layout.cshtml (ISS-05)
  - Tag helper per Calculator: `<a asp-page="/Calculator" class="nav-link @GetActiveClass("/Calculator")">Calculator</a>`
  
- **TN-02:** Per impostare Calculator come homepage (ISS-02):
  - **Approccio selezionato**: Aggiungere redirect in Program.cs prima di `app.Run()`:
    ```csharp
    app.MapGet("/", () => Results.Redirect("/Calculator"));
    ```
  - **Razionale**: Chiarezza, flessibilità, non modifica struttura file esistente

- **TN-03:** Per evidenziare la voce attiva (ISS-06):
  - **Approccio selezionato**: Usare helper method nel layout:
    ```csharp
    @functions {
        string GetActiveClass(string page, string? action = null)
        {
            var currentPage = ViewContext.RouteData.Values["page"]?.ToString();
            var currentAction = ViewContext.RouteData.Values["action"]?.ToString();
            
            // Per Razor Pages
            if (!string.IsNullOrEmpty(page) && currentPage == page)
                return "active";
            
            // Per Controller-based (Home, Privacy se sono controller)
            if (!string.IsNullOrEmpty(action) && currentAction == action)
                return "active";
            
            return "";
        }
    }
    ```
  - Uso: `class="nav-link @GetActiveClass("/Calculator")"`

- **TN-04:** Il layout attuale usa Bootstrap (già presente), quindi (ISS-03):
  - Navbar Bootstrap con classe `navbar-expand-lg` per breakpoint responsive
  - Hamburger toggler: `<button class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarNav">`
  - Menu collapse: `<div class="collapse navbar-collapse" id="navbarNav">`
  - Classe "active" di Bootstrap per highlight

- **TN-05:** La pagina Home (ISS-07):
  - HomeController.Index rimane accessibile tramite route /Home/Index o / (configurare solo redirect a /Calculator)
  - Contenuto suggerito: breve presentazione app, link "Vai alla Calcolatrice", link a Privacy
  - Voce menu "Home" punta a: `<a asp-controller="Home" asp-action="Index" class="nav-link @GetActiveClass(null, "Index")">Home</a>`

---

## ❓ OPEN QUESTIONS

- **OQ-02:** Il nome della voce di menu "Calculator" va bene o preferisci "Calcolatrice" (italiano) o "Calc" (abbreviato)?

- **OQ-03:** È necessario aggiungere un'icona accanto alle voci di menu per migliorare la UX? (es. icona calcolatrice, home, lock per privacy)

- **OQ-04:** Serve un breadcrumb oltre al menu principale per migliorare la navigazione?

**Resolved in v2:**
- ~~OQ-01: Voce Home~~ → risolto: mantenuta, punta a Index informativa (ISS-01, ISS-07)
- ~~OQ-05: Voce Home quando su Calculator~~ → risolto: sempre presente nel menu (ISS-01)

---

## 📊 Confidence

**Level:** high  
**Notes:** La specifica è stata notevolmente migliorata applicando le issue approvate. L'approccio tecnico per homepage (redirect), il path del layout (Pages/Shared), il meccanismo active state (helper method), il comportamento mobile (navbar-expand-lg), e il destino della pagina Index (informativa) sono tutti chiaramente definiti. Le open questions residue riguardano solo personalizzazioni UX non bloccanti.

---

## 📝 Change Log v1 → v2

**Applied Issues:**
- ISS-01: Chiarito ordine menu (Calculator, Home, Privacy) e ruolo di Home
- ISS-02: Specificato approccio redirect in Program.cs per homepage
- ISS-03: Aggiunto dettaglio comportamento responsive (navbar-expand-lg, hamburger toggler)
- ISS-04: Risolto tramite ISS-01 (ordine coerente)
- ISS-05: Specificato path layout: Pages/Shared/_Layout.cshtml
- ISS-06: Specificato approccio helper method per active state
- ISS-07: Chiarito che HomeController.Index diventa pagina informativa
- US-05: Aggiunta nuova user story per accesso Home informativa
- AF-03: Aggiunto flusso Home → Calculator
- BR-01 a BR-08: Aggiornate con dettagli implementativi
- TN-02, TN-03, TN-05: Aggiunti codici esempio concreti

**Skipped Issues:**
- ISS-08: Comportamento click voce già attiva (nice-to-have, non critico)
