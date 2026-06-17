# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-003  
**Name:** CalculatorUIFix

---

### 🧭 Business Context

**Description:**  
Correzione di bug critici che impediscono il corretto funzionamento della pagina Calculator: mancata applicazione degli stili Bootstrap, tastierino numerico non funzionante, ed errore al submit del form.

**Goal:**  
- Ripristinare l'applicazione corretta degli stili Bootstrap alla pagina Calculator
- Rendere funzionante il tastierino numerico per l'inserimento dei valori
- Risolvere l'errore generato al click del pulsante "=" (Calcola)
- Garantire che la pagina Calculator usi il layout condiviso corretto con Bootstrap

---

### 👥 Actors

- **ACT-01:** Utente web — utente che tenta di utilizzare la calcolatrice e riscontra malfunzionamenti

---

### 🔎 Functional View

L'utente accede alla pagina Calculator e riscontra tre problemi principali: (1) l'interfaccia appare "spartana" senza formattazione Bootstrap (pulsanti non stilizzati, layout grezzo), (2) il click sui pulsanti del tastierino numerico non produce alcun effetto sul display, (3) il click sul pulsante "=" genera un errore di sistema. La causa radice è l'assenza di un layout Razor Pages dedicato (Pages/Shared/_Layout.cshtml) che includa Bootstrap e lo script section. La pagina Calculator non eredita il layout MVC (Views/Shared/_Layout.cshtml) in quanto è una Razor Page, non una View MVC.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calculator Page**
  - **Description:** Razor Page che implementa la calcolatrice
  - **Key Attributes:**
    - Path: Pages/Calculator.cshtml
    - Model: CalculatorModel (PageModel)
    - Layout: dovrebbe usare Pages/Shared/_Layout.cshtml
    - Scripts: JavaScript per tastierino in @section Scripts
  - **Related User Stories:** US-01, US-02, US-03

- **ENT-02 — Layout Razor Pages**
  - **Description:** Layout condiviso per Razor Pages (mancante)
  - **Key Attributes:**
    - Path previsto: Pages/Shared/_Layout.cshtml
    - Contenuto: Bootstrap CSS/JS, navbar, footer, @RenderBody(), @RenderSection("Scripts")
  - **Related User Stories:** US-01

- **ENT-03 — JavaScript Keypad Logic**
  - **Description:** Logica client-side per il funzionamento del tastierino
  - **Key Attributes:**
    - Funzioni: appendToDisplay, setOperation, clearForm, updateDisplay
    - Event listeners: form submit, button clicks
    - Stato: currentInput, operandA, operandB, operation, isEnteringB
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — Calculator Page → Layout Razor Pages**
  - **Description:** Calculator dovrebbe usare il layout Razor Pages per ereditare Bootstrap e struttura

- **REL-02 — Calculator Page → JavaScript Keypad Logic**
  - **Description:** La pagina include JavaScript nella section Scripts che deve essere renderizzata dal layout

---

### 📜 Constraints

- **CON-01:** Calculator è una Razor Page (Pages/Calculator.cshtml), non una View MVC, quindi NON può usare Views/Shared/_Layout.cshtml
- **CON-02:** Razor Pages richiede un layout dedicato in Pages/Shared/_Layout.cshtml o configurazione esplicita in Pages/_ViewStart.cshtml
- **CON-03:** Il layout Razor Pages deve includere riferimenti a Bootstrap 5 (CSS + JS)
- **CON-04:** Il layout deve contenere @await RenderSectionAsync("Scripts", required: false) per caricare lo script del tastierino
- **CON-05:** Il JavaScript del tastierino richiede che il DOM sia completamente caricato prima dell'esecuzione (DOMContentLoaded)

---

## 🧩 USER STORIES

### US-01 — Applicazione stili Bootstrap alla pagina Calculator

