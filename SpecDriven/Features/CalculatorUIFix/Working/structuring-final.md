# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-003  
**Name:** CalculatorUIFix

---

### 🧭 Business Context

**Description:**  
Correzione di bug critici che impediscono il corretto funzionamento della pagina Calculator: mancata applicazione degli stili Bootstrap, tastierino numerico non funzionante, ed errore al submit del form. La causa radice è l'assenza di un layout Razor Pages dedicato che includa Bootstrap e supporti le sezioni Scripts/Styles.

**Goal:**  
- Creare layout Razor Pages dedicato (Pages/Shared/_Layout.cshtml) con Bootstrap locale
- Configurare _ViewStart per applicare automaticamente il layout alle Razor Pages
- Ripristinare l'applicazione corretta degli stili Bootstrap alla pagina Calculator
- Rendere funzionante il tastierino numerico per l'inserimento dei valori
- Risolvere l'errore generato al click del pulsante "=" (Calcola)
- Garantire coerenza UI tra pagine MVC e Razor Pages tramite navbar condiviso

---

### 👥 Actors

- **ACT-01:** Utente web — utente che tenta di utilizzare la calcolatrice e riscontra malfunzionamenti
- **ACT-02:** Sviluppatore — persona che implementa la fix seguendo l'implementation plan

---

### 🔎 Functional View

L'utente accede alla pagina Calculator e attualmente riscontra tre problemi: (1) interfaccia "spartana" senza formattazione Bootstrap, (2) tastierino numerico non reagisce ai click, (3) errore al submit del form. Dopo l'applicazione della fix, l'utente accede a /Calculator e visualizza un'interfaccia professionale con stili Bootstrap (pulsanti colorati, griglia responsive, tipografia curata). Il click sui pulsanti del tastierino aggiorna il display in tempo reale, e il click su "=" esegue il calcolo senza errori, mostrando il risultato. La soluzione implementa un layout Razor Pages dedicato (Pages/Shared/_Layout.cshtml) che include riferimenti Bootstrap locali (wwwroot/lib/bootstrap), supporta @section Scripts per caricare JavaScript custom, e include lo stesso navbar delle pagine MVC per coerenza UI.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calculator Page**
  - **Description:** Razor Page che implementa la calcolatrice
  - **Key Attributes:**
    - Path: Pages/Calculator.cshtml
    - Model: CalculatorModel (PageModel)
    - Layout: Pages/Shared/_Layout.cshtml (configurato via _ViewStart)
    - Scripts: JavaScript per tastierino in @section Scripts
    - Styles: CSS custom in @section Styles (opzionale)
  - **Related User Stories:** US-01, US-02, US-03

- **ENT-02 — Layout Razor Pages**
  - **Description:** Layout condiviso per Razor Pages
  - **Key Attributes:**
    - Path: Pages/Shared/_Layout.cshtml
    - Bootstrap: Riferimenti locali (wwwroot/lib/bootstrap/dist/css & js)
    - Navbar: Copiato da Views/Shared/_Layout.cshtml (Calculator, Home, Privacy)
    - Sections supportate: @RenderSectionAsync("Styles") e @RenderSectionAsync("Scripts")
    - Helper: GetActiveClass function per highlight pagina attiva
  - **Related User Stories:** US-01, US-04

- **ENT-03 — _ViewStart Configuration**
  - **Description:** File di configurazione per applicare layout automaticamente
  - **Key Attributes:**
    - Path: Pages/_ViewStart.cshtml
    - Contenuto: `Layout = "_Layout"`
  - **Related User Stories:** US-01

- **ENT-04 — JavaScript Keypad Logic**
  - **Description:** Logica client-side per il funzionamento del tastierino
  - **Key Attributes:**
    - Location: @section Scripts in Calculator.cshtml
    - Funzioni: appendToDisplay, setOperation, clearForm, updateDisplay
    - Event listeners: form submit, button clicks
    - Stato: currentInput, operandA, operandB, operation, isEnteringB
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — Calculator Page → Layout Razor Pages**
  - **Description:** Calculator usa il layout Razor Pages tramite configurazione _ViewStart

