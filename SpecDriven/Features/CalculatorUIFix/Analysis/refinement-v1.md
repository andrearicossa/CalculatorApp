# REFINEMENT REPORT

---

## 📌 Feature
**ID:** FEAT-003

---

## 🔴 Issues Detected

---

- **ID:** ISS-01  
  **Type:** missing_info  
  **Description:** Non è chiaro se Bootstrap è installato localmente nel progetto (wwwroot/lib/bootstrap) o se va usato da CDN. Questo impatta il contenuto del tag <link> per il CSS e <script> per JS nel layout.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** BR-02, BR-03, TN-04  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-02  
  **Type:** missing_info  
  **Description:** Non è specificato se Pages/_ViewStart.cshtml esiste già o va creato. Se esiste, potrebbe avere configurazioni che interferiscono con il nuovo layout.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** BR-07, TN-03  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-03  
  **Type:** ambiguity  
  **Description:** TN-03 propone tre opzioni per risolvere il problema ma non viene selezionata una soluzione definitiva. Serve decidere esplicitamente quale approccio adottare (Opzione 1, 2, o 3).  
  **Impacted User Stories:** US-01, US-02, US-03  
  **Impacted Elements:** TN-03, BR-01  
  **Impacted Entities:** ENT-01, ENT-02  
  **Congruence Level:** HIGH  

---

- **ID:** ISS-04  
  **Type:** missing_info  
  **Description:** Non è chiaro se il layout MVC (Views/Shared/_Layout.cshtml) è già aggiornato con il navbar Calculator/Home/Privacy (da FEAT-002) o contiene ancora il menu legacy. Questo impatta se il nuovo layout Razor Pages deve includerlo o meno.  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** TN-04, OQ-03  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** MEDIUM  

---

- **ID:** ISS-05  
  **Type:** missing_info  
  **Description:** Manca la specifica di come gestire la sezione Styles (CSS custom) nella pagina Calculator. Il layout deve supportare @RenderSection("Styles") oltre a @RenderSection("Scripts")?  
  **Impacted User Stories:** US-01  
  **Impacted Elements:** TN-04  
  **Impacted Entities:** ENT-02  
  **Congruence Level:** LOW  

---

- **ID:** ISS-06  
  **Type:** inconsistency  
  **Description:** AF-03.6 suggerisce che "se il JavaScript ha un errore critico, potrebbe bloccare il submit o generare exception", ma non è chiaro quale tipo di errore specifico (console error, unhandled exception, form validation block).  
  **Impacted User Stories:** US-03  
  **Impacted Elements:** AF-03, EC-05  
  **Impacted Entities:** ENT-03  
  **Congruence Level:** LOW  

---

## 🛠 Suggested Improvements

---

- **ID:** IMP-01  
  **Description:** Verificare se Bootstrap è presente in wwwroot/lib/bootstrap controllando file system. Se presente, usare riferimenti locali; altrimenti usare CDN (Bootstrap 5.3.x). Documentare la scelta in una business rule.  
  **Related Issue:** ISS-01  
  **Target Elements:** BR-02, BR-03  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-02  
  **Description:** Verificare esistenza di Pages/_ViewStart.cshtml. Se non esiste, crearlo con `Layout = "_Layout"`. Se esiste, verificarne il contenuto e documentare se richiede modifiche.  
  **Related Issue:** ISS-02  
  **Target Elements:** BR-07, TN-03  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-03  
  **Description:** Selezionare esplicitamente **Opzione 1** come soluzione raccomandata: creare Pages/Shared/_Layout.cshtml copiando da Views/Shared/_Layout.cshtml e adattandolo per Razor Pages. Aggiungere questa decisione come business rule.  
  **Related Issue:** ISS-03  
  **Target Elements:** TN-03, BR-01  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-04  
  **Description:** Verificare il contenuto di Views/Shared/_Layout.cshtml per confermare se include navbar aggiornata da FEAT-002. Se sì, copiare la navbar nel nuovo layout Razor Pages; altrimenti, aggiornare prima Views/Shared/_Layout.cshtml.  
  **Related Issue:** ISS-04  
  **Target Elements:** TN-04  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-05  
  **Description:** Aggiungere supporto per @RenderSection("Styles", required: false) nel nuovo layout Pages/Shared/_Layout.cshtml per consentire CSS custom per singole pagine.  
  **Related Issue:** ISS-05  
  **Target Elements:** TN-04  
  **Target Entities:** ENT-02  