**Descrizione:**  
Come utente web voglio che la pagina Calculator abbia una formattazione professionale con stili Bootstrap applicati correttamente (pulsanti colorati, griglia responsive, tipografia curata) per un'esperienza visiva gradevole.

**Related Flow:** MF-01, MF-02

---

### US-02 — Funzionamento tastierino numerico

**Descrizione:**  
Come utente web voglio che il click sui pulsanti del tastierino numerico (0-9, punto decimale, operazioni) aggiorni il display e memorizzi i valori per poter inserire i numeri da calcolare senza dover digitare manualmente.

**Related Flow:** MF-03, MF-04

---

### US-03 — Esecuzione calcolo senza errori

**Descrizione:**  
Come utente web voglio che il click sul pulsante "=" esegua il calcolo senza generare errori di sistema per ottenere il risultato dell'operazione matematica.

**Related Flow:** MF-05, MF-06

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede a /Calculator
- **MF-02:** Il sistema carica Calculator.cshtml con layout Pages/Shared/_Layout.cshtml (che include Bootstrap)
- **MF-03:** L'utente visualizza la calcolatrice con stili Bootstrap applicati (pulsanti colorati, griglia allineata)
- **MF-04:** L'utente clicca su un pulsante numerico (es. "7")
- **MF-05:** Il JavaScript esegue appendToDisplay('7'), aggiorna currentInput, e aggiorna il display mostrando "7"
- **MF-06:** L'utente clicca su un'operazione (es. "+"), JavaScript memorizza operandA e imposta operation
- **MF-07:** L'utente inserisce secondo numero (es. "5"), JavaScript aggiorna currentInput
- **MF-08:** L'utente clicca "=" (Calcola)
- **MF-09:** JavaScript popola i campi nascosti (A, B, Operation) e submit il form
- **MF-10:** Il server (CalculatorModel.OnPost) elabora, calcola il risultato, e ritorna la pagina aggiornata
- **MF-11:** Il display mostra il risultato calcolato

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Layout non trovato (situazione attuale)

- **AF-01.1:** L'utente accede a /Calculator
- **AF-01.2:** Razor Pages cerca Pages/Shared/_Layout.cshtml ma non lo trova
- **AF-01.3:** Se non esiste Pages/_ViewStart.cshtml, la pagina viene renderizzata senza layout
- **AF-01.4:** Bootstrap CSS/JS non vengono caricati
- **AF-01.5:** La pagina appare "spartana" senza formattazione

---

### AF-02 — JavaScript non caricato (situazione attuale)

- **AF-02.1:** La pagina Calculator include JavaScript in @section Scripts
- **AF-02.2:** Se il layout non ha @RenderSection("Scripts"), lo script non viene renderizzato
- **AF-02.3:** Le funzioni appendToDisplay, setOperation non sono definite
- **AF-02.4:** Il click sui pulsanti genera errori console JavaScript
- **AF-02.5:** Il tastierino non funziona

---

### AF-03 — Form submit senza dati (situazione attuale)

- **AF-03.1:** L'utente clicca "=" senza che JavaScript abbia popolato i campi nascosti
- **AF-03.2:** Il form viene submitted con A=null, B=null
- **AF-03.3:** CalculatorModel.OnPost riceve A=null, B=null
- **AF-03.4:** La validazione `if (!A.HasValue || !B.HasValue)` fallisce
- **AF-03.5:** Viene impostato ErrorMessage = "Please enter both numbers."
- **AF-03.6:** Se il JavaScript ha un errore critico, potrebbe bloccare il submit o generare exception

---

## 📜 BUSINESS RULES

