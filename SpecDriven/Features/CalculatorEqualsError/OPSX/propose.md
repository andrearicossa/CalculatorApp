# Change Proposal: Fix '=' Error on Calculator

## What
Fix a critical bug where pressing the `=` button on the calculator causes the application to error.

## Why
Users cannot complete calculations. The error could be client-side (JavaScript) or server-side (PageModel/CalculatorService). We will add targeted defensive validation and diagnostics-driven fixes.

## Scope
- Add robust client-side validation in the calculator submit handler
- Ensure hidden inputs are always populated with finite numeric values
- Add/adjust server-side error handling to avoid unhandled exceptions
- Verify logs / reproduction steps

Out of scope:
- Major UI redesign
- New calculator operations

## Success Criteria
- Pressing `=` never crashes the app
- Valid inputs return a result
- Invalid/incomplete inputs show a friendly error
- No unhandled exceptions in server logs
- No JS console errors on `=`
