# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-001  
**Name:** SimpleWebCalculator

---

### 🧭 Business Context

**Description:**  
Sistema web che permette agli utenti di eseguire operazioni matematiche di base (addizione, sottrazione, moltiplicazione, divisione) attraverso un'interfaccia web semplice con tastierino numerico integrato.

**Goal:**  
Fornire uno strumento accessibile via browser per calcoli matematici elementari con gestione degli errori comuni (divisione per zero, input non validi, overflow/underflow) e interfaccia simile a una calcolatrice standard.

---

### 👥 Actors

- **ACT-01:** Utente web — utente finale che accede all'applicazione tramite browser per eseguire calcoli

---

### 🔎 Functional View

L'utente accede a una pagina web dove trova un'interfaccia simile a una calcolatrice standard con display e tastierino numerico. Può inserire numeri tramite il tastierino (cifre 0-9, punto decimale), selezionare un'operazione matematica tra quattro disponibili (con addizione pre-selezionata come default), e ottenere il risultato del calcolo. L'elaborazione avviene lato server e il risultato viene visualizzato nel display, arrotondato a due cifre decimali. Il sistema gestisce situazioni di errore comuni come input non validi, campi vuoti, divisione per zero, e valori fuori range.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Calcolo**
  - **Description:** Rappresenta una singola operazione matematica richiesta dall'utente
  - **Key Attributes:**
    - Operando A (numero decimale)
    - Operando B (numero decimale)
    - Tipo operazione (addizione, sottrazione, moltiplicazione, divisione)
    - Risultato (numero decimale, arrotondato a 2 cifre decimali)
  - **Related User Stories:** US-00, US-01, US-02, US-03

- **ENT-02 — Operazione**
  - **Description:** Rappresenta un tipo specifico di operazione matematica disponibile
  - **Key Attributes:**
    - Codice operazione (add, sub, mul, div)
    - Nome visualizzabile
  - **Related User Stories:** US-02
  - **Note:** La logica di calcolo NON risiede nell'entità ma viene applicata da un servizio/componente separato (ISS-07)

---

### 🔗 Relationships

- **REL-01 — Calcolo → Operazione**
  - **Description:** Ogni Calcolo utilizza esattamente un'Operazione per determinare il risultato da A e B

---

### 📜 Constraints

- **CON-01:** I valori A e B devono essere numeri decimali validi nel range ±10^15 (ISS-09)
- **CON-02:** Per l'operazione di divisione, B non può essere zero
- **CON-03:** L'operazione di default è l'addizione; l'utente può cambiare selezione esplicitamente (ISS-06)
- **CON-04:** Tutti i campi obbligatori (A, B, Operazione) devono essere valorizzati prima del calcolo
- **CON-05:** L'input accetta solo cifre numeriche (0-9) e il punto decimale (ISS-03)
- **CON-06:** Il risultato deve essere arrotondato a due cifre decimali (ISS-02)

---

## 🧩 USER STORIES

### US-00 — Visualizzazione iniziale calcolatrice

**Descrizione:**  
Come utente web voglio accedere a una pagina con un'interfaccia di calcolatrice standard (display vuoto e tastierino numerico con cifre 0-9, punto decimale, e operazioni) per iniziare ad eseguire calcoli in modo intuitivo.

**Related Flow:** MF-01

---

### US-01 — Inserimento dati operandi tramite tastierino

**Descrizione:**  
Come utente web voglio poter inserire due numeri (A e B) utilizzando un tastierino numerico integrato (cifre 0-9 e punto decimale) per garantire input validi e migliorare l'esperienza d'uso.

**Related Flow:** MF-02

---

### US-02 — Selezione operazione matematica

**Descrizione:**  
Come utente web voglio poter selezionare una delle quattro operazioni disponibili (addizione pre-selezionata come default, sottrazione, moltiplicazione, divisione) per specificare quale calcolo eseguire sui numeri inseriti.

**Related Flow:** MF-03

---

### US-03 — Esecuzione calcolo e visualizzazione risultato

**Descrizione:**  
Come utente web voglio poter avviare il calcolo tramite un pulsante "Calcola" e visualizzare il risultato nel display (arrotondato a due cifre decimali) per ottenere immediatamente il risultato dell'operazione in formato leggibile.

