# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-004  
**Name:** CalculatorPercentage

---

### 🧭 Business Context

**Description:**  
Estensione della web calculator esistente (SimpleWebCalculator) per includere la funzionalità di calcolo della percentuale, mantenendo interfaccia a tastierino, Bootstrap e calcolo server-side.

**Goal:**
- Consentire all'utente di calcolare percentuali con un pulsante "%" nella UI
- Supportare il comportamento contestuale tipico di una calcolatrice standard per le operazioni con percentuale
- Mantenere le validazioni e regole esistenti (range ±10^15, divisione per zero, arrotondamento a 2 decimali)

---

### 👥 Actors

- **ACT-01:** Utente web — utilizza la calcolatrice dal browser

---

### 🔎 Functional View

L'utente utilizza la calcolatrice web per eseguire operazioni. Oltre alle operazioni base (+, −, ×, ÷), può utilizzare una funzione percentuale tramite un pulsante "%" nel tastierino. La funzione percentuale agisce in modo contestuale:

- Se l'utente sta inserendo un numero (senza un operando A e senza operazione selezionata), allora `%` trasforma il numero corrente in `x/100`.
- Se invece esiste un operando A e un'operazione selezionata e l'utente sta inserendo il secondo operando B, allora `%` trasforma B in una percentuale relativa ad A secondo la semantica delle calcolatrici:
  - `A + B%` → `A + (A * B/100)`
  - `A - B%` → `A - (A * B/100)`
  - `A * B%` → `A * (B/100)`
  - `A / B%` → `A / (B/100)` (con gestione divisione per zero se B=0)

Il calcolo finale rimane server-side. La pressione ripetuta del tasto `%` sullo stesso operando non applica la percentuale più volte: viene ignorata fino a nuova digitazione.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calcolo**
  - **Description:** Singola operazione matematica richiesta dall'utente
  - **Key Attributes:**
    - Operando A (double)
    - Operando B (double)
    - Operazione (string: add, sub, mul, div)
    - Risultato (double)
  - **Related User Stories:** US-01, US-02, US-03

- **ENT-02 — Percentuale**
  - **Description:** Trasformazione percentuale applicabile a un operando (x → x/100) o a B in modo relativo ad A (contestuale)
  - **Key Attributes:**
    - Operando target (A oppure B)
    - Modalità: semplice (x/100) o contestuale (relativa ad A)
    - Flag applicata (per evitare %%)
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — Calcolo ↔ Percentuale**
  - **Description:** La percentuale può trasformare il valore dell'operando corrente prima dell'esecuzione del calcolo

---

### 📜 Constraints

- **CON-01:** Il calcolo finale deve rimanere server-side (CalculatorService)
- **CON-02:** Il pulsante "%" deve essere integrato nel tastierino senza framework frontend complessi
- **CON-03:** La percentuale deve seguire semantica contestuale (calcolatrice standard) come descritto nella Functional View
- **CON-04:** La pressione ripetuta `%` sullo stesso operando non deve applicare percentuale più volte (anti-%%)
- **CON-05:** Devono rimanere valide le validazioni esistenti (range ±10^15, NaN/Infinity, divisione per zero)
- **CON-06:** Tutti i risultati devono essere arrotondati a 2 decimali

---

## 🧩 USER STORIES

### US-01 — Visualizzazione pulsante percentuale

**Descrizione:**
Come utente web voglio vedere un pulsante "%" nella UI della calcolatrice per poter accedere alla funzione percentuale.

**Related Flow:** MF-01

---

### US-02 — Percentuale come trasformazione del numero corrente

**Descrizione:**
Come utente web voglio poter trasformare il numero corrente in percentuale (x → x/100) cliccando "%" per ottenere rapidamente il valore percentuale.

**Related Flow:** MF-02, MF-03

---

### US-03 — Percentuale contestuale con operazioni base

**Descrizione:**
Come utente web voglio poter usare la percentuale insieme a +, −, ×, ÷ con semantica da calcolatrice standard (es. 50 + 10% = 55) per velocizzare i calcoli.