- **REL-02 — Calculator Page → JavaScript Keypad Logic**
  - **Description:** La pagina include JavaScript nella section Scripts che viene renderizzata dal layout

- **REL-03 — Layout Razor Pages → Bootstrap (locale)**
  - **Description:** Il layout referenzia file CSS e JS di Bootstrap da wwwroot/lib/bootstrap

- **REL-04 — _ViewStart Configuration → Layout Razor Pages**
  - **Description:** _ViewStart specifica quale layout applicare a tutte le Razor Pages

---

### 📜 Constraints

- **CON-01:** Calculator è una Razor Page (Pages/Calculator.cshtml), quindi DEVE usare layout da Pages/Shared/ o configurato in Pages/_ViewStart.cshtml (ISS-03: Opzione 1)
- **CON-02:** Il layout Razor Pages deve includere riferimenti a Bootstrap 5 LOCALI da wwwroot/lib/bootstrap (ISS-01)
- **CON-03:** Il layout deve contenere `@await RenderSectionAsync("Scripts", required: false)` per caricare lo script del tastierino
- **CON-04:** Il layout deve contenere `@await RenderSectionAsync("Styles", required: false)` per supportare CSS custom (ISS-05)
- **CON-05:** Il JavaScript del tastierino richiede che il DOM sia completamente caricato prima dell'esecuzione (wrappato in DOMContentLoaded)
- **CON-06:** Il nuovo layout Razor Pages deve includere lo stesso navbar di Views/Shared/_Layout.cshtml per coerenza UI (ISS-04)
- **CON-07:** Pages/_ViewStart.cshtml deve specificare `Layout = "_Layout"` per applicare automaticamente il layout (ISS-02)

---

## 🧩 USER STORIES

### US-01 — Applicazione stili Bootstrap alla pagina Calculator

**Descrizione:**  
Come utente web voglio che la pagina Calculator abbia una formattazione professionale con stili Bootstrap applicati correttamente (pulsanti colorati, griglia responsive, tipografia curata) per un'esperienza visiva gradevole.

**Related Flow:** MF-01, MF-02, MF-03

**Updated by:** ISS-01 (Bootstrap locale), ISS-03 (layout dedicato), ISS-04 (navbar), ISS-05 (section Styles)

---

### US-02 — Funzionamento tastierino numerico

**Descrizione:**  
Come utente web voglio che il click sui pulsanti del tastierino numerico (0-9, punto decimale, operazioni) aggiorni il display e memorizzi i valori per poter inserire i numeri da calcolare senza dover digitare manualmente.

**Related Flow:** MF-04, MF-05, MF-06

---

### US-03 — Esecuzione calcolo senza errori

**Descrizione:**  
Come utente web voglio che il click sul pulsante "=" esegua il calcolo senza generare errori di sistema per ottenere il risultato dell'operazione matematica.

**Related Flow:** MF-07, MF-08, MF-09

---

### US-04 — Coerenza UI tra pagine (Nuovo)

**Descrizione:**  
Come utente web voglio che la pagina Calculator abbia lo stesso navbar e aspetto visivo delle altre pagine dell'applicazione (Home, Privacy) per un'esperienza coerente durante la navigazione.

**Related Flow:** MF-03

**Added by:** ISS-04 (navbar copiato da Views layout)

---

## 🔄 MAIN FLOW

