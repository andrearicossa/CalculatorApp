# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-007  
**Name:** CalculatorHistory

---

### 🧭 Business Context

**Description:**  
Estensione della calcolatrice web per memorizzare lo storico delle operazioni effettuate in **server-side session** finché non viene premuto il tasto Clear. Lo storico è visualizzato in un popup/modal con scrollbar apribile tramite un pulsante "History" vicino alla calcolatrice.

**Goal:**
- Memorizzare lo storico in sessione (persistente tra i postback POST/GET nella stessa sessione browser)
- Popolare lo storico solo su calcolo completato con successo
- Visualizzare lo storico in un Bootstrap Modal scrollabile
- Chiudere il popup senza alterare lo stato corrente della calcolatrice
- Resettare sia calcolatrice che history su Clear

---

### 👥 Actors

- **ACT-01:** Utente web — esegue calcoli e vuole consultare lo storico recente

---

### 🔎 Functional View

L'utente usa la calcolatrice per eseguire calcoli. Dopo ogni calcolo riuscito (`=` che produce `Result` senza errori), il sistema aggiunge una entry allo storico, mantenuto in sessione server-side così da non perderlo tra i postback (la calcolatrice effettua POST e ricarica la pagina). L'utente può aprire un popup con scrollbar tramite pulsante "History" per consultare le entry. Chiudendo il popup torna alla calcolatrice nello stesso stato (nessun reset). Premendo "Clear" viene cancellato anche lo storico.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — History Entry**
  - **Description:** Singola riga di storico che rappresenta un'operazione eseguita
  - **Key Attributes:**
    - A (double)
    - OperationSymbol (string: +, −, ×, ÷)
    - B (double)
    - Result (double)
    - CreatedAt (opzionale)
  - **Related User Stories:** US-01, US-02

- **ENT-02 — History Session**
  - **Description:** Collezione di History Entry mantenuta nella sessione server-side
  - **Key Attributes:**
    - Lista ordinata di entries
    - Reset policy: Clear
    - Max entries (opzionale; default: nessun limite)
  - **Related User Stories:** US-01, US-03

- **ENT-03 — History Popup (Modal)**
  - **Description:** Bootstrap modal per consultare lo storico
  - **Key Attributes:**
    - open/close
    - corpo scrollabile (overflow-y:auto o modal-dialog-scrollable)
    - lista entries o placeholder "No history"
  - **Related User Stories:** US-02

---

### 🔗 Relationships

- **REL-01 — History Session → History Entry**
  - **Description:** La sessione contiene una lista di entry

- **REL-02 — History Popup → History Session**
  - **Description:** Il popup visualizza i contenuti della sessione

---

### 📜 Constraints

- **CON-01:** Lo storico è mantenuto in sessione server-side finché non viene premuto Clear (ISS-01, ISS-03)
- **CON-02:** Solo calcoli completati con successo vengono aggiunti allo storico (BR-01)
- **CON-03:** UI semplice con Bootstrap Modal, senza framework frontend complessi
- **CON-04:** Il popup deve essere scrollabile (modal-dialog-scrollable o container con max-height + overflow-y:auto)
- **CON-05:** Aprire/chiudere popup non deve resettare calcolatrice

---

## 🧩 USER STORIES

### US-01 — Registrazione operazioni nello storico

**Descrizione:**
Come utente web voglio che ogni operazione calcolata con successo (A op B = Result) venga registrata nello storico per poterla rivedere successivamente nella stessa sessione.

**Related Flow:** MF-02, MF-03

---

### US-02 — Visualizzazione storico in popup

**Descrizione:**
Come utente web voglio premere "History" per aprire un popup con scrollbar che mostra lo storico delle operazioni, così da consultarlo senza lasciare la calcolatrice.

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
- **MF-03:** Il server, in OnPost, se il calcolo è riuscito, crea una HistoryEntry e la aggiunge alla HistorySession (in Session)
- **MF-04:** L'utente preme il pulsante "History"
- **MF-05:** Il sistema apre il Bootstrap Modal e visualizza la lista history (scrollabile)
- **MF-06:** L'utente chiude il modal e torna alla calcolatrice nello stesso stato
- **MF-07:** L'utente preme "Clear"
- **MF-08:** Il client resetta stato calcolatrice e il server cancella la history dalla sessione

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Nessuna entry nello storico

- **AF-01.1:** L'utente apre History prima di eseguire calcoli
- **AF-01.2:** Il popup mostra un placeholder "No history"

---

### AF-02 — Operazione fallita

- **AF-02.1:** L'utente esegue un calcolo che produce errore (es. div/0)
- **AF-02.2:** Il sistema mostra errore sulla calcolatrice
- **AF-02.3:** Il server NON aggiunge entry allo storico

---

### AF-03 — Clear mentre History è aperto

- **AF-03.1:** L'utente apre History (modal aperto)
- **AF-03.2:** L'utente preme Clear
- **AF-03.3:** Il sistema chiude il modal e mostra storico vuoto (o aggiorna lista a vuota)

---

## 📜 BUSINESS RULES

- **BR-01:** Aggiungere una entry allo storico solo se il calcolo è completato con successo (Result presente e nessun ErrorMessage)
- **BR-02:** Storage history: usare server-side session (HttpContext.Session) con serializzazione JSON della lista entries (ISS-01, ISS-03)
- **BR-03:** Il popup History è un Bootstrap Modal
- **BR-04:** Il corpo del modal deve essere scrollabile (modal-dialog-scrollable oppure max-height + overflow-y:auto)
- **BR-05:** Formato entry: `"{A:F2} {opSymbol} {B:F2} = {Result:F2}"` (ISS-05)
- **BR-06:** Clear cancella history nella sessione e resetta stato client (ISS-03)

---

## ⚠️ EDGE CASES

- **EC-01:** Molte operazioni → modal resta usabile grazie scrollbar
- **EC-02:** Numeri lunghi → wrapping/ellipsis nel layout della lista
- **EC-03:** Operazioni percentuali → history include i valori finali A,B già trasformati e l'operazione base (OQ-01)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Necessario abilitare Session in `Program.cs`: `AddDistributedMemoryCache`, `AddSession`, `UseSession`
- **TN-02:** Salvare history come JSON in session: key es. `"CalculatorHistory"`
- **TN-03:** In `CalculatorModel.OnPost`, se calcolo riuscito, append entry e salva
- **TN-04:** In UI, aggiungere un pulsante `History` che apre il modal (Bootstrap data-bs-toggle)

---

## ❓ OPEN QUESTIONS

- **OQ-01:** La history deve mostrare la % esplicitamente (es. "50 + 10% = 55") o i valori già trasformati? (default v2: mostra valori finali trasformati)
- **OQ-02:** Serve un limite massimo di entries (es. 50) per evitare crescita infinita?
- **OQ-03:** Il timestamp deve essere mostrato o è superfluo?

---

## 📊 Confidence

**Level:** high  
**Notes:** Le issue APPLY hanno chiarito storage (session server-side), hook su calcolo riuscito (OnPost), specifica modal Bootstrap e formato entry. Restano decisioni non bloccanti su limite entries e timestamp.