- **BR-01:** Calculator (Razor Page) DEVE usare un layout Razor Pages (Pages/Shared/_Layout.cshtml)
- **BR-02:** Il layout Razor Pages deve includere Bootstrap 5 CSS nell'header (<link> in <head>)
- **BR-03:** Il layout Razor Pages deve includere Bootstrap 5 JS prima del closing </body>
- **BR-04:** Il layout deve contenere `@await RenderSectionAsync("Scripts", required: false)` per caricare script custom
- **BR-05:** Il JavaScript del tastierino deve essere wrappato in DOMContentLoaded o posizionato alla fine del body
- **BR-06:** Le funzioni JavaScript (appendToDisplay, setOperation, clearForm, updateDisplay) devono essere definite prima dell'uso
- **BR-07:** Il layout Razor Pages deve essere configurato in Pages/_ViewStart.cshtml per applicarsi automaticamente a tutte le Razor Pages

---

## ⚠️ EDGE CASES

- **EC-01:** Bootstrap CSS non caricato → stili default browser, aspetto "spartano"
- **EC-02:** Bootstrap JS non caricato → componenti interattivi (collapse, modals) non funzionano
- **EC-03:** @section Scripts non renderizzato → JavaScript custom non eseguito, tastierino non funzionante
- **EC-04:** JavaScript eseguito prima del caricamento DOM → elementi non trovati, event listeners non attachati
- **EC-05:** Form submit prima che JavaScript popoli hidden inputs → validazione fallisce, errore "Please enter both numbers"

---

## 🧪 TECHNICAL NOTES

- **TN-01:** **Problema principale**: Calculator è una Razor Page ma non esiste Pages/Shared/_Layout.cshtml
  
- **TN-02:** **Razor Pages vs MVC Views**:
  - Razor Pages (Pages/) usa layout da Pages/Shared/_Layout.cshtml o configurato in Pages/_ViewStart.cshtml
  - MVC Views (Views/) usa layout da Views/Shared/_Layout.cshtml
  - Calculator è in Pages/ quindi NON usa automaticamente Views/Shared/_Layout.cshtml

- **TN-03:** **Soluzioni possibili**:
  - **Opzione 1** (Raccomndata): Creare Pages/Shared/_Layout.cshtml copiando e adattando da Views/Shared/_Layout.cshtml
  - **Opzione 2**: Creare Pages/_ViewStart.cshtml che specifica `Layout = "~/Views/Shared/_Layout.cshtml"` (cross-reference tra Pages e Views)
  - **Opzione 3**: Spostare Calculator.cshtml da Pages/ a Views/ e convertirla in View MVC (non raccomandato, richiede refactoring)

- **TN-04:** **Bootstrap nella pagina Calculator**:
  - Attualmente Calculator.cshtml include CSS custom nella @section Styles (se esiste)
  - Manca il link a Bootstrap CSS/JS nel layout
  - Soluzione: il layout Pages/Shared/_Layout.cshtml deve includere Bootstrap da CDN o file locali

- **TN-05:** **JavaScript tastierino**:
  - Attualmente in @section Scripts alla fine di Calculator.cshtml
  - Richiede che il layout abbia `@await RenderSectionAsync("Scripts", required: false)`
  - Soluzione: assicurarsi che il nuovo layout Razor Pages includa questa direttiva

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Il progetto ha Bootstrap installato localmente (wwwroot/lib/bootstrap) o usa CDN?
- **OQ-02:** Esiste già un Pages/_ViewStart.cshtml o va creato?
- **OQ-03:** Il layout MVC (Views/Shared/_Layout.cshtml) è aggiornato con navbar Calculator/Home/Privacy o contiene solo menu legacy?
- **OQ-04:** Serve unificare i layout (uno solo per MVC e Razor Pages) o mantenerli separati?

---

## 📊 Confidence

**Level:** high  
**Notes:** La causa radice dei problemi è chiara e ben identificata: mancanza di layout Razor Pages per la pagina Calculator. La soluzione è straightforward: creare Pages/Shared/_Layout.cshtml con Bootstrap e RenderSection per Scripts. Il problema non è nella logica di business (CalculatorService funziona) né nel JavaScript (il codice è corretto), ma nella configurazione architetturale del progetto Razor Pages.
