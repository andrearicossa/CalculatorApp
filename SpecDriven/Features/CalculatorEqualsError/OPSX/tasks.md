# Tasks: Fix '=' Error

## TASK-01: Reproduce and classify
- Press `=` with:
  - no input
  - A only
  - A + op + B
  - after % usage (if enabled)
- Check browser console + network response

## TASK-02: Harden client-side submit handler
- Update `Calcuator/Pages/Calculator.cshtml`
- Validate operandA/operandB are finite numbers
- Ensure correct assignment when only one operand entered

## TASK-03: Verify server-side remains safe
- Ensure OnPost returns friendly ErrorMessage for invalid inputs

## TASK-04: Build and manual verification
- Confirm no crash
