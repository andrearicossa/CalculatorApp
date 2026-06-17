# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-001  
**Name:** SimpleWebCalculator

---

### 🧭 Business Context

**Description:**  
Sistema web che permette agli utenti di eseguire operazioni matematiche di base (addizione, sottrazione, moltiplicazione, divisione) attraverso un'interfaccia web semplice.

**Goal:**  
Fornire uno strumento accessibile via browser per calcoli matematici elementari con gestione degli errori comuni (divisione per zero, input non validi).

---

### 👥 Actors

- **ACT-01:** Utente web — utente finale che accede all'applicazione tramite browser per eseguire calcoli

---

### 🔎 Functional View

L'utente accede a una pagina web dove può inserire due numeri (operandi A e B), selezionare un'operazione matematica tra quattro disponibili, e ottenere il risultato del calcolo. L'elaborazione avviene lato server e il risultato viene visualizzato nella stessa pagina. Il sistema gestisce situazioni di errore comuni come input non validi, campi vuoti e divisione per zero.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calcolo**
  - **Description:** Rappresenta una singola operazione matematica richiesta dall'utente
  - **Key Attributes:**
    - Operando A (numero decimale)
    - Operando B (numero decimale)
    - Tipo operazione (addizione, sottrazione, moltiplicazione, divisione)
    - Risultato (numero decimale)
  - **Related User Stories:** US-01, US-02, US-03

- **ENT-02 — Operazione**
  - **Description:** Rappresenta un tipo specifico di operazione matematica disponibile
  - **Key Attributes:**
    - Codice operazione (add, sub, mul, div)
    - Nome visualizzabile
    - Logica di calcolo
  - **Related User Stories:** US-02

---

### 🔗 Relationships

- **REL-01 — Calcolo → Operazione**
  - **Description:** Ogni Calcolo utilizza esattamente un'Operazione per determinare il risultato da A e B

---

### 📜 Constraints

- **CON-01:** I valori A e B devono essere numeri decimali validi
- **CON-02:** Per l'operazione di divisione, B non può essere zero
- **CON-03:** Deve essere selezionata esattamente un'operazione tra le quattro disponibili
- **CON-04:** Tutti i campi obbligatori (A, B, Operazione) devono essere valorizzati prima del calcolo

---

## 🧩 USER STORIES

### US-01 — Inserimento dati operandi

**Descrizione:**  
Come utente web voglio poter inserire due numeri (A e B) in appositi campi input per poter eseguire un'operazione matematica su di essi.

**Related Flow:** MF-01, MF-02

---

### US-02 — Selezione operazione matematica

**Descrizione:**  
Come utente web voglio poter selezionare una delle quattro operazioni disponibili (addizione, sottrazione, moltiplicazione, divisione) per specificare quale calcolo eseguire sui numeri inseriti.

**Related Flow:** MF-01, MF-03

---

### US-03 — Esecuzione calcolo e visualizzazione risultato

**Descrizione:**  
Come utente web voglio poter avviare il calcolo tramite un pulsante "Calcola" e visualizzare il risultato nella stessa pagina per ottenere immediatamente il risultato dell'operazione.

**Related Flow:** MF-01, MF-04, MF-05, MF-06

---

### US-04 — Gestione errori di input

**Descrizione:**  
Come utente web voglio ricevere messaggi di errore chiari quando inserisco dati non validi (campi vuoti, valori non numerici, divisione per zero) per comprendere cosa correggere.

**Related Flow:** AF-01, AF-02, AF-03

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede alla pagina della calcolatrice
- **MF-02:** L'utente inserisce i valori numerici A e B nei rispettivi campi
- **MF-03:** L'utente seleziona l'operazione desiderata (addizione, sottrazione, moltiplicazione o divisione)
- **MF-04:** L'utente clicca sul pulsante "Calcola"
- **MF-05:** Il sistema elabora la richiesta lato server applicando l'operazione selezionata
- **MF-06:** Il risultato viene mostrato nella pagina aggiornata

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Input non valido

- **AF-01.1:** L'utente inserisce valori non numerici in A o B
- **AF-01.2:** Il sistema mostra un messaggio di errore indicando quali campi contengono valori non validi
- **AF-01.3:** L'utente corregge i valori e riprova

---

### AF-02 — Campi vuoti

- **AF-02.1:** L'utente clicca "Calcola" senza aver compilato tutti i campi obbligatori
- **AF-02.2:** Il sistema mostra un messaggio di errore indicando i campi mancanti
- **AF-02.3:** L'utente completa i campi e riprova

---

### AF-03 — Divisione per zero

- **AF-03.1:** L'utente seleziona "Divisione" e inserisce 0 come valore B
- **AF-03.2:** L'utente clicca "Calcola"
- **AF-03.3:** Il sistema intercetta la condizione e mostra un messaggio di errore specifico per divisione per zero
- **AF-03.4:** L'utente modifica il valore B e riprova

---

## 📜 BUSINESS RULES

- **BR-01:** Addizione: il risultato è la somma di A + B
- **BR-02:** Sottrazione: il risultato è la differenza A - B
- **BR-03:** Moltiplicazione: il risultato è il prodotto A * B
- **BR-04:** Divisione: se B ≠ 0, il risultato è il quoziente A / B; se B = 0, viene generato un errore
- **BR-05:** Tutti i valori numerici sono trattati come numeri decimali (double precision)
- **BR-06:** Il calcolo deve essere eseguito esclusivamente lato server

---

## ⚠️ EDGE CASES

- **EC-01:** Inserimento di valori estremamente grandi che potrebbero causare overflow
- **EC-02:** Inserimento di valori estremamente piccoli che potrebbero causare underflow
- **EC-03:** Risultato di una divisione che produce un numero con molte cifre decimali
- **EC-04:** Uso di caratteri speciali o spazi nei campi numerici
- **EC-05:** Tentativo di submit multipli consecutivi (doppio click su "Calcola")

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Architettura richiesta: ASP.NET Core MVC
- **TN-02:** Rendering: server-side (no framework frontend complessi)
- **TN-03:** UI: layout con Bootstrap per semplicità e responsività
- **TN-04:** Model suggerito con proprietà: A (double), B (double), Operation (string), Result (double)
- **TN-05:** Controller responsabile di: ricezione POST, validazione input, esecuzione logica
- **TN-06:** View responsabile di: form input, visualizzazione risultato

---

## ❓ OPEN QUESTIONS

- **OQ-01:** È necessario mantenere uno storico dei calcoli eseguiti dall'utente?
- **OQ-02:** Serve gestione della sessione utente o ogni calcolo è indipendente?
- **OQ-03:** Quali sono i limiti numerici accettabili per A e B (range validation)?
- **OQ-04:** È richiesta localizzazione per separatori decimali (virgola vs punto)?
- **OQ-05:** Serve logging delle operazioni per audit o debugging?
- **OQ-06:** È necessario implementare test automatici (unit/integration)?

---

## 📊 Confidence

**Level:** high  
**Notes:** La specifica iniziale è chiara e completa per un MVP. Le funzionalità core sono ben definite. Le open questions riguardano principalmente estensioni future o dettagli implementativi non critici per la prima versione.
