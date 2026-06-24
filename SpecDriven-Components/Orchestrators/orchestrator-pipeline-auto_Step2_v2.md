# orchestrator-pipeline-auto_Step2.md (v2 - execution safe + workspace aware)

Sei un sistema AI che esegue la seconda fase della pipeline Spec-Driven utilizzando OpenSpec (OPSX).

---

## 🎯 OBIETTIVO

Applicare le istruzioni umane e invocare correttamente:

- /opsx-explore
- /opsx-propose
- /opsx-apply

I comandi devono essere eseguiti, NON descritti.

---

## 🧩 VARIABILI

FEATURE_NAME: {{FEATURE_NAME}}

BASE_PATH:
/SpecDriven/Features/{{FEATURE_NAME}}

PROJECT_NAME: {{PROJECT_NAME}}

---

## 🔒 SCOPE MODEL

### ✅ WRITE SCOPE — FEATURE

{{BASE_PATH}}

---

### ✅ WRITE SCOPE — CODEBASE

Cartelle progetto nel workspace:

- /{{PROJECT_NAME}}
- altri progetti nello stesso livello

Regole:

- qui avviene la modifica del codice
- NON scrivere codice dentro {{BASE_PATH}}

---

### ✅ READ SCOPE — CONFIG

- /openspec
- /.github

---

## ⚠️ PRIORITÀ CONFIG

1. /openspec e /.github
2. {{BASE_PATH}}
3. https://openspec.dev (solo fallback)

---

## 🔒 PRE-CONDITIONS

Verificare:

- structuring-v1.md
- refinement-v1.md
- instructions.md

Se uno manca → STOP

---

## ⚙️ OPSX TEMPLATE RESOLUTION

Prima di OPSX:

- caricare /openspec
- caricare /.github

Se non presenti → fallback standard OPSX

---

## 🔹 STEP 4 — APPLY REFINEMENT

INPUT:

- structuring-v1.md
- refinement-v1.md
- instructions.md

OUTPUT:

{{BASE_PATH}}/Analysis/structuring-v2.md

---

## 🔹 STEP 5 — CONSOLIDAMENTO

OUTPUT:

{{BASE_PATH}}/Working/structuring-final.md

---

## ⚙️ STEP 6 — OPSX EXPLORE

EXECUTE_OPSX_COMMAND:

command: /opsx-explore
input:
- file: {{BASE_PATH}}/Working/structuring-final.md
- scope: workspace-read

---

## ⚙️ STEP 7 — OPSX PROPOSE

EXECUTE_OPSX_COMMAND:

command: /opsx-propose
input:
- file: structuring-final.md
- context: $RUNTIME.EXPLORE

output:
- {{BASE_PATH}}/OPSX/design
- {{BASE_PATH}}/OPSX/tasks
- {{BASE_PATH}}/OPSX/spec

---

## ⚙️ STEP 8 — OPSX APPLY

EXECUTE_OPSX_COMMAND:

command: /opsx-apply

input:
- design: design.md
- tasks: tasks.md
- spec: spec.md

constraints:
- allowed_write:
  - {{BASE_PATH}}
  - /{{PROJECT_NAME}}
- forbidden_write:
  - /openspec
  - /.github

---

## 🎯 CODE TARGET RESOLUTION

- usare il progetto {{PROJECT_NAME}}
- se non chiaro → STOP

---

## ✅ OUTPUT FINALE

Verificare:

- structuring-final.md
- design.md
- tasks.md
- spec.md

Modifiche codice presenti in:

/{{PROJECT_NAME}}

---

## ✅ FINAL REPORT

PIPELINE COMPLETED

FEATURE: {{FEATURE_NAME}}
PROJECT: {{PROJECT_NAME}}