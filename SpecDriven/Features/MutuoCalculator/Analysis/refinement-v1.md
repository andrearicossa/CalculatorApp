# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-010

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** missing_info  
  **Description:** Non è definita la frequenza "quindicinale". La documentazione elenca mensile/trimestrale/semestrale/annuale. Serve conferma del set definitivo di frequenze per l'implementazione.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** BR-02, OQ-01  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Non è specificato se la tabella del piano di ammortamento deve essere paginata o semplicemente scrollabile. Per piani lunghi (es. 360 rate) la UX può risentirne.  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** OQ-02, EC-02  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-03  
  **Type:** missing_info  
  **Description:** Non è definita la gestione dell'edge case EC-01 (tasso = 0): il sistema deve supportarlo oppure deve mostrare un errore di validazione?  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** EC-01, BR-04  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-04  
  **Type:** missing_info  
  **Description:** Non è specificato il tipo del campo DurataAnni: se espresso in anni (intero) o in numero di rate totali. Serve uniformità con la formula di calcolo.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** BR-01, BR-05  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** HIGH  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Definire set fisso: mensile, trimestrale, semestrale, annuale. Escludere quindicinale per MVP.  
  **Related Issue:** ISS-01  
  **Target Elements:** BR-02  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-02  
  **Description:** Tabella scrollabile (overflow-y) per MVP; paginazione come evoluzione futura.  
  **Related Issue:** ISS-02  
  **Target Elements:** EC-02  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-03  
  **Description:** Tasso = 0 supportato: la rata è C/n, nessun interesse. Non è un errore di validazione.  
  **Related Issue:** ISS-03  
  **Target Elements:** EC-01, BR-04  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-04  
  **Description:** DurataAnni = intero in anni. Il numero totale di rate si ottiene moltiplicando durata × frequenza annua.  
  **Related Issue:** ISS-04  
  **Target Elements:** BR-01, BR-05  
  **Target Entities:** ENT-01  

---

## 🔗 Issue ↔ User Story Mapping

- ISS-01 → US-01
- ISS-02 → US-03
- ISS-03 → US-01
- ISS-04 → US-01

---

## ✅ Completeness Check

- COMP-01: Frequenza rate → risolta con IMP-01
- COMP-02: Tabella scrollabile → risolta con IMP-02
- COMP-03: Tasso 0 → risolta con IMP-03
- COMP-04: DurataAnni in anni → risolta con IMP-04

---

## 📊 Confidence Assessment

**Level:** high  
**Notes:** Tutte le issue hanno soluzione chiara. Il design è diretto e allineato all'architettura esistente.
