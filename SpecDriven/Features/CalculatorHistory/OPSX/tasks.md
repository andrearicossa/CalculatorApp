# Tasks: Calculator History

1. Enable Session in `Calcuator/Program.cs`.
2. Add History entry model (record/class) and session JSON helpers.
3. Update `CalculatorModel.OnPost` to append entry on success.
4. Implement clear-history handler invoked by Clear.
5. Update `Pages/Calculator.cshtml`:
   - Add History button
   - Add Bootstrap modal
   - Render history list
6. Build and manual tests:
   - Perform calculations, open History, verify entries
   - Close modal returns to calculator
   - Clear empties history