- **MF-01:** Lo sviluppatore crea directory Pages/Shared/ se non esiste
- **MF-02:** Lo sviluppatore crea Pages/Shared/_Layout.cshtml copiando contenuto da Views/Shared/_Layout.cshtml (ISS-03, ISS-04)
- **MF-03:** Lo sviluppatore verifica che il nuovo layout includa:
  - Bootstrap CSS/JS da wwwroot/lib/bootstrap (ISS-01)
  - Navbar con Calculator/Home/Privacy + GetActiveClass helper (ISS-04)
  - `@await RenderSectionAsync("Styles", required: false)` in <head> (ISS-05)
  - `@await RenderSectionAsync("Scripts", required: false)` prima di </body>
- **MF-04:** Lo sviluppatore crea Pages/_ViewStart.cshtml con `Layout = "_Layout"` (ISS-02)
- **MF-05:** L'applicazione viene riavviata
- **MF-06:** L'utente accede a /Calculator
- **MF-07:** Il sistema carica Calculator.cshtml che ora eredita Pages/Shared/_Layout.cshtml (via _ViewStart)
- **MF-08:** Il layout renderizza Bootstrap CSS, navbar, @RenderBody (Calculator content), e @section Scripts (JavaScript tastierino)
- **MF-09:** L'utente visualizza la calcolatrice con stili Bootstrap applicati correttamente
- **MF-10:** L'utente clicca su un pulsante numerico (es. "7")
- **MF-11:** Il JavaScript (ora caricato) esegue appendToDisplay('7'), aggiorna currentInput = "7", e aggiorna il display
- **MF-12:** L'utente clicca su un'operazione (es. "+"), JavaScript memorizza operandA=7 e imposta operation="add"
- **MF-13:** L'utente inserisce secondo numero (es. "5"), JavaScript aggiorna currentInput="5"
- **MF-14:** L'utente clicca "=" (Calcola)
- **MF-15:** JavaScript popola i campi nascosti (A=7, B=5, Operation="add") e submit il form
- **MF-16:** Il server (CalculatorModel.OnPost) elabora, calcola 7+5=12.00, e ritorna la pagina aggiornata
- **MF-17:** Il display mostra il risultato "12.00"

**Updated by:** ISS-01, ISS-02, ISS-03, ISS-04, ISS-05

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Verifica Bootstrap locale (pre-implementazione)

- **AF-01.1:** Lo sviluppatore verifica esistenza di wwwroot/lib/bootstrap
- **AF-01.2:** Se esiste: procede con riferimenti locali (ISS-01 risolto)
- **AF-01.3:** Se non esiste: installa Bootstrap via LibMan o NuGet prima di procedere

**Added by:** ISS-01

---

### AF-02 — Directory Pages/Shared già esistente

- **AF-02.1:** Lo sviluppatore tenta di creare Pages/Shared/
- **AF-02.2:** Se già esiste: verifica contenuto (potrebbe esserci già _Layout.cshtml parziale)
- **AF-02.3:** Se _Layout.cshtml esiste: sovrascrive o rinomina backup prima di copiare

---

### AF-03 — Conflitto _ViewStart esistente

- **AF-03.1:** Lo sviluppatore tenta di creare Pages/_ViewStart.cshtml
- **AF-03.2:** Se già esiste: verifica contenuto
- **AF-03.3:** Se specifica layout diverso: aggiorna a `Layout = "_Layout"`
- **AF-03.4:** Se ha configurazioni custom: preserva e integra con Layout assignment

**Added by:** ISS-02

---

## 📜 BUSINESS RULES

