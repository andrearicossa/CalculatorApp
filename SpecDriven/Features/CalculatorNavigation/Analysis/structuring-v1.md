# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-002  
**Name:** CalculatorNavigation

---

### 🧭 Business Context

**Description:**  
Miglioramento dell'esperienza utente dell'applicazione web SimpleWebCalculator attraverso l'integrazione della pagina Calculator nel menu di navigazione principale e la sua impostazione come pagina di default (homepage).

**Goal:**  
- Rendere la pagina Calculator facilmente accessibile tramite il menu di navigazione principale (header)
- Impostare Calculator come landing page predefinita dell'applicazione
- Uniformare l'esperienza di navigazione con le altre pagine esistenti (Index, Privacy)

---

### 👥 Actors

- **ACT-01:** Utente web — utente che accede all'applicazione e naviga tra le pagine

---

### 🔎 Functional View

L'utente accede all'applicazione web e viene automaticamente reindirizzato alla pagina Calculator (homepage). Nel menu di navigazione presente nell'header della pagina, l'utente visualizza tre voci: "Calculator", "Home" (o "Index"), e "Privacy". L'utente può cliccare su queste voci per navigare tra le diverse sezioni dell'applicazione. Il menu è presente in tutte le pagine e mantiene coerenza visiva e funzionale.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Menu Navigation**
  - **Description:** Rappresenta il sistema di navigazione dell'applicazione web
  - **Key Attributes:**
    - Voci di menu (lista di link)
    - Pagina attiva corrente
    - Layout del menu (header/top navigation)
  - **Related User Stories:** US-01, US-02

- **ENT-02 — Route Configuration**
  - **Description:** Configurazione delle route dell'applicazione
  - **Key Attributes:**
    - Default route (homepage)
    - Named routes per ogni pagina
    - Route pattern
  - **Related User Stories:** US-03

---

### 🔗 Relationships

- **REL-01 — Menu Navigation → Route Configuration**
  - **Description:** Ogni voce di menu punta a una route configurata nell'applicazione

---

### 📜 Constraints

- **CON-01:** Il menu di navigazione deve essere presente in tutte le pagine dell'applicazione
- **CON-02:** La pagina Calculator deve essere accessibile tramite route root ("/")
- **CON-03:** Le voci di menu devono includere: Calculator, Home, Privacy
- **CON-04:** La voce di menu corrispondente alla pagina corrente deve essere visivamente evidenziata (active state)
- **CON-05:** Il menu deve essere responsive e funzionare su desktop e mobile

---

## 🧩 USER STORIES

### US-01 — Aggiunta Calculator al menu di navigazione

**Descrizione:**  
Come utente web voglio vedere la voce "Calculator" nel menu di navigazione principale per poter accedere facilmente alla calcolatrice da qualsiasi pagina dell'applicazione.

**Related Flow:** MF-01, MF-02

---

### US-02 — Navigazione tra pagine tramite menu

**Descrizione:**  
Come utente web voglio poter cliccare sulle voci del menu (Calculator, Home, Privacy) per navigare tra le diverse sezioni dell'applicazione in modo intuitivo.

**Related Flow:** MF-03

---

### US-03 — Calculator come pagina di default

**Descrizione:**  
Come utente web voglio essere reindirizzato automaticamente alla pagina Calculator quando accedo all'applicazione (URL root "/") per avere accesso immediato alla funzionalità principale.

**Related Flow:** MF-04

---

### US-04 — Evidenziazione pagina attiva nel menu

**Descrizione:**  
Come utente web voglio vedere visivamente evidenziata la voce di menu corrispondente alla pagina in cui mi trovo per capire chiaramente la mia posizione nell'applicazione.

