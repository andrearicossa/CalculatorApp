# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-001

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** missing_info  
  **Description:** Mancano alternative flows per gestire l'overflow/underflow numerico (EC-01, EC-02) nonostante siano identificati come edge cases. Non è chiaro come il sistema debba comportarsi in questi scenari.  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** EC-01, EC-02, MF-05  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Manca la gestione dell'edge case relativo a risultati con molte cifre decimali (EC-03). Non è specificato se il risultato deve essere troncato, arrotondato o visualizzato per intero.  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** EC-03, MF-06, BR-01, BR-02, BR-03, BR-04  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-03  
  **Type:** missing_info  
  **Description:** Non è presente un alternative flow per gestire l'edge case relativo a caratteri speciali o spazi nei campi numerici (EC-04), anche se viene identificato come possibile problema.  
  **Impacted User Stories:** US-01, US-04  
  **Impacted Elements:** EC-04, MF-02  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-04  
  **Type:** missing_info  
  **Description:** L'edge case EC-05 (submit multipli consecutivi) viene identificato ma non è presente un alternative flow né una business rule che specifichi il comportamento atteso del sistema.  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** EC-05, MF-04  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-05  
  **Type:** ambiguity  
  **Description:** La business rule BR-05 specifica "double precision" ma non definisce il numero di cifre decimali da visualizzare o memorizzare nel risultato, creando ambiguità con EC-03.  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** BR-05, EC-03  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-06  
  **Type:** ambiguity  
  **Description:** Il constraint CON-03 richiede la selezione di "esattamente un'operazione" ma non specifica se deve esserci un valore di default o se l'utente deve compiere una scelta esplicita.  
  **Impacted User Stories:** US-02  
  **Impacted Elements:** CON-03, MF-03  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-07  
  **Type:** inconsistency  
  **Description:** L'entità ENT-02 (Operazione) include "Logica di calcolo" come attributo chiave, ma le business rules (BR-01 a BR-04) sono definite separatamente. Non è chiaro se la logica risiede nell'entità o viene applicata esternamente.  
  **Impacted User Stories:** US-02, US-03  
  **Impacted Elements:** BR-01, BR-02, BR-03, BR-04  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-08  
  **Type:** missing_info  
  **Description:** Non è presente una User Story specifica per la visualizzazione iniziale della pagina calcolatrice (stato iniziale del form). MF-01 descrive l'accesso ma non esiste una US corrispondente.  
  **Impacted User Stories:** (nuova US necessaria)  
  **Impacted Elements:** MF-01  
  **Impacted Entities:** —  
  **Congruence Level:** LOW  

---

- **ID:** ISS-09  
  **Type:** ambiguity  
  **Description:** Le open questions OQ-03 e OQ-04 riguardano aspetti funzionali critici (range validation, localizzazione decimali) che potrebbero impattare significativamente la validazione e le business rules, ma non sono indirizzati.  
  **Impacted User Stories:** US-01, US-04  
  **Impacted Elements:** CON-01, AF-01  
  **Impacted Entities:** ENT-01  
  **Congruence Level:** MEDIUM  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Aggiungere alternative flows specifici per EC-01 (overflow), EC-02 (underflow) e EC-03 (precisione decimale) con comportamenti definiti (es. messaggio di errore, arrotondamento, limite cifre).  
  **Related Issue:** ISS-01, ISS-02  
  **Target Elements:** AF (nuovi), EC-01, EC-02, EC-03  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-02  
  **Description:** Aggiungere alternative flow per EC-04 (caratteri speciali/spazi) che definisca se la validazione deve rimuovere automaticamente i caratteri o mostrare errore.  
  **Related Issue:** ISS-03  
  **Target Elements:** AF (nuovo), EC-04  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-03  
  **Description:** Aggiungere business rule o alternative flow per gestire EC-05 (submit multipli), ad esempio disabilitando il pulsante dopo il primo click fino al completamento della richiesta.  
  **Related Issue:** ISS-04  
  **Target Elements:** AF (nuovo), BR (nuovo)  
  **Target Entities:** —  

---

- **ID:** IMP-04  
  **Description:** Integrare BR-05 con una regola esplicita sul numero di cifre decimali da visualizzare (es. max 6 decimali, arrotondamento a 2 per operazioni standard).  
  **Related Issue:** ISS-05  
  **Target Elements:** BR-05  
  **Target Entities:** ENT-01  

---

