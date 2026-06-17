# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-006  
**Name:** CalculatorSequentialOps

---

### 🧭 Business Context

**Description:**  
Correzione/estensione della calcolatrice web per supportare operazioni sequenziali tipiche di una calcolatrice standard. Attualmente, dopo un calcolo (es. 1+1=2), un'operazione successiva (es. +2=) non usa correttamente il risultato precedente e la seconda operazione torna 0.

**Goal:**
- Permettere operazioni concatenate utilizzando il risultato precedente come nuovo operando A
- Aggiornare correttamente lo stato JS dopo il POST (sincronizzazione risultato → operandA)
- Definire comportamento standard post-risultato (nuova digitazione vs selezione operazione)
- Gestire interazione con percentuale (%) e flag anti-%%

---

### 👥 Actors

- **ACT-01:** Utente web — esegue calcoli consecutivi senza resettare manualmente la calcolatrice

---

### 🔎 Functional View

Dopo aver premuto "=", il risultato viene renderizzato server-side e mostrato nel display. Per consentire operazioni sequenziali, il client deve:
- memorizzare quel risultato come `operandA`
- impostare uno stato `resultShown=true`
- preparare correttamente l'inserimento del nuovo operando (B) quando l'utente seleziona una nuova operazione

Comportamento desiderato:
- `1 + 1 =` → display `2.00`
- poi `+ 2 =` → display `4.00`

Decisioni applicate:
- Digitazione di un numero subito dopo un risultato **inizia un nuovo calcolo** (reset stato, nuova A) (ISS-02)
- Pressione ripetuta di `=` senza nuovi input **non fa nulla** (MVP) (ISS-03)
- Dopo un risultato, `percentApplied` viene resettato (ISS-04)

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Stato Calcolatrice**
  - **Description:** Stato della sessione di input dell'utente durante l'uso della calcolatrice
  - **Key Attributes:**
    - Display corrente (string)
    - Operando A (double?)
    - Operando B (double?)
    - Operazione selezionata (string)
    - isEnteringB (boolean)
    - resultShown (boolean)
    - percentApplied (boolean)
  - **Related User Stories:** US-01, US-02

- **ENT-02 — Risultato**
  - **Description:** Output di una operazione, riutilizzabile come input
  - **Key Attributes:**
    - Valore risultato (double)
    - Formattazione (2 decimali)
  - **Related User Stories:** US-01

---

### 🔗 Relationships

- **REL-01 — Risultato → Stato Calcolatrice**
  - **Description:** Dopo il calcolo, il risultato diventa `operandA` e abilita operazioni sequenziali

---

### 📜 Constraints

- **CON-01:** Dopo "=", il risultato deve essere sincronizzato nello stato JS (`operandA`)
- **CON-02:** Se l'utente seleziona un'operazione dopo un risultato, il sistema prepara l'inserimento di B mantenendo A=result
- **CON-03:** Se l'utente digita un numero dopo un risultato senza selezionare operazione, inizia un nuovo calcolo (reset stato)
- **CON-04:** `percentApplied` deve essere resettato dopo un risultato e dopo cambio operazione

---

## 🧩 USER STORIES

### US-01 — Operazioni sequenziali

**Descrizione:**
Come utente web voglio poter eseguire operazioni consecutive usando il risultato precedente come base (A) per il calcolo successivo.

**Related Flow:** MF-01

---

### US-02 — Comportamento dopo risultato

**Descrizione:**
Come utente web voglio che dopo la visualizzazione del risultato, l'inserimento di nuovi valori o la scelta di una nuova operazione aggiorni correttamente lo stato (A, B, display) senza resettare a 0.

**Related Flow:** MF-02, MF-03

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente esegue `1 + 1 =` e il server ritorna la pagina con display `2.00`
- **MF-02:** Al `DOMContentLoaded`, il client legge il display e imposta `operandA = 2.00`, `resultShown = true`, `isEnteringB = false`
- **MF-03:** L'utente preme `+` → il client mantiene `operandA=2.00`, imposta `operation=add`, `isEnteringB=true`, resetta `currentInput` per B
- **MF-04:** L'utente preme `2` → `currentInput=2`
- **MF-05:** L'utente preme `=` → submit con `A=2`, `B=2`, `Operation=add`
- **MF-06:** Il server calcola e ritorna `4.00`

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Digitazione immediata dopo risultato

- **AF-01.1:** L'utente ottiene un risultato (es. 2.00) e `resultShown=true`
- **AF-01.2:** L'utente digita subito un numero (es. 9) senza selezionare operazione
- **AF-01.3:** Il client resetta lo stato e inizia un nuovo calcolo: `operandA=null`, `operandB=null`, `operation=add`, `isEnteringB=false`, `resultShown=false`, `currentInput="9"`

---

### AF-02 — Premere '=' ripetutamente

- **AF-02.1:** L'utente ha appena ottenuto un risultato e non ha inserito nuovi input
- **AF-02.2:** L'utente preme '='
- **AF-02.3:** Il sistema non esegue alcuna nuova operazione (nessun submit) (MVP)

---

## 📜 BUSINESS RULES

- **BR-01:** Sync result: al load pagina, se nel display è presente un risultato calcolato, impostare `operandA` su quel valore e `resultShown=true`
- **BR-02:** Se `resultShown=true` e l'utente seleziona un'operazione, preparare inserimento B mantenendo A
- **BR-03:** Se `resultShown=true` e l'utente digita un numero senza selezionare operazione, iniziare un nuovo calcolo (reset stato)
- **BR-04:** Premere '=' senza nuovi input non attiva submit (MVP)
- **BR-05:** Dopo un risultato, resettare `percentApplied=false`

---

## ⚠️ EDGE CASES

- **EC-01:** Risultato 0.00 → comunque valido come operandA
- **EC-02:** Operazioni con % dopo un risultato → `%` deve lavorare sul nuovo input, non sul vecchio

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Implementazione principale in JavaScript in `Pages/Calculator.cshtml`
- **TN-02:** Aggiungere variabile `resultShown` e logica in `DOMContentLoaded` per inizializzare `operandA` dal display
- **TN-03:** Aggiornare `appendToDisplay` per resettare stato se `resultShown=true` e `!isEnteringB`
- **TN-04:** Dopo submit riuscito, la pagina viene ricaricata: la sincronizzazione avviene sempre su load

---

## 📊 Confidence

**Level:** high  
**Notes:** La causa radice è la mancata sincronizzazione tra risultato server-side e stato JS. La soluzione è standard: introdurre `resultShown` e inizializzare `operandA` dal display al load, con regole chiare su digitazione e selezione operazione.
