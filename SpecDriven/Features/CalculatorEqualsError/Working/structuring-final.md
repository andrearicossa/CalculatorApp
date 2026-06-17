# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-005  
**Name:** CalculatorEqualsError

---

### 🧭 Business Context

**Description:**  
Correzione di un bug critico nella calcolatrice web: il click sul tasto "=" (Calcola) causa un errore applicativo, impedendo all'utente di ottenere il risultato.

**Goal:**
- Identificare la causa del crash/errore quando l'utente preme "="
- Ripristinare il flusso di calcolo corretto, mostrando risultato o errore gestito
- Garantire che l'azione "=" non interrompa l'applicazione e non produca errori non gestiti

---

### 👥 Actors

- **ACT-01:** Utente web — utilizza la calcolatrice e preme "=" per ottenere il risultato

---

### 🔎 Functional View

L'utente utilizza il tastierino della calcolatrice per inserire un'operazione e preme "=" per eseguire il calcolo. Attualmente, questa azione manda in errore l'applicazione (crash o pagina di errore). Il sistema deve invece:
- accettare il submit del form
- validare input (A, B, Operation)
- eseguire il calcolo lato server tramite il servizio
- mostrare il risultato nel display oppure un messaggio di errore gestito (es. divisione per zero)

Per identificare correttamente la causa, il sistema deve essere in grado di distinguere tra:
- errore client-side (JavaScript: console error, DOM element not found, function not defined)
- errore server-side (exception in PageModel/CalculatorService, routing/layout mismatch)

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Input Calculator**
  - **Description:** Dati necessari per eseguire il calcolo
  - **Key Attributes:**
    - A (double?)
    - B (double?)
    - Operation (string)
  - **Related User Stories:** US-01

- **ENT-02 — Submission Flow**
  - **Description:** Flusso di submit del form e binding dei valori hidden inputs
  - **Key Attributes:**
    - Hidden inputs popolati da JavaScript (aInput, bInput, operationInput)
    - Submit handler che valida presenza operandi
    - Gestione stati incompleti (solo A, solo B, operazione non scelta)
  - **Related User Stories:** US-01

- **ENT-03 — Calculation Service**
  - **Description:** Componente server-side che esegue il calcolo
  - **Key Attributes:**
    - CalculatorService.Calculate(a, b, operation)
    - Validazione range, divisione per zero
    - Eccezioni gestite dal PageModel
  - **Related User Stories:** US-01

- **ENT-04 — Diagnostic Evidence**
  - **Description:** Evidenze raccolte per diagnosticare l'errore del tasto "="
  - **Key Attributes:**
    - Console browser (F12) error log
    - Network tab (POST /Calculator) status code
    - Output server/Visual Studio stacktrace
  - **Related User Stories:** US-01

---

### 🔗 Relationships

- **REL-01 — Submission Flow → Input Calculator**
  - **Description:** Il submit handler popola A/B/Operation prima della richiesta POST

- **REL-02 — Input Calculator → Calculation Service**
  - **Description:** Il PageModel riceve A/B/Operation e invoca il servizio di calcolo

- **REL-03 — Diagnostic Evidence → Submission Flow/Calculation Service**
  - **Description:** Le evidenze permettono di attribuire l'errore a client-side o server-side

---

### 📜 Constraints

- **CON-01:** Il tasto "=" deve sempre produrre una risposta gestita (risultato o messaggio), mai crash
- **CON-02:** Il validatore lato client non deve permettere submit con dati assenti o non numerici
- **CON-03:** Il server deve gestire errori noti (div/0, out of range) senza eccezioni non gestite
- **CON-04:** Devono essere definiti passi riproducibili per l'errore e evidenze minime da raccogliere

---

## 🧩 USER STORIES

### US-01 — Calcolo tramite tasto "="

**Descrizione:**
Come utente web voglio premere "=" per eseguire il calcolo inserito e vedere il risultato nel display, senza che l'applicazione vada in errore.

**Related Flow:** MF-01

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente inserisce A, seleziona operazione, inserisce B
- **MF-02:** L'utente preme "="
- **MF-03:** Il JavaScript popola i campi hidden A e B e consente il submit
- **MF-04:** Il server riceve POST, valida input, calcola il risultato e ritorna la pagina
- **MF-05:** Il display mostra il risultato

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Submit con dati mancanti
- **AF-01.1:** L'utente preme "=" senza aver inserito A o B
- **AF-01.2:** Il JavaScript blocca il submit e mostra un messaggio "Please enter both numbers."

---

### AF-02 — Errore server-side gestito
- **AF-02.1:** L'utente divide per zero o inserisce valori fuori range
- **AF-02.2:** Il server gestisce l'errore e mostra un messaggio nel layout

---

### AF-03 — Errore client-side (JavaScript)
- **AF-03.1:** L'utente preme "="
- **AF-03.2:** In console browser appare un errore JavaScript (es. function not defined, elemento DOM non trovato)
- **AF-03.3:** Il submit handler non popola correttamente hidden inputs o si interrompe
- **AF-03.4:** Il sistema non deve crashare: deve bloccare submit e mostrare messaggio coerente

---

### AF-04 — Errore server-side non gestito (da eliminare)
- **AF-04.1:** L'utente preme "="
- **AF-04.2:** Il server genera un'eccezione non gestita (500)
- **AF-04.3:** Il sistema deve essere modificato per intercettare l'eccezione e restituire ErrorMessage

---

## 📜 BUSINESS RULES

- **BR-01:** Il click su "=" deve attivare il submit del form e non deve generare eccezioni JavaScript
- **BR-02:** Prima del submit devono essere valorizzati i campi hidden `A` e `B` con numeri finiti
- **BR-03:** Il PageModel deve intercettare eccezioni note e restituire `ErrorMessage`
- **BR-04:** Il layout deve sempre rendere lo script della pagina (`@section Scripts`) per garantire il binding dei hidden inputs
- **BR-05:** Devono essere definiti passi minimi di diagnosi:
  - Console browser: errori JS
  - Network tab: status/response del POST
  - Output server: stacktrace

---

## ⚠️ EDGE CASES

- **EC-01:** Click su "=" con solo un operando inserito
- **EC-02:** Stato JS incoerente (operandA null, isEnteringB true)
- **EC-03:** Submit multipli rapidi
- **EC-04:** Errore solo dopo alcune operazioni (es. %)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Per diagnosticare l'errore, raccogliere evidenze minime (BR-05)
- **TN-02:** Possibili cause client:
  - errori JS nel submit handler
  - funzioni non definite (script non caricato)
  - DOM ids cambiati/mismatch
- **TN-03:** Possibili cause server:
  - binding fallito (A/B null) con eccezione
  - eccezione in CalculatorService
  - route/layout mismatch

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Qual è l'errore specifico mostrato quando si preme "="? (messaggio, stacktrace)
- **OQ-02:** L'errore è visibile in console browser (JavaScript) o nei log server?
- **OQ-03:** Il problema avviene sempre o solo in specifici casi (es. dopo %)?

---

## 📊 Confidence

**Level:** medium  
**Notes:** La specifica è stata migliorata applicando le issue APPROVE: ora include un flusso di diagnosi e distingue esplicitamente le categorie di errore. Tuttavia, resta necessario ottenere l'evidenza dell'errore reale per implementare una fix deterministica.
