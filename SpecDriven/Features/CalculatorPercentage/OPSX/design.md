# Design: Calculator Percentage

## Approach

Implementare `%` principalmente **client-side** come trasformazione degli operandi prima del submit, mantenendo il calcolo finale server-side:
- La UI/JavaScript trasforma `currentInput` (numero inserito) in base al contesto.
- Il form invia al server `A`, `B`, `Operation` già normalizzati.
- `CalculatorService` rimane invariato (usa add/sub/mul/div su double), salvo necessità di gestione casi limite.

Motivazione:
- Evita di introdurre una nuova operation code nel server.
- Mantiene vincolo di calcolo server-side (il risultato finale viene comunque calcolato dal servizio).

## UI Changes

Aggiungere pulsante `%` nel tastierino `Pages/Calculator.cshtml`.

Layout consigliato (riga superiore o vicino al punto):
- Opzione semplice: sostituire un pulsante non essenziale o aggiungere una riga.

## JavaScript State Machine

Stato già esistente:
- `currentInput`
- `operandA`, `operandB`
- `operation`
- `isEnteringB`

Aggiunte:
- `percentApplied` (boolean) per anti-%%, resettato quando l'utente inserisce nuove cifre o fa clear.

Funzione nuova: `applyPercent()`

Pseudo-logica:
1. Se `currentInput` è vuoto → ignore (o messaggio) (decisione UX non bloccante)
2. Se `percentApplied` è true → ignore
3. Se `isEnteringB` è false:
   - Trasforma `currentInput = parseFloat(currentInput) / 100`
4. Se `isEnteringB` è true:
   - Calcola `b = parseFloat(currentInput)`
   - Se operation è add/sub: `b = operandA * (b/100)`
   - Se operation è mul/div: `b = b/100`
   - Imposta `currentInput = b`
5. Set `percentApplied = true`
6. Update display

Submit:
- On submit, i hidden inputs A/B devono usare i valori finali.

## Server

`CalculatorService` resta invariato.

---

## Testing

Casi minimi:
- `20 %` → 0.20
- `50 + 10 % =` → 55.00
- `50 - 10 % =` → 45.00
- `50 * 10 % =` → 5.00
- `50 / 10 % =` → 500.00
- `20 %%` → invariato dopo primo %
