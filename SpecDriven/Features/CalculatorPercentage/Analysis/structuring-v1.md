# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-004  
**Name:** CalculatorPercentage

---

### 🧭 Business Context

**Description:**  
Estensione della web calculator esistente (SimpleWebCalculator) per includere la funzionalità di calcolo della percentuale, mantenendo l'interfaccia a tastierino e l'elaborazione server-side.

**Goal:**
- Consentire all'utente di calcolare percentuali in modo semplice dalla stessa pagina Calculator
- Mantenere coerenza UX/UI col layout già presente (Bootstrap, tastierino, display)
- Definire chiaramente la semantica dell'operazione percentuale per evitare ambiguità

---

### 👥 Actors

- **ACT-01:** Utente web — utente finale che utilizza la calcolatrice dal browser

---

### 🔎 Functional View

L'utente utilizza la calcolatrice web per eseguire operazioni. Oltre alle operazioni base (+, −, ×, ÷), desidera utilizzare una funzione percentuale. La funzione percentuale deve essere accessibile dall'interfaccia (ad esempio come pulsante "%") e deve produrre un risultato coerente con le aspettative d'uso di una calcolatrice standard, senza introdurre complessità non necessarie.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calcolo**
  - **Description:** Rappresenta una singola operazione matematica richiesta dall'utente
  - **Key Attributes:**
    - Operando A (double)
    - Operando B (double)
    - Operazione (string)
    - Risultato (double)
  - **Related User Stories:** US-01, US-02, US-03

- **ENT-02 — Operazione Percentuale**
  - **Description:** Rappresenta l'operazione di calcolo percentuale disponibile nella calcolatrice
  - **Key Attributes:**
    - Simbolo: %
    - Semantica operazione (da definire)
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — Calcolo → Operazione Percentuale**
  - **Description:** Un calcolo può utilizzare l'operazione percentuale come operazione selezionata oppure come variante di un'operazione base

---

### 📜 Constraints

- **CON-01:** La funzione percentuale deve essere comprensibile nel contesto d'uso di una calcolatrice standard
- **CON-02:** La percentuale non deve introdurre nuove dipendenze frontend complesse
- **CON-03:** Il calcolo deve rimanere server-side, delegato a un servizio di calcolo
- **CON-04:** Devono rimanere valide le regole di validazione esistenti (range ±10^15, divisione per zero, arrotondamento)

---

## 🧩 USER STORIES

### US-01 — Visualizzazione pulsante percentuale

**Descrizione:**
Come utente web voglio vedere un pulsante "%" nella UI della calcolatrice per poter accedere alla funzione percentuale.

**Related Flow:** MF-01

---

### US-02 — Calcolo percentuale su un numero

**Descrizione:**
Come utente web voglio poter calcolare la percentuale di un numero (es. 20% = 0.2 oppure 20% di 50) per ottenere rapidamente valori percentuali.

**Related Flow:** MF-02, MF-03

---

### US-03 — Calcolo percentuale in combinazione con operazioni base

**Descrizione:**
Come utente web voglio poter usare la percentuale insieme a +, −, ×, ÷ (es. 50 + 10% = 55) per replicare il comportamento di una calcolatrice standard.

**Related Flow:** MF-04

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede alla pagina Calculator e visualizza anche il pulsante "%"
- **MF-02:** L'utente inserisce un numero e clicca "%" per ottenere la sua rappresentazione percentuale
- **MF-03:** Il sistema calcola e visualizza il valore percentuale corrispondente
- **MF-04:** L'utente utilizza "%" in combinazione con un'operazione base per ottenere un risultato coerente

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Percentuale con input mancante
- **AF-01.1:** L'utente clicca "%" senza aver inserito alcun numero
- **AF-01.2:** Il sistema mostra un messaggio di errore o ignora l'azione

---

### AF-02 — Percentuale con valori fuori range
- **AF-02.1:** L'utente usa valori che portano il risultato fuori dal range ±10^15
- **AF-02.2:** Il sistema mostra un messaggio di errore coerente con la validazione esistente

---

## 📜 BUSINESS RULES

- **BR-01:** La semantica dell'operazione percentuale deve essere definita esplicitamente (evitare ambiguità) 
- **BR-02:** La UI deve includere un pulsante "%" integrato nel tastierino
- **BR-03:** Il calcolo percentuale deve essere eseguito server-side tramite il servizio di calcolo
- **BR-04:** Il risultato percentuale deve rispettare arrotondamento a 2 cifre decimali (come le altre operazioni)
- **BR-05:** Devono essere rispettate le validazioni esistenti (range, NaN/Infinity)

---

## ⚠️ EDGE CASES

- **EC-01:** Pressione ripetuta del pulsante "%" (es. 20%%)
- **EC-02:** Percentuale su numero negativo
- **EC-03:** Uso della percentuale dopo aver ottenuto un risultato precedente

---

## 🧪 TECHNICAL NOTES

- **TN-01:** La logica deve risiedere nel `CalculatorService`, non nel PageModel
- **TN-02:** La UI è una Razor Page con tastierino gestito da JavaScript; il pulsante "%" deve integrarsi nello stato esistente della calcolatrice

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Qual è la semantica desiderata per "%"?
  - Opzione A: "x%" → x / 100 (percentuale come trasformazione del numero corrente)
  - Opzione B: "A op B%" (es. 50 + 10%) → B% di A applicato all'operazione (comportamento calcolatrici)
  - Opzione C: entrambe, a seconda del contesto

- **OQ-02:** In UI, il tasto "%" deve agire immediatamente sul display (client-side) o deve sempre eseguire submit al server?

- **OQ-03:** Come deve comportarsi "%" durante l'inserimento del primo operando vs secondo operando?

---

## 📊 Confidence

**Level:** medium  
**Notes:** La richiesta è chiara nell'intento (aggiungere percentuale) ma la semantica dell'operazione percentuale è potenzialmente ambigua. È necessario definire i casi d'uso (trasformazione semplice vs percentuale relativa ad A) per evitare implementazioni non desiderate.
