# Spec: Fix '=' Error

## Acceptance
- `=` never causes app crash.
- For incomplete input, show a friendly message.
- For valid input, calculate and show result.

## Implementation
- Add guards in JS submit handler:
  - reject NaN/Infinity
  - ensure hidden inputs set only with finite numbers
- Keep server-side try/catch; ensure null inputs are handled.