**Related Flow:** MF-04, MF-05, MF-06

---

### US-04 — Gestione errori di input

**Descrizione:**  
Come utente web voglio ricevere messaggi di errore chiari quando inserisco dati non validi (campi vuoti, valori non numerici, divisione per zero, valori fuori range) per comprendere cosa correggere.

**Related Flow:** AF-01, AF-02, AF-03, AF-04, AF-05

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede alla pagina della calcolatrice e visualizza un'interfaccia con display vuoto, tastierino numerico (0-9, punto decimale), operazioni disponibili (addizione pre-selezionata), e pulsante "Calcola"
- **MF-02:** L'utente inserisce i valori numerici A e B utilizzando il tastierino numerico integrato
- **MF-03:** L'utente seleziona l'operazione desiderata (addizione già selezionata di default, oppure sottrazione, moltiplicazione, divisione)
- **MF-04:** L'utente clicca sul pulsante "Calcola"
- **MF-05:** Il sistema valida gli input (range ±10^15), elabora la richiesta lato server applicando l'operazione selezionata tramite un servizio dedicato (non nel controller)
- **MF-06:** Il risultato viene calcolato, arrotondato a due cifre decimali, e mostrato nel display

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Input non valido (caratteri non consentiti)

- **AF-01.1:** L'utente tenta di inserire caratteri non numerici o speciali (diversi da 0-9 e punto decimale)
- **AF-01.2:** Il sistema blocca preventivamente l'input tramite il tastierino (che consente solo cifre e punto decimale)
- **AF-01.3:** Se l'input avviene comunque (es. copia/incolla), il sistema mostra un messaggio di errore indicando che sono consentiti solo numeri e punto decimale

---

### AF-02 — Campi vuoti

- **AF-02.1:** L'utente clicca "Calcola" senza aver compilato tutti i campi obbligatori (A o B vuoti)
- **AF-02.2:** Il sistema mostra un messaggio di errore indicando i campi mancanti
- **AF-02.3:** L'utente completa i campi tramite il tastierino e riprova

---

### AF-03 — Divisione per zero

- **AF-03.1:** L'utente seleziona "Divisione" e inserisce 0 come valore B
- **AF-03.2:** L'utente clicca "Calcola"
- **AF-03.3:** Il sistema intercetta la condizione e mostra un messaggio di errore specifico: "Impossibile dividere per zero"
- **AF-03.4:** L'utente modifica il valore B e riprova

---

### AF-04 — Overflow/Underflow numerico

- **AF-04.1:** L'utente inserisce valori estremamente grandi (>10^15) o estremamente piccoli (<-10^15), oppure il risultato del calcolo genera overflow/underflow
- **AF-04.2:** Il sistema valida i valori in input e il risultato
- **AF-04.3:** Se un valore è fuori range, il sistema mostra un messaggio di errore: "Valore fuori dal range consentito (±10^15)"
- **AF-04.4:** L'utente inserisce valori validi e riprova

---

### AF-05 — Risultato con molte cifre decimali

- **AF-05.1:** L'utente esegue un'operazione che produce un risultato con molte cifre decimali (es. 10 / 3)
- **AF-05.2:** Il sistema calcola il risultato completo
- **AF-05.3:** Il sistema arrotonda automaticamente il risultato a due cifre decimali (es. 3.33)
- **AF-05.4:** Il risultato arrotondato viene visualizzato nel display

---

## 📜 BUSINESS RULES

- **BR-01:** Addizione: il risultato è la somma di A + B
- **BR-02:** Sottrazione: il risultato è la differenza A - B
- **BR-03:** Moltiplicazione: il risultato è il prodotto A * B
- **BR-04:** Divisione: se B ≠ 0, il risultato è il quoziente A / B; se B = 0, viene generato un errore
- **BR-05:** Tutti i valori numerici sono trattati come numeri decimali (double precision)
- **BR-06:** Il calcolo deve essere eseguito esclusivamente lato server da un servizio/componente dedicato, NON dal controller (ISS-07)
- **BR-07:** Il risultato deve essere arrotondato a due cifre decimali prima della visualizzazione (ISS-02)
- **BR-08:** I valori A e B devono essere nel range ±10^15; valori fuori range generano errore (ISS-09)
- **BR-09:** L'operazione di default all'apertura della pagina è l'addizione (ISS-06)
- **BR-10:** Il separatore decimale è il punto (.) indipendentemente dalla localizzazione del browser (ISS-09)

