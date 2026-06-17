# Proposal: Calculator Sequential Operations

Implement support for chained operations so that after `=`, the result becomes the next `A`.

Key changes:
- Add `resultShown` state in JS.
- On page load, sync `operandA` from display when a result is present.
- After result, selecting an operation prepares `B` entry; typing a number starts a new calculation.
- Reset percent flags after result.
