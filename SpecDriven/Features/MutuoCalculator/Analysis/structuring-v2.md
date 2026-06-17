# ANALISI FUNZIONALE STRUTTURATA v2

### 📌 Feature

**ID:** FEAT-010  
**Name:** MutuoCalculator  
**Versione Structuring:** v2  
**Decisioni applicate:** ISS-01, ISS-02, ISS-03, ISS-04

---

### 🧭 Business Context

**Description:**  
Nuova pagina web navigabile dalla navbar che consente la simulazione del piano di ammortamento di un mutuo. Deve integrarsi nell'applicazione esistente (SimpleWebCalculator) come nuova voce di menu, seguendo la stessa architettura Razor Pages e lo stesso layout visivo del Calculator.

**Goal:**
- Permettere all'utente di inserire i dati principali del mutuo e ottenere il piano di ammortamento completo
- Integrare la pagina nella navbar esistente come nuova voce "Mutuo"
- Usare la stessa architettura del Calculator: Razor Page + servizio dedicato + layout condiviso

---

### 👥 Actors

- **ACT-01:** Privati che desiderano simulare un mutuo
- **ACT-02:** Consulenti finanziari
- **ACT-03:** Operatori del settore
- **ACT-04:** Utenti a scopo educativo

---

### 🔎 Functional View

L'utente accede alla pagina "Mutuo" tramite il menu di navigazione. Inserisce i dati del mutuo (importo, tasso di interesse strettamente positivo, durata in anni, frequenza rate scelta da combo box, data di inizio) e avvia il calcolo. Il sistema valida i dati — rifiutando tasso = 0 con un messaggio informativo — calcola rata e piano di ammortamento, mostra il riepilogo (rata, totali) e la tabella del piano con paginazione. In caso di dati mancanti o non validi il sistema mostra messaggi di errore senza crashare.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Mutuo Input**
  - **Description:** Dati inseriti dall'utente per la simulazione
  - **Key Attributes:**
    - ImportoMutuo (decimal) — valore positivo > 0
    - TassoInteresse (decimal, percentuale annua) — valore strettamente positivo > 0; tasso = 0 non è ammesso
    - DurataAnni (int) — durata espressa in anni interi positivi; il numero totale di rate si ottiene moltiplicando DurataAnni × frequenza annua
    - FrequenzaRate (enum/string) — selezionabile tramite combo box; valori ammessi: mensile, trimestrale, semestrale, annuale
    - DataInizio (DateOnly)
  - **Related User Stories:** US-01
  - **Applied Issues:** ISS-01 (FrequenzaRate come combo), ISS-03 (TassoInteresse > 0 strettamente), ISS-04 (DurataAnni in anni)

- **ENT-02 — Piano di Ammortamento**
  - **Description:** Risultato del calcolo: riepilogo e lista rate
  - **Key Attributes:**
    - Rata (decimal)
    - TotaleInteressi (decimal)
    - TotaleCapitale (decimal)
    - TotaleComplessivo (decimal)
    - Righe: lista RataDettaglio
  - **Related User Stories:** US-02, US-03
  - **Applied Issues:** ISS-02 (tabella con paginazione)

- **ENT-03 — Rata Dettaglio**
  - **Description:** Singola riga del piano
  - **Key Attributes:**
    - NumeroRata (int)
    - Data (DateOnly)
    - ImportoRata (decimal)
    - QuotaCapitale (decimal)
    - QuotaInteressi (decimal)
    - CapitaleResiduo (decimal)
  - **Related User Stories:** US-03

---

### 🔗 Relationships

- **REL-01 — Mutuo Input → Piano di Ammortamento**
  - **Description:** Il servizio riceve l'input e produce il piano

---

### 📜 Constraints

- **CON-01:** Architettura Razor Pages + IMutuoService (stessa struttura di Calculator)
- **CON-02:** Layout condiviso Pages/Shared/_Layout.cshtml (già esistente)
- **CON-03:** Voce "Mutuo" aggiunta alla navbar in entrambi i layout (Views/Shared e Pages/Shared)
- **CON-04:** Calcolo server-side, rendering server-side
- **CON-05:** UI Bootstrap coerente con il resto dell'applicazione

---

## 🧩 USER STORIES

### US-01 — Inserimento dati mutuo

**Descrizione:**
Come utente voglio inserire importo, tasso, durata in anni, frequenza (tramite combo box) e data di inizio per configurare la simulazione del mio mutuo.

**Related Flow:** MF-01, MF-02  
**Applied Issues:** ISS-01, ISS-03, ISS-04

---

### US-02 — Visualizzazione riepilogo

**Descrizione:**
Come utente voglio vedere un riepilogo con importo rata, totale interessi, capitale restituito e totale complessivo pagato.