---

## ⚠️ EDGE CASES

- **EC-01:** Inserimento di valori estremamente grandi che potrebbero causare overflow → gestito da AF-04 e BR-08
- **EC-02:** Inserimento di valori estremamente piccoli che potrebbero causare underflow → gestito da AF-04 e BR-08
- **EC-03:** Risultato di una divisione che produce un numero con molte cifre decimali → gestito da AF-05 e BR-07
- **EC-04:** Uso di caratteri speciali o spazi nei campi numerici → gestito da AF-01 e CON-05 (tastierino consente solo input validi)
- **EC-05:** Tentativo di submit multipli consecutivi (doppio click su "Calcola") → non gestito in questa versione (ISS-04 in SKIP)

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Architettura richiesta: ASP.NET Core Razor Pages (priorità per il workspace corrente)
- **TN-02:** Rendering: server-side (no framework frontend complessi)
- **TN-03:** UI: layout con Bootstrap per semplicità e responsività
- **TN-04:** Model suggerito con proprietà: A (double), B (double), Operation (string), Result (double)
- **TN-05:** PageModel responsabile di: ricezione POST, validazione input base, delegazione calcolo al servizio
- **TN-06:** View responsabile di: form con tastierino numerico, visualizzazione display, pulsanti operazioni
- **TN-07:** Servizio di calcolo separato: CalculatorService responsabile di eseguire le operazioni matematiche (BR-06)
- **TN-08:** Tastierino numerico: implementabile con pulsanti Bootstrap che aggiungono valori al campo input

---

## ❓ OPEN QUESTIONS

- **OQ-01:** È necessario mantenere uno storico dei calcoli eseguiti dall'utente?
- **OQ-02:** Serve gestione della sessione utente o ogni calcolo è indipendente?
- **OQ-05:** Serve logging delle operazioni per audit o debugging?
- **OQ-06:** È necessario implementare test automatici (unit/integration)?
- **OQ-07:** Il tastierino numerico deve essere visivamente simile a una calcolatrice fisica (layout griglia 3x4)?
- **OQ-08:** Serve un pulsante "Clear" per resettare il form e ricominciare un nuovo calcolo?

**Resolved in v2:**
- ~~OQ-03: Limiti numerici~~ → risolto: range ±10^15 (BR-08)
- ~~OQ-04: Localizzazione decimali~~ → risolto: punto come separatore standard (BR-10)

---

## 📊 Confidence

**Level:** high  
**Notes:** La specifica è stata migliorata applicando le issue approvate. L'interfaccia con tastierino numerico e le validazioni per range e precisione decimale rendono l'analisi più completa. La separazione della logica di calcolo dal controller migliora la qualità architetturale. Le open questions residue riguardano principalmente estensioni future non critiche per il MVP.

---

## 📝 Change Log v1 → v2

**Applied Issues:**
- ISS-01: Aggiunto AF-04 per overflow/underflow con BR-08 (range ±10^15)
- ISS-02: Aggiunto AF-05 e BR-07 per arrotondamento a 2 cifre decimali
- ISS-03: Aggiunto tastierino numerico in US-01, MF-01, MF-02, AF-01, CON-05
- ISS-06: Aggiunto operazione default (addizione) in CON-03, BR-09, MF-03
- ISS-07: Chiarito ENT-02 (no logica calcolo) e aggiunto BR-06 (servizio dedicato)
- ISS-08: Aggiunta US-00 per stato iniziale form con tastierino
- ISS-09: Aggiunto BR-08 (range validation), BR-10 (separatore decimale punto)

**Skipped Issues:**
- ISS-04: Submit multipli (nice to have, non critico per MVP)
- ISS-05: Coperto da ISS-02
