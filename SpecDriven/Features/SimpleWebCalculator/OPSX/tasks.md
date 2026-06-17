# Implementation Tasks: Simple Web Calculator

## Task Breakdown

### Phase 1: Service Layer (Business Logic)

#### TASK-01: Create ICalculatorService Interface
**Priority**: High  
**Estimated Time**: 15 minutes

**Steps**:
1. Create `Services/ICalculatorService.cs`
2. Define interface:
   ```csharp
   public interface ICalculatorService
   {
       double Calculate(double a, double b, string operation);
   }
   ```

**Acceptance Criteria**:
- Interface file exists in Services folder
- Single method signature defined
- Compiles without errors

---

#### TASK-02: Implement CalculatorService
**Priority**: High  
**Estimated Time**: 1-2 hours  
**Dependencies**: TASK-01

**Steps**:
1. Create `Services/CalculatorService.cs`
2. Implement `ICalculatorService` interface
3. Add constants for range validation:
   ```csharp
   private const double MaxValue = 1e15;
   private const double MinValue = -1e15;
   ```
4. Implement `Calculate` method:
   - Validate input range for A and B
   - Execute operation using switch expression
   - Handle division by zero
   - Round result to 2 decimals
   - Validate result range
5. Implement private helper methods:
   - `ValidateRange(double value, string paramName)`
   - `Divide(double a, double b)`

**Acceptance Criteria**:
- All four operations work correctly (add, sub, mul, div)
- Division by zero throws `DivideByZeroException`
- Out of range inputs throw `ArgumentOutOfRangeException`
- Results rounded to 2 decimal places
- Out of range results throw `ArgumentOutOfRangeException`

**Test Cases**:
```
10 + 5 = 15.00 ✓
10 - 5 = 5.00 ✓
10 * 5 = 50.00 ✓
10 / 5 = 2.00 ✓
10 / 3 = 3.33 ✓ (rounded)
10 / 0 = DivideByZeroException ✓
1e16 + 1 = ArgumentOutOfRangeException ✓
```

---

#### TASK-03: Register Service in DI Container
**Priority**: High  
**Estimated Time**: 10 minutes  
**Dependencies**: TASK-02

**Steps**:
1. Open `Program.cs`
2. Add service registration:
   ```csharp
   builder.Services.AddSingleton<ICalculatorService, CalculatorService>();
   ```

**Acceptance Criteria**:
- Service registered as Singleton
- Application builds successfully
- DI container can resolve `ICalculatorService`

---

### Phase 2: Page Model (Server-Side Logic)

#### TASK-04: Create Calculator PageModel
**Priority**: High  
**Estimated Time**: 1 hour  
**Dependencies**: TASK-03

**Steps**:
1. Create `Pages/Calculator.cshtml.cs`
2. Define `CalculatorModel : PageModel`
3. Inject `ICalculatorService` via constructor
4. Add properties:
   ```csharp
   [BindProperty] public double? A { get; set; }
   [BindProperty] public double? B { get; set; }
   [BindProperty] public string Operation { get; set; } = "add";
   public double? Result { get; set; }
   public string? ErrorMessage { get; set; }
   ```
5. Implement `OnGet()`:
   - Initialize `Operation = "add"`
6. Implement `OnPost()`:
   - Validate A and B are not null
   - Call service with try-catch
   - Handle exceptions and set error messages
   - Set Result
   - Return Page()

**Acceptance Criteria**:
- PageModel compiles without errors
- Constructor injection works
- OnGet initializes default operation
- OnPost validates inputs
- OnPost delegates calculation to service
- OnPost handles all exception types with appropriate error messages
- Page re-renders with result or error

**Test Scenarios**:
- Valid calculation returns result
- Missing A or B shows error
- Division by zero shows specific error
- Out of range value shows range error

---

### Phase 3: Razor View (UI)

#### TASK-05: Create Calculator Razor Page (Basic Structure)
**Priority**: High  
**Estimated Time**: 1 hour  
**Dependencies**: TASK-04

**Steps**:
1. Create `Pages/Calculator.cshtml`
2. Add `@page` directive
3. Add `@model CalculatorModel` directive
4. Create form with `method="post"`
5. Add basic Bootstrap layout structure (container, row, col)
6. Add result display (readonly input or div)
7. Add hidden inputs for A, B, Operation
8. Add Calculate button (submit)
9. Add error message display (@Model.ErrorMessage)

**Acceptance Criteria**:
- Page renders without errors
- Form structure correct
- Bootstrap classes applied
- Model binding works (A, B, Operation)
- Error messages display when present
- Result displays when present