**Related Flow:** MF-04

---

### US-03 — Visualizzazione piano di ammortamento

**Descrizione:**
Come utente voglio vedere la tabella del piano di ammortamento con numero rata, data, importo, quota capitale, quota interessi e capitale residuo, navigabile tramite paginazione.

**Related Flow:** MF-05  
**Applied Issues:** ISS-02

---

### US-04 — Navigazione dalla navbar

**Descrizione:**
Come utente voglio raggiungere la pagina Mutuo cliccando sulla voce "Mutuo" nel menu di navigazione.

**Related Flow:** MF-01

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente clicca su "Mutuo" nella navbar
- **MF-02:** Il sistema mostra il form di inserimento dati con combo box per la frequenza
- **MF-03:** L'utente compila i campi e avvia il calcolo
- **MF-04:** Il sistema valida i dati, calcola rata e piano, mostra riepilogo
- **MF-05:** Il sistema mostra la tabella paginata con le rate

---

## 🔁 ALTERNATIVE FLOWS

### AF-01 — Dati mancanti o non validi
- **AF-01.1:** L'utente preme Calcola con campi mancanti o valori non validi
- **AF-01.2:** Il sistema mostra messaggi di errore descrittivi senza crashare

### AF-02 — Tasso uguale a zero
- **AF-02.1:** L'utente inserisce tasso = 0
- **AF-02.2:** Il sistema mostra un messaggio informativo: il tasso deve essere strettamente positivo; il calcolo non viene eseguito

---

## 📜 BUSINESS RULES

- **BR-01:** La rata si calcola con la formula di ammortamento alla francese: `R = C * (i / (1 - (1+i)^-n))` dove C=capitale, i=tasso periodico (TassoInteresse/100/frequenza_annua), n=DurataAnni × frequenza_annua
- **BR-02:** Le frequenze supportate, selezionabili tramite combo box: mensile (12/anno), trimestrale (4/anno), semestrale (2/anno), annuale (1/anno)
- **BR-03:** Importo mutuo: valore positivo > 0
- **BR-04:** Tasso interesse: valore strettamente positivo > 0 e < 100 (percentuale annua); tasso = 0 non è supportato e genera errore di validazione
- **BR-05:** DurataAnni: intero positivo > 0, espresso in anni; il numero totale di rate = DurataAnni × frequenza_annua
- **BR-06:** Data di inizio: data valida
- **BR-07:** La prima rata è alla prima scadenza periodica dalla data di inizio
- **BR-08:** Il capitale residuo dell'ultima rata deve essere 0 (o molto prossimo a 0, gestito con arrotondamento)
- **BR-09:** I valori monetari sono arrotondati a 2 cifre decimali

---

## ⚠️ EDGE CASES

- **EC-01:** Tasso = 0 → errore di validazione; il calcolo non viene eseguito; viene mostrato un messaggio informativo *(ISS-03 applicata)*
- **EC-02:** Durata molto lunga (es. 360 rate mensili) → tabella paginata per garantire consultabilità *(ISS-02 applicata)*
- **EC-03:** Importo molto piccolo → arrotondamento può creare lievi discrepanze nel totale

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Seguire lo stesso pattern del Calculator:
  - `Pages/Mutuo.cshtml` (Razor Page)
  - `Pages/Mutuo.cshtml.cs` (MutuoModel : PageModel)
  - `Services/IMutuoService.cs` + `Services/MutuoService.cs`
  - Registrazione del servizio in Program.cs
- **TN-02:** Aggiungere voce "Mutuo" a entrambi i layout: `Views/Shared/_Layout.cshtml` e `Pages/Shared/_Layout.cshtml`
- **TN-03:** FrequenzaRate renderizzato come `<select>` con le 4 opzioni fisse *(ISS-01)*
- **TN-04:** Paginazione lato server o lato client sulla tabella del piano *(ISS-02)*
- **TN-05:** Validazione BR-04 (tasso > 0) sia tramite data annotation che lato server nel servizio *(ISS-03)*
- **TN-06:** n = DurataAnni × frequenza_annua usato nella formula BR-01 *(ISS-04)*

---

## ❓ OPEN QUESTIONS

*(Tutte le open questions risolte con le istruzioni applicate)*

- ~~**OQ-01:** La frequenza deve includere anche "quindicinale"?~~ → Risolto con ISS-01: set fisso (mensile, trimestrale, semestrale, annuale), selezionabile tramite combo.
- ~~**OQ-02:** La tabella deve essere paginata o scrollabile?~~ → Risolto con ISS-02: paginazione.

---

## 📊 Confidence

**Level:** high  
**Notes:** Tutte le issue approvate sono state integrate. Il design è allineato all'architettura esistente. Nessuna open question residua.
