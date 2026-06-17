# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-007

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** ambiguity  
  **Description:** Il requisito "memorizzare lo storico in sessione" è ambiguo: può significare (A) server-side session (HttpContext.Session) oppure (B) sessione client-side (memoria JS finché la pagina resta aperta). La scelta impatta implementazione e persistenza durante refresh della pagina (POST reload).  
  **Impacted User Stories:** US-01, US-03  
  **Impacted Elements:** TN-01, CON-01  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Non è definito come catturare l'evento "calcolo riuscito" per aggiungere una entry allo storico. In questo progetto il risultato viene calcolato server-side via POST e la pagina viene ricaricata; serve specificare dove e quando appendere la history (server prima di render, oppure client leggendo Result renderizzato).  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** MF-02, MF-03, BR-01, TN-02  
  **Impacted Entities:** ENT-01, ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-03  
  **Type:** missing_info  
  **Description:** Non è definito cosa succede alla history dopo un refresh (che avviene naturalmente dopo POST). Se la history è client-side pura, si perderebbe. Se deve restare finché Clear, serve un meccanismo di persistenza (server session o hidden field).  
  **Impacted User Stories:** US-01, US-03  
  **Impacted Elements:** CON-01, TN-01  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-04  
  **Type:** missing_info  
  **Description:** Il popup/modal non è specificato in termini di componente Bootstrap (Modal) e markup, né l'interazione (open/close, focus). Serve definire che useremo Bootstrap Modal e controlli standard.  
  **Impacted User Stories:** US-02  
  **Impacted Elements:** BR-03, CON-04  
  **Impacted Entities:** ENT-03  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-05  
  **Type:** missing_info  
  **Description:** Non è definito il formato di una riga history (testo). Esempio: "A op B = Result" con 2 decimali. Serve esplicitare per coerenza e test.  
  **Impacted User Stories:** US-01, US-02  
  **Impacted Elements:** ENT-01, BR-01  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** MEDIUM  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Decidere storage della history: raccomandazione per questo progetto (POST reload) -> **server-side session** per mantenere history tra i postback finché Clear, senza database.  
  **Related Issue:** ISS-01, ISS-03  
  **Target Elements:** CON-01, TN-01  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-02  
  **Description:** Registrare history in PageModel dopo calcolo riuscito: se Result valorizzato e niente ErrorMessage, appendere entry a session (list).  
  **Related Issue:** ISS-02  
  **Target Elements:** MF-03, BR-01  
  **Target Entities:** ENT-01, ENT-02  

---

- **ID:** IMP-03  
  **Description:** Definire Bootstrap Modal per History con lista scrollabile: usare `modal-dialog-scrollable` o un contenitore con `max-height` + `overflow-y:auto`.  
  **Related Issue:** ISS-04  
  **Target Elements:** BR-03, BR-04  
  **Target Entities:** ENT-03  

---

- **ID:** IMP-04  
  **Description:** Definire formato entry: `"{A} {opSymbol} {B} = {Result}"` con 2 decimali. Per operazione percentuale, includere `%` se presente nella UI.  
  **Related Issue:** ISS-05  
  **Target Elements:** BR-01  
  **Target Entities:** ENT-01  

---

## 🔗 Issue ↔ User Story Mapping

- ISS-01 → US-01, US-03
- ISS-02 → US-01
- ISS-03 → US-01, US-03
- ISS-04 → US-02
- ISS-05 → US-01, US-02

---

## ✅ Completeness Check

**Missing:**
- COMP-01: Scelta storage history (server vs client)
- COMP-02: Hook "calcolo riuscito" per append history
- COMP-03: Persistenza tra postback

**Weak Areas:**
- COMP-04: Specifica popup/modal incompleta
- COMP-05: Formato entry non definito

---

## 📊 Confidence Assessment

**Level:** medium  
**Notes:** La feature è chiara ma servono decisioni su storage e integrazione con flusso POST. Raccomandazione: usare session server-side per coerenza con Razor Pages postback.
