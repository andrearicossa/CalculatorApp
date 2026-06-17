# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-007  
**Name:** CalculatorHistory

---

### 🧭 Business Context

**Description:**  
Estensione della calcolatrice web per memorizzare lo storico delle operazioni effettuate durante la sessione utente, fino a quando l'utente non preme il tasto Clear. Lo storico deve essere consultabile tramite un pulsante "History" che apre un popup con scrollbar. Chiudendo il popup, l'utente torna alla calcolatrice.

**Goal:**
- Memorizzare in sessione lo storico delle operazioni (A op B = Result)
- Mantenere lo storico finché non viene premuto Clear
- Visualizzare lo storico in un popup/modal con scrollbar
- Consentire apertura/chiusura del popup senza perdere lo stato della calcolatrice

---

### 👥 Actors

- **ACT-01:** Utente web — esegue calcoli e vuole consultare lo storico recente

---

### 🔎 Functional View

L'utente usa la calcolatrice per eseguire calcoli. Dopo ogni calcolo completato (pressione di "=" con risultato valido), l'operazione viene registrata nello storico in memoria di sessione. L'utente può premere il pulsante "History" vicino alla calcolatrice per aprire un popup che mostra l'elenco delle operazioni eseguite, scorribile se lungo. Quando l'utente chiude il popup (es. X o close), torna alla calcolatrice nello stesso stato. Premendo "Clear" lo storico viene cancellato insieme allo stato della calcolatrice.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — History Entry**
  - **Description:** Singola riga di storico che rappresenta un'operazione eseguita
  - **Key Attributes:**
    - Operando A
    - Operazione
    - Operando B
    - Risultato
    - Timestamp (opzionale)
  - **Related User Stories:** US-01, US-02

- **ENT-02 — History Session**
  - **Description:** Collezione di History Entry mantenuta in sessione fino a Clear
  - **Key Attributes:**
    - Lista ordinata di entries
    - Policy di reset (Clear)
    - Limite massimo entries (opzionale)
  - **Related User Stories:** US-01, US-03

- **ENT-03 — History Popup**
  - **Description:** Popup/modal UI per consultare lo storico
  - **Key Attributes:**
    - Visibilità (open/closed)
    - Scrollbar (contenitore scrollabile)
    - Pulsante di apertura (History)
    - Pulsante di chiusura
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — History Session → History Entry**
  - **Description:** La sessione contiene una lista di entry

- **REL-02 — History Popup → History Session**
  - **Description:** Il popup visualizza i contenuti della sessione

---

### 📜 Constraints

- **CON-01:** Lo storico è mantenuto in sessione finché non viene premuto Clear
- **CON-02:** Solo operazioni completate con successo vengono aggiunte allo storico
- **CON-03:** La UI deve rimanere semplice (Bootstrap) e senza framework frontend complessi
- **CON-04:** Il popup deve avere scrollbar quando le entry superano l'altezza disponibile
- **CON-05:** Apertura/chiusura popup non deve resettare la calcolatrice

---

## 🧩 USER STORIES

### US-01 — Registrazione operazioni nello storico

**Descrizione:**
Come utente web voglio che ogni operazione calcolata (A op B = Result) venga registrata nello storico per poterla rivedere successivamente nella stessa sessione.

**Related Flow:** MF-02, MF-03

---

### US-02 — Visualizzazione storico in popup

**Descrizione:**
Come utente web voglio premere un pulsante "History" per aprire un popup con scrollbar che mostra lo storico delle operazioni, così da consultarlo senza lasciare la calcolatrice.

**Related Flow:** MF-04, MF-05

---

### US-03 — Cancellazione storico con Clear

**Descrizione:**
Come utente web voglio che premendo "Clear" venga cancellato anche lo storico, così da ripartire da una sessione pulita.

**Related Flow:** MF-06

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente esegue un calcolo nella calcolatrice
- **MF-02:** L'utente preme "=" e ottiene un risultato
- **MF-03:** Il sistema aggiunge l'operazione allo storico in sessione
- **MF-04:** L'utente preme il pulsante "History"
- **MF-05:** Il sistema apre il popup e visualizza lo storico con scrollbar
- **MF-06:** L'utente chiude il popup e torna alla calcolatrice nello stesso stato
- **MF-07:** L'utente preme "Clear"
- **MF-08:** Il sistema resetta calcolatrice e cancella lo storico

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Nessuna entry nello storico
- **AF-01.1:** L'utente apre History prima di eseguire calcoli
- **AF-01.2:** Il popup mostra messaggio "No history" (o equivalente)

---

### AF-02 — Operazione fallita
- **AF-02.1:** L'utente esegue un calcolo che produce errore (es. div/0)
- **AF-02.2:** Il sistema mostra errore sulla calcolatrice
- **AF-02.3:** L'operazione NON viene aggiunta allo storico

---

## 📜 BUSINESS RULES

- **BR-01:** Aggiungere una entry allo storico solo se il calcolo è completato con successo (Result valido)
- **BR-02:** Lo storico è mantenuto per sessione utente e si resetta su Clear
- **BR-03:** Il popup History deve essere implementato con Bootstrap Modal (o equivalente semplice)
- **BR-04:** Il contenitore della lista history deve avere altezza fissa e overflow-y: auto per scrollbar
- **BR-05:** Closing del popup non modifica stato calcolatrice

---

## ⚠️ EDGE CASES

- **EC-01:** Molte operazioni → popup deve restare usabile con scrollbar
- **EC-02:** Operazioni molto lunghe (numeri grandi) → layout history deve gestire wrapping o ellipsis
- **EC-03:** Clear premuto mentre popup è aperto → popup mostra lista vuota o si chiude

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Memorizzazione "in sessione" in Razor Pages: usare Session State ASP.NET Core oppure mantenere history sul client e inviarla al server solo per render
- **TN-02:** Il progetto attuale usa JavaScript per gestire input; l'aggiunta history richiede un hook dopo calcolo riuscito (dopo POST response o prima del submit se si calcola lato server)
- **TN-03:** Il popup può essere un Bootstrap Modal markup nella stessa pagina Calculator

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Lo storico deve contenere anche l'operazione percentuale (es. 50 + 10% = 55)?
- **OQ-02:** Serve un limite massimo di entries (es. 50) per evitare crescita infinita?
- **OQ-03:** Il timestamp deve essere mostrato o è superfluo?
- **OQ-04:** In caso di operazioni sequenziali, come rappresentare l'espressione (es. "2 + 2 = 4")?

---

## 📊 Confidence

**Level:** medium  
**Notes:** La feature è chiara dal punto di vista UX, ma la parola "in sessione" può essere interpretata come server-side session (HttpContext.Session) o sessione client (runtime browser). Serve decidere l'approccio per mantenere coerenza con architettura corrente e semplicità.
