# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-009  
**Name:** CalculatorDisplayPlaceholder

---

### 🧭 Business Context

**Description:**  
Miglioramento UX della calcolatrice: nel display, quando l'utente non ha ancora inserito alcun valore, al posto di mostrare "0" deve essere mostrato un testo guida (placeholder) "Digit Here...".

**Goal:**
- Migliorare la chiarezza iniziale dell'interfaccia
- Indicare all'utente dove iniziare a digitare
- Mantenere comportamento coerente con le operazioni (il placeholder scompare quando l'utente inserisce numeri o quando viene mostrato un risultato)

---

### 👥 Actors

- **ACT-01:** Utente web — apre la calcolatrice e inizia a digitare

---

### 🔎 Functional View

All'apertura della pagina Calculator, se non è presente alcun input o risultato, il display non deve mostrare "0" ma un testo guida "Digit Here...". Quando l'utente preme un tasto numerico o punto decimale, il display deve iniziare a mostrare il numero inserito (il placeholder scompare). Se l'utente preme Clear, tornando allo stato iniziale, deve ricomparire il placeholder. Se viene mostrato un risultato di un calcolo, il display mostra il risultato (non il placeholder).

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Display**
  - **Description:** area di visualizzazione della calcolatrice
  - **Key Attributes:**
    - Stato iniziale (placeholder)
    - Stato input (numero in corso)
    - Stato risultato (valore calcolato)
  - **Related User Stories:** US-01

---

### 📜 Constraints

- **CON-01:** Il placeholder deve essere visibile solo quando non c'è input e non c'è risultato
- **CON-02:** Il display continua a essere read-only
- **CON-03:** Il display deve tornare a placeholder dopo Clear

---

## 🧩 USER STORIES

### US-01 — Placeholder nel display

**Descrizione:**
Come utente web voglio vedere nel display la scritta "Digit Here..." quando la calcolatrice è in stato iniziale, così da capire immediatamente dove inserire i numeri.

**Related Flow:** MF-01

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente apre la pagina Calculator
- **MF-02:** Il sistema mostra nel display "Digit Here..." (stato iniziale)
- **MF-03:** L'utente preme un tasto numerico
- **MF-04:** Il display mostra i numeri inseriti (il placeholder scompare)

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Clear
- **AF-01.1:** L'utente preme Clear
- **AF-01.2:** Il sistema resetta lo stato e mostra di nuovo "Digit Here..."

---

### AF-02 — Risultato
- **AF-02.1:** L'utente esegue un calcolo e ottiene un risultato
- **AF-02.2:** Il display mostra il risultato (non il placeholder)

---

## 📜 BUSINESS RULES

- **BR-01:** Stato iniziale: display mostra "Digit Here..."
- **BR-02:** Alla prima digitazione, il placeholder viene sostituito dai numeri inseriti
- **BR-03:** Clear ripristina lo stato iniziale e quindi il placeholder
- **BR-04:** Se esiste un risultato, il display mostra il risultato

---

## ⚠️ EDGE CASES

- **EC-01:** Apertura History modal senza aver digitato → display resta con placeholder
- **EC-02:** Utente digita 0 come primo numero → display mostra "0" (non placeholder)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Il placeholder può essere ottenuto usando l'attributo HTML `placeholder` su input read-only, oppure gestendo il value iniziale e uno stile "text-muted".
- **TN-02:** Serve coerenza tra rendering server-side (Model.Result) e stato client-side (currentInput).

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Il placeholder deve essere localizzabile (IT/EN) o fisso "Digit Here..."?

---

## 📊 Confidence

**Level:** high  
**Notes:** Requisito chiaro e non ambiguo. L'impatto è limitato al display e alla logica Clear/inizializzazione.