---

#### TASK-06: Add Numeric Keypad UI
**Priority**: High  
**Estimated Time**: 1 hour  
**Dependencies**: TASK-05

**Steps**:
1. Add keypad grid (4x4 buttons using Bootstrap)
2. Create buttons for digits 0-9
3. Add decimal point button
4. Add operation buttons (+, -, *, /)
5. Add Clear button
6. Style buttons with Bootstrap classes
7. Add `data-value` attributes for JavaScript handling

**Acceptance Criteria**:
- Keypad displays in 4x4 grid layout
- All digits (0-9) visible as buttons
- Decimal point button included
- Operation buttons (+, -, *, /) included
- Clear button visible
- Layout responsive (mobile-friendly)

**Layout**:
```
┌───┬───┬───┬───┐
│ 7 │ 8 │ 9 │ / │
├───┼───┼───┼───┤
│ 4 │ 5 │ 6 │ * │
├───┼───┼───┼───┤
│ 1 │ 2 │ 3 │ - │
├───┼───┼───┼───┤
│ 0 │ . │ = │ + │
└───┴───┴───┴───┘
```

---

#### TASK-07: Add Client-Side JavaScript (Keypad Logic)
**Priority**: Medium  
**Estimated Time**: 1-2 hours  
**Dependencies**: TASK-06

**Steps**:
1. Add `<script>` section to Calculator.cshtml
2. Implement state management:
   ```javascript
   let currentInput = '';
   let operandA = null;
   let operandB = null;
   let operation = 'add';
   let isEnteringB = false;
   ```
3. Implement `appendToDisplay(value)`:
   - Append digit or dot to currentInput
   - Prevent multiple dots
   - Update display
4. Implement `setOperation(op)`:
   - Store operandA when first entered
   - Set operation
   - Set isEnteringB flag
   - Update hidden input
5. Implement `calculate()`:
   - Set operandB from currentInput
   - Populate hidden inputs (A, B, Operation)
   - Submit form
6. Implement `clearForm()`:
   - Reset all variables
   - Clear display
   - Clear error messages
7. Attach event listeners to keypad buttons
8. Implement `updateDisplay()` to show currentInput

**Acceptance Criteria**:
- Clicking keypad buttons appends values to display
- Only one decimal point allowed
- Operation buttons set the operation and prepare for second operand
- Calculate button (=) submits form with correct A, B, Operation values
- Clear button resets form completely
- Display updates in real-time as user clicks buttons

**Edge Cases to Handle**:
- Multiple clicks on decimal point (only one allowed)
- Operation change before entering second operand (override operation)
- Calculate without entering operandB (show error or use operandA twice?)

---

#### TASK-08: Add Bootstrap Styling and Custom CSS
**Priority**: Medium  
**Estimated Time**: 30 minutes  
**Dependencies**: TASK-06

**Steps**:
1. Link Bootstrap 5 CDN in layout or page
2. Apply Bootstrap classes:
   - `.container` for page wrapper
   - `.form-control` for display input
   - `.btn btn-secondary` for keypad buttons
   - `.btn btn-warning` for operation buttons
   - `.btn btn-primary` for Calculate button
   - `.btn btn-danger` for Clear button
   - `.text-danger` for error message
3. Add custom CSS (optional):
   - Display styling (large font, right-aligned)
   - Keypad button sizing (uniform 60x60px)
   - Operation button highlighting

**Acceptance Criteria**:
- Page uses Bootstrap for responsive layout
- Buttons styled consistently
- Display area visually distinct
- Error messages clearly visible
- Mobile-responsive (grid collapses appropriately)

---

### Phase 4: Testing and Refinement

#### TASK-09: Manual Testing (Functional)
**Priority**: High  
**Estimated Time**: 1 hour  
**Dependencies**: TASK-07, TASK-08

**Test Scenarios**:
1. **Basic Operations**:
   - Test 10 + 5 = 15.00 ✓
   - Test 10 - 5 = 5.00 ✓
   - Test 10 * 5 = 50.00 ✓
   - Test 10 / 5 = 2.00 ✓

2. **Rounding**:
   - Test 10 / 3 = 3.33 ✓

3. **Error Handling**:
   - Test division by zero (10 / 0) → error message ✓
   - Test out of range (1e16 + 1) → error message ✓
   - Test missing operands → error message ✓

4. **UI Interaction**:
   - Test keypad input
   - Test operation selection
   - Test Clear button
   - Test multiple calculations in sequence