- **BR-01:** Calculator (Razor Page) DEVE usare layout Razor Pages da Pages/Shared/_Layout.cshtml (ISS-03: Opzione 1 selezionata)
- **BR-02:** Il layout Razor Pages deve includere Bootstrap 5 CSS da wwwroot/lib/bootstrap/dist/css/bootstrap.min.css (ISS-01)
- **BR-03:** Il layout Razor Pages deve includere Bootstrap 5 JS da wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js (ISS-01)
- **BR-04:** Il layout deve contenere `@await RenderSectionAsync("Scripts", required: false)` prima del closing </body> per caricare script custom
- **BR-05:** Il layout deve contenere `@await RenderSectionAsync("Styles", required: false)` in <head> dopo Bootstrap CSS per CSS custom (ISS-05)
- **BR-06:** Il JavaScript del tastierino deve essere wrappato in DOMContentLoaded o posizionato alla fine del body
- **BR-07:** Le funzioni JavaScript (appendToDisplay, setOperation, clearForm, updateDisplay) devono essere definite prima dell'uso
- **BR-08:** Il layout Razor Pages deve essere configurato in Pages/_ViewStart.cshtml per applicarsi automaticamente a tutte le Razor Pages (ISS-02)
- **BR-09:** Il layout Razor Pages deve includere lo stesso navbar di Views/Shared/_Layout.cshtml (con Calculator, Home, Privacy e GetActiveClass helper) per coerenza UI (ISS-04)
- **BR-10:** Bootstrap deve essere referenziato da path LOCALE wwwroot/lib/bootstrap, NON da CDN (ISS-01: decisione applicata)

**Updated by:** ISS-01, ISS-02, ISS-03, ISS-04, ISS-05

---

## ⚠️ EDGE CASES

- **EC-01:** Bootstrap CSS non caricato → stili default browser, aspetto "spartano" (RISOLTO da ISS-01, ISS-03)
- **EC-02:** Bootstrap JS non caricato → componenti interattivi (collapse, modals) non funzionano (RISOLTO da ISS-01, ISS-03)
- **EC-03:** @section Scripts non renderizzato → JavaScript custom non eseguito, tastierino non funzionante (RISOLTO da ISS-03: layout include RenderSectionAsync)
- **EC-04:** JavaScript eseguito prima del caricamento DOM → elementi non trovati, event listeners non attachati (GESTITO: JavaScript in @section Scripts caricato dopo body)
- **EC-05:** Form submit prima che JavaScript popoli hidden inputs → validazione fallisce, errore "Please enter both numbers" (RISOLTO: JavaScript ora viene caricato correttamente)
- **EC-06:** Pages/Shared/ directory non esiste → creazione fallisce se parent directory non esiste (GESTITO: creare con -Force in PowerShell)
- **EC-07:** Conflitto tra layout MVC e Razor Pages → modifiche a uno non si propagano all'altro (ACCETTATO: separazione intenzionale per manutenibilità)

**Updated by:** ISS-01, ISS-02, ISS-03

---

## 🧪 TECHNICAL NOTES

- **TN-01:** **Causa radice confermata**: Calculator è una Razor Page ma non esiste Pages/Shared/_Layout.cshtml né Pages/_ViewStart.cshtml
  
- **TN-02:** **Soluzione selezionata** (ISS-03): **Opzione 1** - Creare Pages/Shared/_Layout.cshtml copiando da Views/Shared/_Layout.cshtml
  - **Vantaggi**: Separazione pulita, manutenibilità, standard ASP.NET Core
  - **Implementazione**: Copy/paste Views/Shared/_Layout.cshtml → Pages/Shared/_Layout.cshtml + verifiche

- **TN-03:** **Bootstrap verificato** (ISS-01): Presente in wwwroot/lib/bootstrap (confermato via file system check)
  - **Riferimenti**: `~/lib/bootstrap/dist/css/bootstrap.min.css` e `~/lib/bootstrap/dist/js/bootstrap.bundle.min.js`
  - **Versione**: Bootstrap 5.x (da verificare nel file package se necessario)

- **TN-04:** **Navbar coerenza** (ISS-04): Views/Shared/_Layout.cshtml è già aggiornato con navbar Calculator/Home/Privacy (FEAT-002)
  - **Contenuto da copiare**: Linee 14-33 circa (navbar HTML) + @functions block con GetActiveClass (fine file)
  - **Helper GetActiveClass**: Gestisce sia Razor Pages (page route) che MVC (controller/action)

