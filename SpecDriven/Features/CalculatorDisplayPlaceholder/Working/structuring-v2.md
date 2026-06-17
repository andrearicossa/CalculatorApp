# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-009  
**Name:** CalculatorDisplayPlaceholder

---

### 🧭 Business Context

**Description:**  
Miglioramento UX della calcolatrice: nel display, quando l'utente non ha ancora inserito alcun valore e non è presente alcun risultato, al posto di mostrare "0" deve essere mostrato un testo guida "Digit Here...".

**Goal:**
- Migliorare la chiarezza iniziale dell'interfaccia
- Indicare all'utente dove iniziare a digitare
- Mantenere comportamento coerente con le operazioni: il placeholder scompare quando l'utente inserisce numeri o quando viene mostrato un risultato
- Ripristinare il placeholder quando l'utente preme Clear

---

### 👥 Actors

- **ACT-01:** Utente web — apre la calcolatrice e inizia a digitare

---

### 🔎 Functional View

All'apertura della pagina Calculator, se non è presente alcun input e non è presente un risultato, il display mostra la scritta guida "Digit Here...". Al primo inserimento di un numero (anche 0) o del punto decimale, il display mostra il valore inserito e il placeholder scompare. Se l'utente esegue un calcolo, il display mostra sempre il risultato, anche se il risultato è 0.00. Se l'utente preme Clear, la calcolatrice torna allo stato iniziale e il display torna a mostrare "Digit Here...".

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Display**
  - **Description:** Area di visualizzazione della calcolatrice
  - **Key Attributes:**
    - Stato iniziale: placeholder "Digit Here..."
    - Stato input: numero in corso
    - Stato risultato: valore calcolato (anche 0.00)
  - **Related User Stories:** US-01

---

### 📜 Constraints

- **CON-01:** Il placeholder deve essere visibile solo quando non c'è input e non c'è risultato
- **CON-02:** Il display continua a essere read-only
- **CON-03:** Il display deve tornare a placeholder dopo Clear
- **CON-04:** Il placeholder non deve mai sostituire un risultato, anche se il risultato è 0.00

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
- **MF-03:** L'utente preme un tasto numerico o il punto decimale
- **MF-04:** Il display mostra i numeri inseriti (il placeholder scompare)

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Clear
- **AF-01.1:** L'utente preme Clear
- **AF-01.2:** Il sistema resetta lo stato e mostra di nuovo "Digit Here..."

---

### AF-02 — Risultato
- **AF-02.1:** L'utente esegue un calcolo e ottiene un risultato
- **AF-02.2:** Il display mostra il risultato (anche se 0.00)

---

## 📜 BUSINESS RULES

- **BR-01:** Stato iniziale: display mostra "Digit Here..."
- **BR-02:** Alla prima digitazione, il placeholder viene sostituito dai numeri inseriti
- **BR-03:** Clear ripristina lo stato iniziale e quindi il placeholder
- **BR-04:** Se esiste un risultato, il display mostra sempre il risultato (anche se 0.00)

---

## ⚠️ EDGE CASES

- **EC-01:** Apertura History senza aver digitato → display resta con placeholder
- **EC-02:** Utente digita 0 come primo numero → display mostra "0" (non placeholder)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Il placeholder è un testo guida e non deve essere interpretato come valore numerico.
- **TN-02:** La logica di visualizzazione deve basarsi sullo stato (input presente / risultato presente), non sul valore numerico.

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Il placeholder deve essere localizzabile (IT/EN) o fisso "Digit Here..."? (default: fisso)

---

## 📊 Confidence

**Level:** high  
**Notes:** Requisito chiaro. La parte critica è garantire che un risultato pari a 0.00 venga comunque mostrato come risultato e non sostituito dal placeholder.