5. **Edge Cases**:
   - Multiple decimal points → prevented ✓
   - Very small numbers (1e-10) → allowed if in range ✓
   - Negative numbers → supported via subtraction ✓

**Acceptance Criteria**:
- All test scenarios pass
- No console errors (browser dev tools)
- No server errors (check logs)
- UI behaves as expected

---

#### TASK-10: Accessibility Improvements
**Priority**: Low  
**Estimated Time**: 30 minutes  
**Dependencies**: TASK-08

**Steps**:
1. Add `aria-label` to keypad buttons
2. Add `role="button"` if necessary
3. Ensure keyboard navigation works (tab order)
4. Add focus indicators (CSS `:focus`)
5. Test with screen reader (optional)

**Acceptance Criteria**:
- All buttons have descriptive aria-labels
- Keyboard navigation functional (tab, enter)
- Focus indicators visible
- WCAG AA color contrast met

---

#### TASK-11: Code Review and Refactoring
**Priority**: Medium  
**Estimated Time**: 30 minutes  
**Dependencies**: TASK-09

**Steps**:
1. Review CalculatorService for code quality
2. Review PageModel for best practices
3. Review JavaScript for potential bugs
4. Add XML documentation comments to service methods
5. Ensure naming conventions followed
6. Check for magic numbers (use constants)

**Acceptance Criteria**:
- Code follows C# conventions
- No code smells (e.g., magic numbers, long methods)
- XML comments added to public methods
- JavaScript is clean and well-commented

---

### Phase 5: Documentation and Deployment Prep

#### TASK-12: Add README or Documentation
**Priority**: Low  
**Estimated Time**: 20 minutes  
**Dependencies**: TASK-11

**Steps**:
1. Create `README.md` in project root (if not exists)
2. Document:
   - Project purpose
   - How to run locally
   - Features implemented
   - Known limitations
   - Future enhancements

**Acceptance Criteria**:
- README exists with clear instructions
- Features documented
- Run instructions accurate

---

#### TASK-13: Final Build and Smoke Test
**Priority**: High  
**Estimated Time**: 15 minutes  
**Dependencies**: All previous tasks

**Steps**:
1. Clean solution
2. Rebuild solution
3. Run application locally
4. Perform quick smoke test:
   - Load page ✓
   - Perform one calculation ✓
   - Trigger one error ✓
5. Check for build warnings
6. Verify no console errors

**Acceptance Criteria**:
- Application builds without errors
- Application runs without crashes
- Basic functionality works end-to-end
- No critical warnings

---

## Task Summary

| Phase | Tasks | Estimated Time |
|-------|-------|----------------|
| Phase 1: Service Layer | TASK-01 to TASK-03 | 2-2.5 hours |
| Phase 2: Page Model | TASK-04 | 1 hour |
| Phase 3: Razor View | TASK-05 to TASK-08 | 3.5-4.5 hours |
| Phase 4: Testing | TASK-09 to TASK-11 | 2 hours |
| Phase 5: Documentation | TASK-12 to TASK-13 | 35 minutes |
| **TOTAL** | **13 tasks** | **~9-10 hours** |

---

## Critical Path

```
TASK-01 (Interface)
   ↓
TASK-02 (Service Implementation)
   ↓
TASK-03 (DI Registration)
   ↓
TASK-04 (PageModel)
   ↓
TASK-05 (Basic Razor Page)
   ↓
TASK-06 (Keypad UI)
   ↓
TASK-07 (JavaScript Logic) ← Most complex task
   ↓
TASK-08 (Styling)
   ↓
TASK-09 (Manual Testing)
   ↓
TASK-11 (Code Review)
   ↓
TASK-13 (Final Build)
```

**Parallel Work Opportunities**:
- TASK-10 (Accessibility) can be done alongside TASK-09
- TASK-12 (Documentation) can be done anytime after TASK-05

---

## Risk Mitigation

| Risk | Mitigation |
|------|------------|
| JavaScript keypad complexity | Start with simple implementation, iterate |
| Form state management | Test thoroughly with different input sequences |
| Bootstrap compatibility | Use stable Bootstrap 5.3.x version |
| DI configuration errors | Verify registration early (TASK-03) |
| Cross-browser issues | Test in Chrome, Edge, Firefox |

---

## Definition of Done

- [ ] All tasks completed
- [ ] All acceptance criteria met
- [ ] Manual testing passed
- [ ] Code reviewed
- [ ] No build errors or warnings
- [ ] Documentation updated
- [ ] Application runs locally without issues

---

**Status**: Ready for Implementation  
**Next Step**: Start with TASK-01 (Create ICalculatorService Interface)
