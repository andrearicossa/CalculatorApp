# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-008  
**Name:** CalculatorHistoryClearImmediate

---

### 🧭 Business Context

**Description:**  
Bugfix sulla funzionalità History: l'utente preme "Clear" e si aspetta che la History si svuoti immediatamente. Attualmente, la History si svuota solo dopo che l'utente esegue un'altra operazione (tipicamente perché la UI non viene aggiornata e continua a mostrare lo storico precedente finché non avviene un nuovo render).

**Goal:**
- Cancellare la history in sessione immediatamente al click su Clear
- Aggiornare la UI del popup History in modo che mostri subito "No history" senza necessità di eseguire un'altra operazione
- Evitare side effect indesiderati dovuti a `Session.Clear()` (che potrebbe cancellare altri dati di sessione)

---

### 👥 Actors

- **ACT-01:** Utente web — usa la calcolatrice e desidera azzerare history e stato

---

### 🔎 Functional View

L'utente esegue alcune operazioni e lo storico viene popolato. L'utente preme "Clear". Il sistema deve:
1. Resettare lo stato della calcolatrice (display, operandi, operazione)
2. Cancellare lo storico in sessione
3. Se l'utente apre o ha aperto il popup History, deve visualizzare subito lo storico vuoto ("No history")

Comportamento attuale:
- La sessione viene pulita, ma il popup (o la UI) mostra ancora lo storico precedente finché non avviene un nuovo render (eseguendo una nuova operazione).

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — History Session**
  - **Description:** Lista di History Entry salvata in sessione server-side
  - **Key Attributes:**
    - Session key: CalculatorHistory
    - Operazioni: append, clear
  - **Related User Stories:** US-01, US-02

- **ENT-02 — History Popup**
  - **Description:** Modal UI che mostra la lista history
  - **Key Attributes:**
    - Stato UI (open/closed)
    - Contenuto lista (render server-side) e/o (update client-side)
  - **Related User Stories:** US-01, US-02

- **ENT-03 — Clear Action**
  - **Description:** Azione utente che resetta calcolatrice e history
  - **Key Attributes:**
    - Clear client-side (reset input)
    - Clear server-side (session)
    - Coerenza immediata UI
  - **Related User Stories:** US-01

---

### 🔗 Relationships

- **REL-01 — Clear Action → History Session**
  - **Description:** Clear cancella immediatamente i dati in sessione

- **REL-02 — Clear Action → History Popup**
  - **Description:** Clear aggiorna immediatamente la UI del popup per mostrare storico vuoto

---

### 📜 Constraints

- **CON-01:** La cancellazione history deve essere immediata e visibile subito in UI
- **CON-02:** Evitare `Session.Clear()` se cancella dati non correlati (preferibile removal mirata della key)
- **CON-03:** Nessun refresh pagina completo richiesto per vedere history vuota

---

## 🧩 USER STORIES

### US-01 — Clear svuota history immediatamente

**Descrizione:**
Come utente web voglio che quando premo Clear, la History venga cancellata e risulti vuota immediatamente, senza dover eseguire un'altra operazione.

**Related Flow:** MF-01

---

### US-02 — Popup History riflette subito lo stato

**Descrizione:**
Come utente web voglio che aprendo (o avendo aperto) il popup History dopo Clear, venga mostrato subito "No history".

**Related Flow:** MF-02

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente preme Clear
- **MF-02:** Il client resetta lo stato calcolatrice (display/operandi)
- **MF-03:** Il client invia richiesta POST a handler server-side per cancellare history
- **MF-04:** Il server cancella la history in sessione (key specifica)
- **MF-05:** Il client aggiorna immediatamente anche la UI del modal (lista svuotata / placeholder "No history")

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Clear mentre popup è aperto

- **AF-01.1:** L'utente ha il popup History aperto
- **AF-01.2:** L'utente preme Clear
- **AF-01.3:** Il popup aggiorna immediatamente il contenuto a "No history" senza chiudersi o con chiusura opzionale

---

## 📜 BUSINESS RULES

- **BR-01:** Il handler server-side di ClearHistory deve rimuovere especificamente la key `CalculatorHistory` (non `Session.Clear()`), salvo esplicita necessità
- **BR-02:** Dopo risposta OK del ClearHistory, il client deve svuotare la lista history nel DOM (update immediato)
- **BR-03:** Il popup history deve mostrare placeholder se lista vuota

---

## ⚠️ EDGE CASES

- **EC-01:** Clear immediato e utente apre History senza refresh pagina
- **EC-02:** Clear mentre una richiesta POST precedente è in corso

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Attualmente history viene renderizzata server-side in Razor; senza re-render la UI resta con vecchi dati. Serve un update client-side del modal.
- **TN-02:** La fetch POST a `?handler=ClearHistory` deve includere anti-forgery token.

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Quando Clear è premuto con popup aperto: preferisci che il popup resti aperto mostrando "No history" o che si chiuda automaticamente?

---

## 📊 Confidence

**Level:** high  
**Notes:** La root cause è chiara: il contenuto del modal è renderizzato server-side e non viene aggiornato senza refresh. La soluzione è aggiornare il DOM del modal in `clearAll()` dopo la fetch di ClearHistory.
