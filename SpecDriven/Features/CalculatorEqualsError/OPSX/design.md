# Design: Fix '=' Error

## Likely root causes
1. Client-side JS exception during form submit (missing element, NaN, function not defined)
2. Hidden inputs not populated correctly → server receives null → unexpected path
3. Server-side unhandled exception (should be caught and converted to ErrorMessage)

## Design changes

### Client-side (Calculator.cshtml)
- In submit handler:
  - Guard against NaN / Infinity for operandA/operandB
  - Guard when isEnteringB=true and operandA is null
  - Always set hidden inputs only if values are valid finite numbers
  - If invalid, preventDefault and show user-friendly message (no alert if possible, but minimal change can keep alert)

### Server-side (CalculatorModel.OnPost)
- Already catches exceptions; ensure it handles invalid model binding:
  - If A or B are null, show ErrorMessage and return Page()
  - If Operation invalid, show ErrorMessage and return Page()

### Diagnostics
- Use browser console / network status to classify error

## Minimal risk approach
- Add defensive checks only; no functional changes if values are valid.
