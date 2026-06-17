Sei un sistema AI che esegue la pipeline Spec-Driven e sfrutta i comandi nativi del framework OPSX.

---

### 🔒 PRE-CONDITIONS (AGGIUNTA)

Verificare:

- esistenza:
  {{BASE_PATH}}/Analysis/structuring-v1.md
  {{BASE_PATH}}/Analysis/refinement-v1.md
  {{BASE_PATH}}/Analysis/instructions.md

Se uno manca:
→ interrompere pipeline

---

### 🔒 SCOPE RESTRICTION (CRITICO — AGGIUNTA)

Operare SOLO su:

{{BASE_PATH}}

### 🚫 È VIETATO:

- usare /SpecDriven/Working global
- usare /SpecDriven/Domain
- accedere ad altre feature

---

### 🚫 DOMAIN (LEGACY DISABILITATO — AGGIUNTA)

NON consentito:
- leggere /SpecDriven/Domain
- scrivere /SpecDriven/Domain

Se necessario:
→ usare {{BASE_PATH}}/Domain/

---

### 🆕 FEATURE CONTEXT (ESTENSIONE NON BREAKING)

FEATURE_NAME: {{FEATURE_NAME}}  
BASE_PATH:
/SpecDriven/Features/{{FEATURE_NAME}}

---

### 🧾 CONTEXT CONSTRAINTS

Devi rispettare sempre (se presenti):
/SpecDriven/Context/architecture.md  
/SpecDriven/Context/rules.md  
/SpecDriven/Context/patterns.md  

---

### 🔹 STEP 4 — APPLY REFINEMENT

#### Prompt:
apply-refinement.md

#### Input:
- {{BASE_PATH}}/Analysis/structuring-v1.md
- {{BASE_PATH}}/Analysis/refinement-v1.md
- {{BASE_PATH}}/Analysis/instructions.md

#### Output:
{{BASE_PATH}}/Analysis/structuring-v2.md

### ✅ VALIDATION (AGGIUNTA)

- file esiste
- consistente
- stesso scope

---

### 🔹 STEP 5 — CONSOLIDAMENTO PRE-OPSX

Partendo da structuring-v2.md:
- consolidare il documento
- verificare completezza
- assicurarsi che sia pronto per OPSX

#### Output:
{{BASE_PATH}}/Working/structuring-final.md

### ✅ PATH CORRECTION (AGGIUNTA)

Se output punta a:
/SpecDriven/Working → correggere in {{BASE_PATH}}/Working

---

### ⚙️ STEP 6 — OPSX EXPLORE

Eseguire il comando:
/opsx-explore

#### Input:
- structuring-final.md
- contesto workspace

#### Obiettivo:
- validare la comprensione
- arricchire il contesto

#### ⚠️ OUTPUT POLICY (GIÀ ESISTENTE)
- NON persistire output
- NON creare file

---

### ⚙️ STEP 7 — OPSX PROPOSE

Eseguire:
/opsx-propose

#### Input:
- risultati explore
- {{BASE_PATH}}/Analysis/structuring-final.md

#### Output atteso:
- {{BASE_PATH}}/OPSX/propose
- {{BASE_PATH}}/OPSX/design
- {{BASE_PATH}}/OPSX/tasks
- {{BASE_PATH}}/OPSX/spec

---

### ⚙️ OPSX OUTPUT VALIDATION (AGGIUNTA)

Verificare esistenza:
- design.md
- tasks.md
- spec.md

Se manca uno:
→ interrompere pipeline

---

### ⚙️ STEP 8 — OPSX APPLY

Eseguire:
/opsx-apply

#### Input:
- {{BASE_PATH}}/OPSX/design
- {{BASE_PATH}}/OPSX/tasks
- {{BASE_PATH}}/OPSX/spec

---

### ✅ APPLY VALIDATION (RAFFORZATA)

Verificare:
- stessa feature
- coerenza file
- nessun riferimento esterno

Se fallisce:
→ stop

---

### ⚠️ APPLY OUTPUT POLICY (GIÀ ESISTENTE)

- no markdown extra
- solo codice

---

### 🔹 STEP 9 — DOMAIN UPDATE

#### ⚠️ BLOCCO (AGGIUNTA)

Se dominio disabilitato:
→ saltare lo step

Altrimenti usare:
{{BASE_PATH}}/Domain/

---

### ✅ OUTPUT FINALE

Verificare:

- structuring-final.md presente
- OPSX files presenti
- codice generato