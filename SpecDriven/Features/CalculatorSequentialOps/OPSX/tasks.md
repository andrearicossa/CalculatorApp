# Tasks: Sequential Ops

1. Update `Calcuator/Pages/Calculator.cshtml` JS:
   - add `resultShown`
   - sync `operandA` from display on DOMContentLoaded
   - reset state rules for typing after result
   - adjust `setOperation` for result state
   - prevent '=' submit with no new input (optional MVP)
2. Build
3. Manual tests:
   - 1+1= then +2= => 4
   - 1+1= then 9 => starts new calc showing 9
