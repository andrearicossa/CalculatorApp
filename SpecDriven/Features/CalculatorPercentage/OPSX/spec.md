# Spec: Calculator Percentage

## Summary
Add `%` capability to the existing calculator UI with standard calculator semantics.

## Semantics
- Simple mode (no A/op selected): `x% = x/100`
- Contextual mode (A and op selected, entering B):
  - add/sub: `B = A * (B/100)` then compute A ± B
  - mul/div: `B = B/100` then compute A * B or A / B
- Anti-%%: further presses ignored until new digit entry or clear

## UI
- Add a `%` button to `Pages/Calculator.cshtml` keypad.

## Client-side logic
- Implement `applyPercent()` and `percentApplied` guard.
- Keep server-side calculation unchanged; send normalized operands.

## Acceptance
- The 5 reference scenarios produce expected results.
