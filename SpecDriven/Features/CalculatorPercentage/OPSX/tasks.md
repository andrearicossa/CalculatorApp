# Tasks: Calculator Percentage

## TASK-01 — Add % button to calculator keypad
- Update `Calcuator/Pages/Calculator.cshtml`
- Add a `%` button (type="button") wired to `applyPercent()`

## TASK-02 — Implement percent state + logic in JavaScript
- In `Calcuator/Pages/Calculator.cshtml` script section:
  - Add `let percentApplied = false;`
  - Reset on digit append and clear
  - Implement `applyPercent()` per design

## TASK-03 — Ensure hidden inputs are populated with transformed values
- Ensure submit handler uses the current transformed values of A/B

## TASK-04 — Manual testing
- Validate cases:
  - 20% → 0.20
  - 50 + 10% = 55.00
  - 50 - 10% = 45.00
  - 50 * 10% = 5.00
  - 50 / 10% = 500.00
  - %% ignored

## TASK-05 — Build
- Run build and ensure no errors