- **TN-05:** **_ViewStart configurazione** (ISS-02): NON esiste, va creato
  - **Path**: Pages/_ViewStart.cshtml
  - **Contenuto minimo**: `@{ Layout = "_Layout"; }`

- **TN-06:** **Section Styles supporto** (ISS-05): Aggiunto per permettere CSS custom per singole pagine
  - **Posizionamento**: In <head> dopo Bootstrap CSS link
  - **Sintassi**: `@await RenderSectionAsync("Styles", required: false)`

- **TN-07:** **JavaScript tastierino**: Attualmente in @section Scripts alla fine di Calculator.cshtml
  - **Verifica**: Il codice JavaScript è corretto, richiede solo che il layout lo renderizzi
  - **Nessuna modifica necessaria**: Calculator.cshtml non richiede modifiche, solo il layout

- **TN-08:** **ISS-06 SKIPPED**: Tipo errore JavaScript ("ReferenceError: appendToDisplay is not defined") è chiarimento tecnico, non impatta implementazione

---

## ❓ OPEN QUESTIONS

**Resolved in v2:**
- ~~OQ-01: Bootstrap locale o CDN?~~ → RISOLTO: Locale (ISS-01)
- ~~OQ-02: Pages/_ViewStart.cshtml esiste?~~ → RISOLTO: NO, creare (ISS-02)
- ~~OQ-03: Quale approccio?~~ → RISOLTO: Opzione 1 (ISS-03)
- ~~OQ-04: Layout MVC aggiornato?~~ → RISOLTO: SÌ, con navbar (ISS-04)

**Remaining:**
- **OQ-NEW-01:** Serve aggiornare documentazione FEAT-001 (SimpleWebCalculator) per menzionare la fix del layout?
- **OQ-NEW-02:** Altre Razor Pages nel progetto (es. Privacy se è Razor Page) beneficeranno automaticamente del nuovo layout?

---

## 📊 Confidence

**Level:** very high  
**Notes:** Tutte le decisioni critiche sono state prese e verificate tramite file system check. La soluzione è chiara: creare Pages/Shared/_Layout.cshtml + Pages/_ViewStart.cshtml. Bootstrap è presente localmente, il navbar MVC è aggiornato, e la struttura della fix è standard ASP.NET Core. Non ci sono ambiguità residue. L'implementation plan è dettagliato e pronto per l'esecuzione.

---

## 📝 Change Log v1 → v2

**Applied Issues:**
- ISS-01: Specificato uso Bootstrap LOCALE da wwwroot/lib/bootstrap
- ISS-02: Aggiunto requisito creazione Pages/_ViewStart.cshtml
- ISS-03: Selezionata **Opzione 1** (Pages/Shared/_Layout.cshtml dedicato) come soluzione ufficiale
- ISS-04: Specificato contenuto da copiare da Views/Shared/_Layout.cshtml (navbar + GetActiveClass)
- ISS-05: Aggiunto supporto @RenderSectionAsync("Styles") nel layout

**Skipped Issues:**
- ISS-06: Chiarimento tipo errore JavaScript (low priority, non bloccante)

**New Entities:**
- ENT-03: _ViewStart Configuration
- ENT-04: JavaScript Keypad Logic (esplicitato)

**New User Story:**
- US-04: Coerenza UI tra pagine

**New Relationships:**
- REL-03, REL-04

**Updated Business Rules:**
- BR-01 a BR-10: Tutte aggiornate con decisioni specifiche da ISS-01 a ISS-05

**Updated Main Flow:**
- MF-01 a MF-17: Espanso per includere step di implementazione e utilizzo post-fix

**Updated Alternative Flows:**
- AF-01: Verifica Bootstrap locale (nuovo)
- AF-02, AF-03: Gestione conflitti directory/file esistenti (nuovi)

**Updated Technical Notes:**
- TN-02 a TN-08: Completamente riscritti con decisioni concrete e path verificati