- **ID:** IMP-05  
  **Description:** Chiarire in CON-03 se esiste un'operazione di default pre-selezionata o se l'utente deve effettuare una selezione esplicita prima del calcolo.  
  **Related Issue:** ISS-06  
  **Target Elements:** CON-03, MF-03  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-06  
  **Description:** Allineare la definizione di ENT-02 con le business rules, chiarendo se "Logica di calcolo" è un attributo dell'entità o se le BR sono applicate dal controller. Considerare se ENT-02 sia effettivamente necessaria come entità di dominio.  
  **Related Issue:** ISS-07  
  **Target Elements:** ENT-02, BR-01, BR-02, BR-03, BR-04  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-07  
  **Description:** Aggiungere una User Story esplicita per la visualizzazione iniziale della pagina/form (es. "Come utente voglio accedere a una pagina con un form vuoto pronto per l'inserimento").  
  **Related Issue:** ISS-08  
  **Target Elements:** MF-01  
  **Target Entities:** —  

---

- **ID:** IMP-08  
  **Description:** Trasformare OQ-03 e OQ-04 in decisioni funzionali esplicite: definire range numerico accettabile (es. ±10^15) e formato decimale (punto o virgola, eventualmente gestito via browser locale).  
  **Related Issue:** ISS-09  
  **Target Elements:** CON-01, BR (nuova)  
  **Target Entities:** ENT-01  

---

## 🔗 Issue ↔ User Story Mapping

---

- ISS-01 → US-03  
- ISS-02 → US-03  
- ISS-03 → US-01, US-04  
- ISS-04 → US-03  
- ISS-05 → US-03  
- ISS-06 → US-02  
- ISS-07 → US-02, US-03  
- ISS-08 → (nuova US necessaria)  
- ISS-09 → US-01, US-04  

### ✅ Validazione Mapping

- ✅ Tutte le issue (ISS-01 a ISS-09) sono mappate
- ✅ Tutte le User Story referenziate (US-01, US-02, US-03, US-04) esistono nello structuring
- ⚠️ ISS-08 segnala la necessità di una nuova User Story

---

## ✅ Completeness Check

**Missing:**
- COMP-01: Mancano alternative flows per 4 degli edge cases identificati (EC-01, EC-02, EC-03, EC-04, EC-05)
- COMP-02: Manca una User Story esplicita per lo stato iniziale del form (MF-01)
- COMP-03: Mancano business rules per: gestione precisione decimale, range validation, behavior su submit multipli

**Weak Areas:**
- COMP-04: Gli edge cases sono ben identificati ma non hanno copertura nei flussi alternativi
- COMP-05: Le open questions OQ-03 e OQ-04 dovrebbero essere risolte in fase funzionale (impattano validazione e UX)
- COMP-06: Domain Model: l'entità ENT-02 (Operazione) ha utilità limitata e potrebbe non essere necessaria come entità di dominio

---

## 🔄 Consistency Check

- CONS-01: Incoerenza tra attributo "Logica di calcolo" in ENT-02 e business rules BR-01 a BR-04 definite separatamente
- CONS-02: EC-03 (precisione decimale) e BR-05 (double precision) non sono allineati: manca definizione di policy di visualizzazione/arrotondamento
- CONS-03: Tutti gli edge cases (EC-01 a EC-05) sono identificati ma solo 3 alternative flows esistono (AF-01, AF-02, AF-03), lasciando scoperti EC-01, EC-02, EC-03, EC-04, EC-05

### ✅ Controlli aggiuntivi

- ✅ Ogni issue è collegata ad almeno una User Story (eccetto ISS-08 che ne richiede una nuova)
- ✅ Ogni issue è collegata ad almeno un elemento (MF, BR, EC, CON)
- ✅ Le entità coinvolte (ENT-01, ENT-02) sono coerenti con i flussi
- ⚠️ ENT-02 è poco utilizzata nei flussi: appare solo in US-02 e potrebbe essere semplificata

---

## ❗ Additional Open Questions

- OQ-REF-01: L'entità ENT-02 (Operazione) deve essere persistente o può essere gestita come enum/lista statica?
- OQ-REF-02: Quale livello di validazione client-side è accettabile prima della validazione server-side (considerando BR-06)?
- OQ-REF-03: È necessario un feedback visivo durante l'elaborazione server-side per migliorare la UX?
- OQ-REF-04: Come gestire il caso in cui l'utente modifichi i valori dopo aver ottenuto un risultato? Il form deve essere resettato o mantenere i valori?

---

## 📊 Confidence Assessment

**Level:** medium  

**Notes:**
- Qualità generale dell'analisi: buona, con User Story chiare e domain model esplicito
- Presenza lacune rilevanti: sì, principalmente nella copertura degli edge cases con alternative flows
- Livello di coerenza tra User Story, Issue e Domain Model: medio-alto; alcuni miglioramenti necessari per ENT-02 e per collegare meglio gli edge cases ai flussi
- La maggior parte degli issue identificati sono di tipo missing_info, indicando che l'analisi è coerente ma incompleta in alcuni aspetti funzionali
- Le issue hanno congruenza generalmente alta con le User Story impattate
