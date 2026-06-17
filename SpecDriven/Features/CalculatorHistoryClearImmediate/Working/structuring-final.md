# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-008  
**Name:** CalculatorHistoryClearImmediate

---

### 🧭 Business Context

**Description:**  
Bugfix sulla funzionalità History: quando l'utente preme "Clear" si aspetta che la History risulti vuota immediatamente. Attualmente l'utente percepisce che la History si svuoti solo dopo aver eseguito un'altra operazione.

**Goal:**
- Rendere la cancellazione della History immediata al click su Clear
- Fare in modo che l'apertura del popup History dopo Clear mostri subito "No history"
- Mantenere l'esperienza d'uso della calcolatrice invariata (Clear resetta lo stato e la History)

---

### 👥 Actors

- **ACT-01:** Utente web — usa la calcolatrice e desidera azzerare history e stato

---

### 🔎 Functional View

L'utente esegue alcune operazioni e lo storico viene popolato. Quando preme "Clear", il sistema deve resettare la calcolatrice e svuotare lo storico in modo immediatamente visibile. Se l'utente apre il popup History subito dopo Clear, deve visualizzare "No history" senza dover effettuare ulteriori calcoli.

Decisione UX:
- Se il popup History è aperto quando l'utente preme Clear, il popup resta aperto ma il suo contenuto viene aggiornato mostrando lo storico vuoto.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — History Session**
  - **Description:** Collezione di entry di storico associata alla sessione utente
  - **Key Attributes:**
    - Lista entries
    - Stato "vuoto" dopo Clear
  - **Related User Stories:** US-01, US-02

- **ENT-02 — History Popup**
  - **Description:** Popup/modal che mostra lo storico
  - **Key Attributes:**
    - Open/closed
    - Contenuto: lista o placeholder "No history"
  - **Related User Stories:** US-02

- **ENT-03 — Clear Action**
  - **Description:** Azione utente che resetta calcolatrice e history
  - **Key Attributes:**
    - Reset stato calcolatrice
    - Reset history
    - Aggiornamento immediato del popup
  - **Related User Stories:** US-01, US-02

---

### 🔗 Relationships

- **REL-01 — Clear Action → History Session**
  - **Description:** Clear svuota lo storico

- **REL-02 — Clear Action → History Popup**
  - **Description:** Clear aggiorna il contenuto visualizzato nel popup in modo immediato

---

### 📜 Constraints

- **CON-01:** La History deve apparire vuota immediatamente dopo Clear
- **CON-02:** La cancellazione della History non deve impattare altre informazioni utente non correlate
- **CON-03:** Non è richiesto un cambio pagina o un nuovo calcolo per vedere la History vuota

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
- **MF-02:** Il sistema resetta lo stato della calcolatrice
- **MF-03:** Il sistema svuota lo storico
- **MF-04:** Se il popup History è aperto, il contenuto viene aggiornato mostrando "No history"
- **MF-05:** Se l'utente apre History dopo Clear, visualizza subito "No history"

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Clear mentre popup è aperto

- **AF-01.1:** L'utente ha il popup History aperto
- **AF-01.2:** L'utente preme Clear
- **AF-01.3:** Il popup rimane aperto e mostra lo storico vuoto

---

## 📜 BUSINESS RULES

- **BR-01:** Clear deve svuotare lo storico in modo immediato
- **BR-02:** Dopo Clear, il popup History deve mostrare placeholder "No history"
- **BR-03:** Lo storico deve essere cancellato in modo mirato (solo History), senza effetti collaterali su altre informazioni

---

## ⚠️ EDGE CASES

- **EC-01:** Clear e apertura immediata di History
- **EC-02:** Clear premuto più volte consecutivamente

---

## ❓ OPEN QUESTIONS

- Nessuna per MVP (comportamento definito)

---

## 📊 Confidence

**Level:** high  
**Notes:** Il comportamento atteso è chiaro: Clear deve rendere la History vuota immediatamente e in modo visibile, sia con popup aperto sia con popup chiuso.