**Related Flow:** MF-02, MF-03

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede all'applicazione digitando l'URL root (es. https://localhost/)
- **MF-02:** Il sistema visualizza la pagina Calculator con il menu di navigazione nell'header
- **MF-03:** L'utente visualizza nel menu tre voci: "Calculator" (evidenziata), "Home", "Privacy"
- **MF-04:** L'utente clicca su una voce di menu (es. "Privacy")
- **MF-05:** Il sistema naviga alla pagina selezionata
- **MF-06:** Il menu aggiorna lo stato attivo evidenziando la nuova pagina corrente

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Accesso diretto a una pagina specifica

- **AF-01.1:** L'utente accede direttamente a una pagina specifica digitando l'URL completo (es. /Privacy)
- **AF-01.2:** Il sistema mostra la pagina richiesta con il menu di navigazione
- **AF-01.3:** Il menu evidenzia la voce corrispondente alla pagina corrente

---

### AF-02 — Navigazione su dispositivo mobile

- **AF-02.1:** L'utente accede all'applicazione da dispositivo mobile
- **AF-02.2:** Il menu di navigazione si adatta al layout mobile (hamburger menu o layout responsive)
- **AF-02.3:** L'utente può aprire/chiudere il menu e navigare tra le pagine

---

## 📜 BUSINESS RULES

- **BR-01:** La route root ("/") deve reindirizzare o mappare direttamente alla pagina Calculator
- **BR-02:** Il menu di navigazione deve essere implementato nel layout condiviso (_Layout.cshtml) per essere presente in tutte le pagine
- **BR-03:** Le voci di menu devono utilizzare tag helper ASP.NET Core per generare URL corretti (asp-page o asp-controller/asp-action)
- **BR-04:** La voce di menu attiva deve avere una classe CSS specifica (es. "active") per l'evidenziazione visiva
- **BR-05:** L'ordine delle voci nel menu deve essere: Calculator (prima), Home, Privacy
- **BR-06:** Se l'applicazione usa Razor Pages, Calculator deve essere mappata come DefaultPage o la route root deve reindirizzare a /Calculator

---

## ⚠️ EDGE CASES

- **EC-01:** Utente accede a una route non esistente → gestito dal sistema (404 error page)
- **EC-02:** JavaScript disabilitato su browser → menu deve comunque funzionare (link HTML standard)
- **EC-03:** Utente naviga usando pulsanti back/forward del browser → lo stato attivo del menu deve aggiornarsi correttamente

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Il progetto utilizza ASP.NET Core Razor Pages, quindi:
  - Calculator è una Razor Page (Pages/Calculator.cshtml)
  - Il menu va aggiunto in Views/Shared/_Layout.cshtml (o Pages/Shared/_Layout.cshtml se esiste)
  - Tag helper: `<a asp-page="/Calculator">Calculator</a>`
  
- **TN-02:** Per impostare Calculator come homepage:
  - Opzione 1: Configurare MapRazorPages con convenzione per default page
  - Opzione 2: Aggiungere redirect in Program.cs: `app.MapGet("/", () => Results.Redirect("/Calculator"))`
  - Opzione 3: Rinominare Calculator.cshtml in Index.cshtml nella root di Pages

- **TN-03:** Per evidenziare la voce attiva:
  - Usare ViewContext per determinare la pagina corrente
  - Aggiungere classe CSS "active" condizionalmente
  - Esempio: `class="@(ViewContext.RouteData.Values["page"]?.ToString() == "/Calculator" ? "active" : "")"`

- **TN-04:** Il layout attuale usa Bootstrap (già presente), quindi:
  - Navbar Bootstrap già configurata in _Layout.cshtml
  - Aggiungere nuova voce di menu nella navbar esistente
  - Classe "active" di Bootstrap per highlight

---

## ❓ OPEN QUESTIONS

- **OQ-01:** La voce "Home" deve puntare alla pagina Index esistente o deve essere rimossa dato che Calculator è la homepage?
- **OQ-02:** Il nome della voce di menu deve essere "Calculator" o un nome diverso (es. "Calcolatrice", "Calc")?
- **OQ-03:** È necessario aggiungere un'icona accanto alle voci di menu?
- **OQ-04:** Serve un breadcrumb oltre al menu principale?
- **OQ-05:** Quando l'utente è su Calculator (homepage), la voce "Home" nel menu deve comunque essere presente o può essere nascosta?

---

## 📊 Confidence

**Level:** high  
**Notes:** La richiesta è chiara e ben definita. L'implementazione è straightforward in un contesto Razor Pages. Gli aspetti tecnici sono standard per ASP.NET Core. Le open questions riguardano principalmente decisioni di UX che non bloccano l'implementazione base.