**Related Flow:** MF-04, MF-05

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede alla pagina Calculator e visualizza anche il pulsante "%"
- **MF-02:** (Caso semplice) L'utente inserisce un numero e clicca "%"
- **MF-03:** Il display mostra il valore trasformato (x/100) e il sistema marca la percentuale come applicata per evitare `%%`
- **MF-04:** (Caso contestuale) L'utente inserisce A, seleziona un'operazione, inserisce B
- **MF-05:** L'utente clicca "%" mentre sta inserendo B: il sistema trasforma B in base ad A e all'operazione selezionata
- **MF-06:** L'utente clicca "=" e il sistema esegue il calcolo server-side con i valori finali di A, B e Operation
- **MF-07:** Il sistema mostra il risultato (2 decimali)

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Percentuale con input mancante

- **AF-01.1:** L'utente clicca "%" senza aver inserito alcun numero
- **AF-01.2:** Il sistema ignora l'azione e mantiene il display invariato oppure mostra un messaggio di errore (da definire in UI)

---

### AF-02 — Percentuale con valori fuori range

- **AF-02.1:** L'utente usa valori che portano il risultato fuori dal range ±10^15
- **AF-02.2:** Il sistema mostra un messaggio di errore coerente con la validazione esistente

---

## 📜 BUSINESS RULES

- **BR-01:** `%` in modalità semplice: se non esiste un contesto `A + operazione`, allora `%` trasforma il numero corrente `x` in `x/100`
- **BR-02:** `%` in modalità contestuale: se esiste un operando A e un'operazione selezionata e l'utente sta inserendo B, allora:
  - `add`: `B = A * (B/100)`
  - `sub`: `B = A * (B/100)`
  - `mul`: `B = B/100`
  - `div`: `B = B/100`
- **BR-03:** Calcolo finale:
  - `add`: `Result = A + B`
  - `sub`: `Result = A - B`
  - `mul`: `Result = A * B`
  - `div`: `Result = A / B` (se B != 0)
- **BR-04:** Anti-%%: se `%` è già stato applicato sull'operando corrente, ulteriori pressioni di `%` vengono ignorate fino a nuova digitazione o clear
- **BR-05:** Il calcolo finale e le validazioni di range/arrotodamento restano server-side

---

## ⚠️ EDGE CASES

- **EC-01:** Pressione ripetuta del pulsante "%" (es. 20%%) → ignorata (BR-04)
- **EC-02:** Percentuale su numero negativo → consentita (x/100 mantiene segno)
- **EC-03:** Uso della percentuale dopo aver ottenuto un risultato precedente → applicabile al nuovo input dopo clear o dopo digitazione di nuovi numeri

---

## 🧪 TECHNICAL NOTES

- **TN-01:** UI: aggiungere pulsante "%" nel tastierino della Razor Page Calculator
- **TN-02:** JavaScript: estendere state machine per supportare:
  - applicazione % su currentInput
  - applicazione % contestuale su B quando esiste A e operation
  - flag per evitare applicazione ripetuta (anti-%%)
- **TN-03:** Server: estendere CalculatorService per gestire un nuovo codice operazione se necessario (es. `pct`) oppure gestire percentuale solo lato UI trasformando operandi prima del submit

---

## ❓ OPEN QUESTIONS

- **OQ-02:** In caso AF-01.2: preferisci ignorare il click su `%` oppure mostrare errore UI?
- **OQ-03:** Dopo aver mostrato un risultato, la digitazione di un numero deve resettare automaticamente lo stato (come calcolatrice standard) o mantenere il risultato come nuovo A?

---

## 📊 Confidence

**Level:** high  
**Notes:** Le decisioni APPLY hanno risolto le ambiguità principali definendo una semantica contestuale completa per `%` e una gestione chiara per l'edge case `%%`. L'unica area residua riguarda la scelta UX per `%` senza input (ignorare vs messaggio), che non blocca l'implementazione.