---

- **ID:** IMP-06  
  **Description:** Chiarire in AF-03.6 che l'errore specifico è "JavaScript function not defined" (console error) che previene l'esecuzione del form submit handler, non un'exception server-side.  
  **Related Issue:** ISS-06  
  **Target Elements:** AF-03.6  
  **Target Entities:** ENT-03  

---

## 🔗 Issue ↔ User Story Mapping

---

- ISS-01 → US-01  
- ISS-02 → US-01  
- ISS-03 → US-01, US-02, US-03  
- ISS-04 → US-01  
- ISS-05 → US-01  
- ISS-06 → US-03  

### ✅ Validazione Mapping

- ✅ Tutte le issue (ISS-01 a ISS-06) sono mappate
- ✅ Tutte le User Story referenziate (US-01, US-02, US-03) esistono nello structuring
- ✅ Ogni issue ha almeno una User Story associata

---

## ✅ Completeness Check

**Missing:**
- COMP-01: Manca verifica stato Bootstrap nel progetto (locale vs CDN)
- COMP-02: Manca verifica esistenza e contenuto Pages/_ViewStart.cshtml
- COMP-03: Manca selezione esplicita dell'approccio risolutivo (3 opzioni proposte ma nessuna scelta definitiva)

**Weak Areas:**
- COMP-04: L'alternative flow AF-03 descrive il problema ma non specifica chiaramente la soluzione (come evitare submit con campi vuoti)
- COMP-05: Manca un task esplicito per testare la soluzione dopo implementazione
- COMP-06: Non è specificato se serve aggiornare anche la documentazione esistente (FEAT-001 design/spec)

---

## 🔄 Consistency Check

- CONS-01: TN-03 propone Opzione 1 come "Recommended" ma BR-01 non riflette questa scelta esplicita
- CONS-02: AF-01 descrive "situazione attuale" ma non è collegato a una soluzione specifica nel main flow
- CONS-03: EC-01 ed EC-02 sono conseguenze dell'assenza di layout ma non hanno alternative flow corrispondenti

### ✅ Controlli aggiuntivi

- ✅ Ogni issue è collegata ad almeno una User Story
- ✅ Ogni issue è collegata ad almeno un elemento (MF, BR, AF, EC, TN)
- ✅ Le entità coinvolte (ENT-01, ENT-02, ENT-03) sono coerenti con i flussi
- ⚠️ REL-02 è definita ma non viene referenziata esplicitamente nelle issue o nei flussi

---

## ❗ Additional Open Questions

- OQ-REF-01: Il nuovo layout Pages/Shared/_Layout.cshtml deve essere identico a Views/Shared/_Layout.cshtml o può essere semplificato (es. senza funzionalità specifiche MVC)?
- OQ-REF-02: Serve un task per verificare che altre eventuali Razor Pages nel progetto (es. Privacy se esiste) funzionino correttamente con il nuovo layout?
- OQ-REF-03: Come gestire eventuali Razor Pages future? Documentare pattern/template per creazione nuove pagine?

---

## 📊 Confidence Assessment

**Level:** high  

**Notes:**
- Qualità generale dell'analisi: molto buona, causa radice chiaramente identificata
- Presenza lacune rilevanti: sì, principalmente decisioni implementative (quale opzione, Bootstrap locale vs CDN, contenuto _ViewStart)
- Livello di coerenza tra User Story, Issue e Domain Model: alto; le issue sono strettamente collegate alla causa radice (mancanza layout)
- La maggior parte delle issue sono di tipo missing_info o ambiguity, indicando che l'analisi funzionale è corretta ma richiede verifiche tecniche e decisioni
- ISS-03 (ambiguity su quale opzione scegliere) è bloccante per l'implementazione

---

**Raccomandazione**: Prima di procedere con APPLY, eseguire verifiche tecniche (TASK: check Bootstrap, check _ViewStart, check Views layout) e selezionare esplicitamente **Opzione 1** (Pages/Shared/_Layout.cshtml dedicato) come soluzione.
