# Design: Sequential Ops

## Client-side state
Add:
- `let resultShown = false;`

## Sync on load
On `DOMContentLoaded`:
- read `currentDisplay.value`
- parse to number; if finite and not currently typing, set:
  - `operandA = parsed`
  - `resultShown = true`
  - `isEnteringB = false`
  - `currentInput = ''`
  - `percentApplied = false`

## Typing after result
In `appendToDisplay`:
- if `resultShown === true && !isEnteringB`:
  - reset state and start a new number (currentInput becomes the digit)

## Operation after result
In `setOperation`:
- if `resultShown === true && operandA !== null`:
  - set `isEnteringB = true`
  - clear `currentInput`

## '=' behavior
If `resultShown === true` and `currentInput === ''` and no new B entered:
- prevent submit (MVP)

## Server
No server change required; still computes from hidden inputs.
