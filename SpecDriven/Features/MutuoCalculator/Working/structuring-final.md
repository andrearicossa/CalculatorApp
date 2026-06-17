# ANALISI FUNZIONALE STRUTTURATA — FINAL

### 📌 Feature

**ID:** FEAT-010  
**Name:** MutuoCalculator  
**Versione:** structuring-final  
**Derivata da:** structuring-v2.md

---

### 🧭 Business Context

**Description:**  
Nuova pagina web navigabile dalla navbar che consente la simulazione del piano di ammortamento di un mutuo. Si integra nell'applicazione esistente (SimpleWebCalculator) come nuova voce di menu "Mutuo", seguendo la stessa architettura Razor Pages e lo stesso layout visivo del Calculator.

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

L'utente accede alla pagina "Mutuo" tramite il menu di navigazione. Inserisce importo, tasso di interesse (strettamente > 0), durata in anni interi, frequenza rate (combo box: mensile/trimestrale/semestrale/annuale) e data di inizio, poi avvia il calcolo. Il sistema valida i dati — rifiutando tasso = 0 con messaggio informativo — calcola rata e piano (ammortamento alla francese), mostra riepilogo con i totali e la tabella del piano con paginazione. In caso di dati non validi viene mostrato un messaggio di errore senza crashare.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Mutuo Input**
  - ImportoMutuo (decimal) — > 0
  - TassoInteresse (decimal, % annua) — strettamente > 0; tasso = 0 non ammesso
  - DurataAnni (int) — anni interi > 0; n. rate = DurataAnni × frequenza_annua
  - FrequenzaRate (string) — selezionabile tramite combo: mensile, trimestrale, semestrale, annuale
  - DataInizio (DateOnly)

- **ENT-02 — Piano di Ammortamento**
  - Rata (decimal)
  - TotaleInteressi (decimal)
  - TotaleCapitale (decimal)
  - TotaleComplessivo (decimal)
  - Righe: lista di ENT-03

- **ENT-03 — Rata Dettaglio**
  - NumeroRata (int)
  - Data (DateOnly)
  - ImportoRata (decimal)
  - QuotaCapitale (decimal)
  - QuotaInteressi (decimal)
  - CapitaleResiduo (decimal)

### 🔗 Relationships

- **REL-01:** Mutuo Input → Piano di Ammortamento (il servizio riceve l'input e produce il piano)

### 📜 Constraints

- **CON-01:** Architettura Razor Pages + IMutuoService
- **CON-02:** Layout condiviso Pages/Shared/_Layout.cshtml
- **CON-03:** Voce "Mutuo" nella navbar (Views/Shared e Pages/Shared)
- **CON-04:** Calcolo e rendering server-side
- **CON-05:** UI Bootstrap coerente con il resto dell'applicazione

---

## 🧩 USER STORIES

| ID    | Descrizione sintetica                                                              | Flow         |
|-------|------------------------------------------------------------------------------------|--------------|
| US-01 | Inserire importo, tasso, durata (anni), frequenza (combo), data inizio            | MF-01, MF-02 |
| US-02 | Visualizzare riepilogo: rata, totale interessi, capitale, totale complessivo       | MF-04        |
| US-03 | Visualizzare tabella paginata del piano con tutte le colonne                       | MF-05        |
| US-04 | Raggiungere la pagina Mutuo dalla navbar                                           | MF-01        |

---

## 🔄 MAIN FLOW

1. **MF-01** — L'utente clicca su "Mutuo" nella navbar
2. **MF-02** — Il sistema mostra il form con combo box per la frequenza
3. **MF-03** — L'utente compila i campi e avvia il calcolo
4. **MF-04** — Il sistema valida, calcola, mostra riepilogo
5. **MF-05** — Il sistema mostra la tabella paginata del piano

## 🔁 ALTERNATIVE FLOWS

- **AF-01** — Dati mancanti/non validi: messaggio di errore descrittivo, nessun crash
- **AF-02** — Tasso = 0: messaggio informativo, calcolo non eseguito

---

## 📜 BUSINESS RULES

| ID    | Regola                                                                                                                             |
|-------|------------------------------------------------------------------------------------------------------------------------------------|
| BR-01 | Ammortamento alla francese: `R = C × (i / (1 − (1+i)^−n))` con i = TassoInteresse/100/freq_annua, n = DurataAnni × freq_annua    |
| BR-02 | Frequenze supportate (combo): mensile (×12), trimestrale (×4), semestrale (×2), annuale (×1)                                       |
| BR-03 | ImportoMutuo > 0                                                                                                                   |
| BR-04 | TassoInteresse strettamente > 0 e < 100; tasso = 0 → errore di validazione                                                        |
| BR-05 | DurataAnni intero > 0 (anni); n. rate totali = DurataAnni × freq_annua                                                             |
| BR-06 | DataInizio: data valida                                                                                                            |
| BR-07 | Prima rata alla prima scadenza periodica dalla DataInizio                                                                          |
| BR-08 | CapitaleResiduo ultima rata = 0 (gestito con arrotondamento)                                                                       |
| BR-09 | Valori monetari arrotondati a 2 decimali                                                                                           |

---

## ⚠️ EDGE CASES

| ID    | Caso                                   | Comportamento                                         |
|-------|----------------------------------------|-------------------------------------------------------|
| EC-01 | Tasso = 0                              | Errore di validazione; messaggio informativo; stop    |
| EC-02 | Durata molto lunga (es. 360 rate)      | Tabella paginata; nessun problema di consultabilità   |
| EC-03 | Importo molto piccolo                  | Arrotondamento può creare lievi discrepanze sul totale|

---

## 🧪 TECHNICAL NOTES

- **TN-01:** Pattern: `Pages/Mutuo.cshtml`, `Pages/Mutuo.cshtml.cs` (MutuoModel : PageModel), `Services/IMutuoService.cs`, `Services/MutuoService.cs`, registrazione in Program.cs
- **TN-02:** Voce "Mutuo" in entrambi i layout: `Views/Shared/_Layout.cshtml` e `Pages/Shared/_Layout.cshtml`
- **TN-03:** FrequenzaRate renderizzato come `<select>` con 4 opzioni fisse
- **TN-04:** Paginazione sulla tabella del piano (lato server o lato client)
- **TN-05:** Validazione BR-04 (TassoInteresse > 0) tramite data annotation e lato server nel servizio
- **TN-06:** n = DurataAnni × freq_annua usato direttamente nella formula BR-01

---

## 📊 Confidence

**Level:** high  
**Notes:** Nessuna open question residua. Tutte le issue approvate integrate. Pronto per OPSX.
