# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-006  
**Name:** CalculatorSequentialOps

---

### 🧭 Business Context

**Description:**  
Correzione/estensione della calcolatrice web per supportare operazioni sequenziali tipiche di una calcolatrice standard. Attualmente, dopo un calcolo (es. 1+1=2), un'operazione successiva (es. +2=) non usa correttamente il risultato precedente e la seconda operazione torna 0.

**Goal:**
- Permettere operazioni concatenate utilizzando il risultato precedente come nuovo operando A
- Allineare il comportamento alla user experience di una calcolatrice standard
- Evitare che dopo un calcolo lo stato venga resettato in modo scorretto (operandA/operandB/currentInput)

---

### 👥 Actors

- **ACT-01:** Utente web — esegue calcoli consecutivi senza resettare manualmente la calcolatrice

---

### 🔎 Functional View

L'utente inserisce un'operazione (A op B) e preme "=" per ottenere il risultato. Subito dopo, l'utente desidera continuare con un'altra operazione usando quel risultato come base (nuovo A), senza dover reinserire manualmente il risultato.

Esempio desiderato:
- Inserisco: 1 + 1 = → visualizza 2
- Poi premo: + 2 = → visualizza 4

Comportamento attuale:
- Dopo il primo calcolo, la seconda operazione utilizza 0 (o perde lo stato) e produce un risultato errato.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Stato Calcolatrice**
  - **Description:** Stato della sessione di input dell'utente durante l'uso della calcolatrice
  - **Key Attributes:**
    - Display corrente (string/numero)
    - Operando A (double?)
    - Operando B (double?)
    - Operazione selezionata (string)
    - Flag: "risultato mostrato" (boolean)
  - **Related User Stories:** US-01, US-02

- **ENT-02 — Risultato**
  - **Description:** Output di una operazione, potenzialmente riutilizzabile come input per operazioni successive
  - **Key Attributes:**
    - Valore risultato (double)
    - Formattazione (2 decimali)
  - **Related User Stories:** US-01

---

### 🔗 Relationships

- **REL-01 — Risultato → Stato Calcolatrice**
  - **Description:** Dopo il calcolo, il risultato può diventare il nuovo operando A per un'operazione successiva

---

### 📜 Constraints

- **CON-01:** Dopo aver premuto "=", il risultato deve essere memorizzato e riutilizzabile come A
- **CON-02:** La digitazione di un nuovo numero dopo un risultato deve iniziare un nuovo input per B (o nuovo A se operazione non selezionata)
- **CON-03:** Il comportamento deve rimanere coerente con la gestione esistente di Clear, operazioni e percentuale
- **CON-04:** Il calcolo finale rimane server-side; lo stato utente viene gestito dal client (JavaScript)

---

## 🧩 USER STORIES

### US-01 — Operazioni sequenziali

**Descrizione:**
Come utente web voglio poter eseguire operazioni consecutive usando il risultato precedente come base (A) per il calcolo successivo, così da usare la calcolatrice come una calcolatrice standard.

**Related Flow:** MF-01

---

### US-02 — Comportamento dopo risultato

**Descrizione:**
Come utente web voglio che dopo la visualizzazione del risultato, l'inserimento di nuovi valori o la scelta di una nuova operazione aggiorni correttamente lo stato (A, B, display) senza resettare a 0.

**Related Flow:** MF-02, MF-03

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente inserisce A (es. 1), seleziona op (+), inserisce B (es. 1) e preme "="
- **MF-02:** Il sistema calcola e mostra il risultato (2)
- **MF-03:** L'utente seleziona una nuova operazione (+)
- **MF-04:** Il sistema imposta A = risultato precedente (2) e prepara l'inserimento di B
- **MF-05:** L'utente inserisce B (2) e preme "="
- **MF-06:** Il sistema calcola e mostra il nuovo risultato (4)

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Digitazione immediata dopo risultato

- **AF-01.1:** L'utente ottiene un risultato (2)
- **AF-01.2:** L'utente inizia subito a digitare un numero (es. 9) senza selezionare operazione
- **AF-01.3:** Il sistema inizia un nuovo calcolo impostando A = 9 (nuova operazione)

---

### AF-02 — Cambio operazione senza inserire B

- **AF-02.1:** L'utente ottiene un risultato
- **AF-02.2:** L'utente seleziona un'operazione e poi cambia operazione prima di inserire B
- **AF-02.3:** Il sistema mantiene A = risultato precedente e aggiorna solo l'operazione selezionata

---

## 📜 BUSINESS RULES

- **BR-01:** Dopo un "=", il risultato mostrato diventa il nuovo operando A (state update)
- **BR-02:** Dopo un "=", `currentInput` deve essere resettato per permettere l'inserimento del nuovo operando B quando viene selezionata una nuova operazione
- **BR-03:** Se l'utente digita un numero dopo un risultato senza selezionare operazione, inizia un nuovo calcolo (nuovo A)
- **BR-04:** Clear resetta completamente lo stato (A, B, operazione, display)

---

## ⚠️ EDGE CASES

- **EC-01:** Premere "=" ripetutamente senza nuovi input (ripete operazione? o nulla?)
- **EC-02:** Operazioni con percentuale dopo un risultato
- **EC-03:** Operazioni con decimali e rounding sequenziale

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Probabile causa: il JS non aggiorna operandA col risultato restituito dal server, o resetta operandA/operandB in modo errato.
- **TN-02:** Serve un meccanismo per sincronizzare lo stato client con il risultato mostrato (es. leggere il display/Model.Result al load e impostare operandA).

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Dopo un "=", premere subito un numero deve azzerare il risultato (nuovo calcolo) o continuare come inserimento di B?
- **OQ-02:** Premere "=" ripetutamente deve ripetere l'ultima operazione (es. 2 + 2 = 4, = 6, = 8) o non fare nulla?

---

## 📊 Confidence

**Level:** high  
**Notes:** Il comportamento desiderato è chiaro e tipico delle calcolatrici standard. La causa più probabile è una gestione incompleta dello stato JS dopo il POST: il risultato viene renderizzato server-side ma lo stato client non viene aggiornato, e quindi l'operazione successiva usa 0/null.
