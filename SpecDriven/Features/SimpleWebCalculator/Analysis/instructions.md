# HUMAN DECISION REQUIRED

## 📋 Refinement Summary

Il refinement ha identificato **9 issue** che richiedono attenzione:

- **ISS-01** a **ISS-05**: Missing information (alternative flows mancanti, gestione edge cases)
- **ISS-06** e **ISS-09**: Ambiguità funzionali
- **ISS-07**: Inconsistenza nel Domain Model
- **ISS-08**: Missing User Story

---

## ✅ DECISION FORMAT

Indica quali issue devono essere applicate e quali saltate:

### APPLY:
- ISS-01 : overflow/underflow (importante per robustezza, ma edge case raro) 
- ISS-02 : arrotonda alla seconda cifra decimale
- ISS-03 : consenti solamente l'inserimento di numeri e caratteri necessari al calcolo (es. punto decimale), prevedi un tastierino con numeri da 1 a 9,0 , punto e operazioni
- ISS-06 : definire un'operazione di default (es. addizione) per migliorare UX
- ISS-07 : No business rules nel controller
- ISS-08 : aggiungere US per stato iniziale form, mi aspetto un form simile alle calcolatrici standard, con tastierino numerico e display vuoto
- ISS-09 : operazioni per numeri estremamente grandi o piccoli, definire un range accettabile (es. ±10^15) e gestire localizzazione per decimali (punto/virgola)
### SKIP:
- ISS-04
- ISS-05: Vedi ISS-02
- 

---

## 📊 Issue Overview

| ID | Type | Description | Impact |
|---|---|---|---|
| ISS-01 | missing_info | Alternative flows per overflow/underflow | US-03 |
| ISS-02 | missing_info | Gestione precisione decimale | US-03 |
| ISS-03 | missing_info | Alternative flow caratteri speciali | US-01, US-04 |
| ISS-04 | missing_info | Gestione submit multipli | US-03 |
| ISS-05 | ambiguity | Definizione precisione double | US-03 |
| ISS-06 | ambiguity | Selezione operazione default | US-02 |
| ISS-07 | inconsistency | Logica calcolo in ENT-02 vs BR | US-02, US-03 |
| ISS-08 | missing_info | User Story per stato iniziale form | MF-01 |
| ISS-09 | ambiguity | Range validation e localizzazione | US-01, US-04 |

---

## 💡 Recommendation

Per un **MVP funzionale minimo**, suggerisco:

**APPLY:**
- ISS-03 (validazione caratteri - critico per UX)
- ISS-05 (precisione decimale - critico per correttezza)
- ISS-06 (selezione operazione - critico per UX)
- ISS-07 (chiarimento domain model)

**SKIP (o fase successiva):**
- ISS-01 (overflow/underflow - edge case raro)
- ISS-02 (già coperto da ISS-05)
- ISS-04 (submit multipli - nice to have)
- ISS-08 (US implicita in MF-01)
- ISS-09 (decisioni di prodotto post-MVP)

---

⚠️ **LA PIPELINE È IN ATTESA DELLA TUA DECISIONE**

Modifica questo file con le tue scelte prima di proseguire.
