# Spec: Sequential Ops

Acceptance:
- After `=`, the result is used as next A.
- Example: `1 + 1 =` shows 2.00; then `+ 2 =` shows 4.00.

Implementation:
- Add `resultShown` and sync `operandA` from display on load.
- Typing after result without selecting operation starts a new calculation.
