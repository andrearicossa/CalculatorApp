# Change Proposal: Calculator Percentage

## What

Aggiungere alla calcolatrice web una funzionalità di percentuale (%) tramite un pulsante dedicato nel tastierino.

Comportamento definito (semantica contestuale "calcolatrice"):
- **% semplice**: se l'utente sta inserendo un numero senza contesto operativo → `x%` trasforma `x` in `x/100`.
- **% contestuale**: se esistono A e un'operazione selezionata e l'utente sta inserendo B:
  - `A + B%` → `A + (A * B/100)`
  - `A - B%` → `A - (A * B/100)`
  - `A * B%` → `A * (B/100)`
  - `A / B%` → `A / (B/100)`
- Pressioni ripetute di `%` sullo stesso operando sono ignorate (anti-%%).

## Why

Permette di replicare un comportamento comune delle calcolatrici standard e velocizzare calcoli frequenti (sconti, incrementi percentuali, ecc.).

## Scope

In scope:
- Aggiunta pulsante `%` alla UI della Razor Page Calculator
- Estensione della logica JavaScript del tastierino per gestire % semplice e contestuale
- Eventuale estensione del servizio di calcolo solo se necessario (preferenza: trasformare operandi prima del submit)
- Validazioni e rounding esistenti invariati

Out of scope:
- Operazioni avanzate (es: percentuale composta, interesse, IVA)
- Storico calcoli

## Success Criteria

- `%` visibile nel tastierino
- `%` aggiorna correttamente il valore in display secondo regole definite
- `=` produce risultati corretti per casi con percentuale
- Nessun regressione su +,-,*,/
- Range validation e rounding a 2 decimali mantenuti

## Risks

- Complessità state machine JS: mitigare con test manuali mirati

---

**Status**: Proposed
