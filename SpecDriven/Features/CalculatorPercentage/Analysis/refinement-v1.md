# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-004

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** ambiguity  
  **Description:** La semantica dell'operazione percentuale (%) non è definita in modo univoco. In particolare, non è chiarito se "%" debba:
  - trasformare il numero corrente in x/100 (es. 20% → 0.2)
  - calcolare una percentuale relativa all'operando A in combinazione con un'operazione (es. 50 + 10% → 55)
  - supportare entrambi i comportamenti in base al contesto.
  Senza una decisione esplicita, l'implementazione rischia di non rispettare le aspettative utente.
  
  **Impacted User Stories:** US-02, US-03  
  **Impacted Elements:** MF-02, MF-04, BR-01, OQ-01  
  **Impacted Entities:** ENT-02, ENT-01  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Non è definito il comportamento del tasto "%" nei diversi stati della UI (inserimento A, scelta operazione, inserimento B, risultato mostrato). Serve specificare come la percentuale interagisce con lo stato gestito dal JavaScript del tastierino.
  
  **Impacted User Stories:** US-01, US-02, US-03  
  **Impacted Elements:** MF-01, MF-02, MF-04, TN-02, OQ-03  
  **Impacted Entities:** ENT-01, ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-03  
  **Type:** missing_info  
  **Description:** Non è definito se il calcolo percentuale debba essere eseguito immediatamente client-side (aggiornando il display) oppure tramite submit al server. La scelta impatta UX e coerenza con il vincolo "calcolo server-side".
  
  **Impacted User Stories:** US-02, US-03  
  **Impacted Elements:** BR-03, TN-02, OQ-02  
  **Impacted Entities:** ENT-03  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-04  
  **Type:** missing_info  
  **Description:** Manca una business rule esplicita su come gestire la percentuale con le diverse operazioni base:
  - per addizione/sottrazione spesso: A ± (A * B/100)
  - per moltiplicazione/divisione spesso: A * (B/100) oppure A / (B/100)
  Senza definizione, US-03 è incompleta.
  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** MF-04, BR-01  
  **Impacted Entities:** ENT-01, ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-05  
  **Type:** missing_info  
  **Description:** Non è definita la gestione dell'edge case EC-01 (pressione ripetuta del tasto "%"). Serve definire se applicare percentuale iterativamente o bloccare l'operazione.
  
  **Impacted User Stories:** US-02  
  **Impacted Elements:** EC-01  
  **Impacted Entities:** ENT-03  
  **Congruence Level:** MEDIUM  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Definire in modo esplicito la semantica di "%" scegliendo una delle opzioni:
  - A) sempre x/100 sul numero corrente
  - B) comportamento "calcolatrice" contestuale (A op B%)
  - C) supporto di entrambi: se esiste A+operazione selezionata, calcolare B% di A; altrimenti x/100.
  
  **Related Issue:** ISS-01  
  **Target Elements:** BR-01, OQ-01, MF-02, MF-04  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-02  
  **Description:** Documentare lo state machine della UI per l'uso del tasto "%" (stato inserimento A, stato inserimento B, stato risultato), e collegare tale state machine alle user story.
  
  **Related Issue:** ISS-02  
  **Target Elements:** MF-01..MF-04, TN-02  
  **Target Entities:** ENT-03  

---

- **ID:** IMP-03  
  **Description:** Decidere se "%" deve aggiornare immediatamente il display (client-side) ma delegare comunque il calcolo finale al server (coerente con vincolo server-side), oppure se deve fare submit al server come operazione a sé.
  
  **Related Issue:** ISS-03  
  **Target Elements:** BR-03, TN-02  
  **Target Entities:** ENT-03  

---

- **ID:** IMP-04  
  **Description:** Aggiungere regole di business per ciascuna operazione in combinazione con "%" e aggiornare US-03 e MF-04 con esempi.
  
  **Related Issue:** ISS-04  
  **Target Elements:** BR (nuove), US-03, MF-04  
  **Target Entities:** ENT-01, ENT-02  

---

- **ID:** IMP-05  
  **Description:** Definire il comportamento di pressioni ripetute del tasto "%" (es. applicare una sola volta per operando e ignorare pressioni successive fino a nuova digitazione).
  
  **Related Issue:** ISS-05  
  **Target Elements:** EC-01, MF-02  
  **Target Entities:** ENT-03  

---

## 🔗 Issue ↔ User Story Mapping

---

- ISS-01 → US-02, US-03  
- ISS-02 → US-01, US-02, US-03  
- ISS-03 → US-02, US-03  
- ISS-04 → US-03  
- ISS-05 → US-02  

### ✅ Validazione Mapping

- ✅ Tutte le issue (ISS-01 a ISS-05) sono mappate
- ✅ Tutte le User Story referenziate esistono nello structuring
- ✅ Ogni issue ha almeno una User Story associata

---

## ✅ Completeness Check

**Missing:**
- COMP-01: Semantica di "%" non definita (bloccante)
- COMP-02: Regole di business per "%" in combinazione con +,−,×,÷ mancanti
- COMP-03: State machine UI per tasto "%" mancante

**Weak Areas:**
- COMP-04: Alternative flow su input mancante per % non specifica messaggio/azione (errore vs ignore)
- COMP-05: Edge case EC-01 (%%) non definito

---

## 🔄 Consistency Check

- CONS-01: Vincolo "calcolo server-side" sembra in tensione con possibile necessità di aggiornare display immediatamente per %
- CONS-02: Domain model ENT-02 (Operazione Percentuale) esiste ma non ha regole operative definite

---

## ❗ Additional Open Questions

- OQ-REF-01: Deve esistere un tasto "+/-" (numero negativo) o la percentuale deve lavorare solo con numeri non negativi?
- OQ-REF-02: Il risultato percentuale va mostrato con 2 decimali sempre, anche per percentuali piccole (es. 0.01)?

---

## 📊 Confidence Assessment

**Level:** medium  

**Notes:** La feature è fattibile ma richiede una decisione chiara sulla semantica del tasto "%" e sul comportamento contestuale nella UI. Senza questa definizione, qualsiasi implementazione rischia di differire dalle aspettative dell'utente.
