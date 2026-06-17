# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-008

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** inconsistency  
  **Description:** L'utente ha modificato manualmente il metodo Clear usando `Session.Clear()`, ma l'obiettivo dichiarato è pulire solo la history. `Session.Clear()` può cancellare dati non correlati (es. future feature) ed è incoerente con una cancellazione mirata.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** BR-01, CON-02  
  **Impacted Entities:** ENT-01, ENT-03  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Non è definito un meccanismo UI per aggiornare immediatamente il contenuto del popup dopo Clear. Attualmente il popup mostra dati server-rendered e senza re-render rimane stale.  
  **Impacted User Stories:** US-01, US-02  
  **Impacted Elements:** TN-01, MF-05  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-03  
  **Type:** ambiguity  
  **Description:** Non è deciso se, quando Clear viene premuto con popup aperto, il popup debba restare aperto e aggiornarsi a "No history" oppure chiudersi automaticamente. Serve decisione UX.  
  **Impacted User Stories:** US-02  
  **Impacted Elements:** AF-01, OQ-01  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Ripristinare la cancellazione mirata della history: usare `Session.Remove("CalculatorHistory")` nel handler (o mantenere già esistente). Evitare `Session.Clear()`.
  
  **Related Issue:** ISS-01  
  **Target Elements:** BR-01  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-02  
  **Description:** In `clearAll()` aggiornare il DOM del modal svuotando la lista e mostrando placeholder "No history" subito dopo la fetch. Opzionale: chiudere il modal.
  
  **Related Issue:** ISS-02  
  **Target Elements:** MF-05  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-03  
  **Description:** Decidere UX per Clear con modal aperto. Raccomandazione: tenere aperto e mostrare subito "No history" (minimo intrusive).  
  **Related Issue:** ISS-03  
  **Target Elements:** AF-01  
  **Target Entities:** ENT-02  

---

## 🔗 Issue ↔ User Story Mapping

- ISS-01 → US-01
- ISS-02 → US-01, US-02
- ISS-03 → US-02

---

## 📊 Confidence Assessment

**Level:** high  
**Notes:** La soluzione richiede un piccolo aggiustamento server-side (remove mirato) e un update client-side del modal per evitare contenuto stale.
