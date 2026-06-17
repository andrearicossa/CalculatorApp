# Design: Simple Web Calculator

## Architecture Overview

```
┌────────────────────────────────────────────────────────────────┐
│                    Browser (Client-Side)                        │
├────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Calculator.cshtml (Razor View)                                │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │  • Display (read-only input/div)                         │  │
│  │  • Numeric Keypad (buttons 0-9, .)                      │  │
│  │  • Operation Buttons (+, -, *, /)                       │  │
│  │  • Calculate Button (submits form)                       │  │
│  │  • Clear Button (resets form)                           │  │
│  │  • Error Message Display (@Model.ErrorMessage)           │  │
│  └──────────────────────────────────────────────────────────┘  │
│                           │                                      │
│                           │ JavaScript (client-side UX)          │
│                           │ - Keypad click → append to display   │
│                           │ - Input validation (digits, dot)     │
│                           │                                      │
│                           │ POST /Calculator                     │
│                           ▼                                      │
└────────────────────────────────────────────────────────────────┘
                            │
                            │
┌────────────────────────────────────────────────────────────────┐
│                   Server (ASP.NET Core)                         │
├────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Pages/Calculator.cshtml.cs (PageModel)                        │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │  CalculatorModel : PageModel                             │  │
│  │                                                           │  │
│  │  [BindProperty] A: double?                               │  │
│  │  [BindProperty] B: double?                               │  │
│  │  [BindProperty] Operation: string                        │  │
│  │  Result: double?                                         │  │
│  │  ErrorMessage: string                                    │  │
│  │                                                           │  │
│  │  OnGet()                                                 │  │
│  │    → Initialize: Operation = "add"                       │  │
│  │                                                           │  │
│  │  OnPost()                                                │  │
│  │    → Validate ModelState                                 │  │
│  │    → Validate A, B not null                              │  │
│  │    → Call _calculatorService.Calculate(A, B, Operation)  │  │
│  │    → Handle exceptions → set ErrorMessage                │  │
│  │    → Set Result                                          │  │
│  │    → Return Page()                                       │  │
│  └──────────────────────────────────────────────────────────┘  │
│                           │                                      │
│                           │ DI (Dependency Injection)            │
│                           ▼                                      │
│  Services/CalculatorService.cs (Business Logic)                │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │  interface ICalculatorService                            │  │
│  │    double Calculate(double a, double b, string operation)│  │
│  │                                                           │  │
│  │  class CalculatorService : ICalculatorService            │  │
│  │                                                           │  │
│  │  Calculate(double a, double b, string operation)         │  │
│  │    1. Validate range: ±10^15                             │  │
│  │       → throw ArgumentOutOfRangeException                │  │
│  │    2. Validate operation: add, sub, mul, div             │  │
│  │       → throw ArgumentException                          │  │
│  │    3. Execute operation:                                 │  │
│  │       - add  → a + b                                     │  │
│  │       - sub  → a - b                                     │  │
│  │       - mul  → a * b                                     │  │
│  │       - div  → if b == 0: throw DivideByZeroException    │  │
│  │                else: a / b                               │  │
│  │    4. Round result to 2 decimals: Math.Round(result, 2)  │  │
│  │    5. Validate result range: ±10^15                      │  │
│  │    6. Return result                                      │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
│  Program.cs (Configuration)                                    │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │  builder.Services.AddRazorPages();                       │  │
│  │  builder.Services.AddSingleton<ICalculatorService,       │  │
│  │                                 CalculatorService>();    │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
└────────────────────────────────────────────────────────────────┘
```

## Component Breakdown

### 1. Calculator Page (Razor Page)

**File**: `Pages/Calculator.cshtml`

**Responsibilities**:
- Render calculator UI with Bootstrap styling
- Display area for numbers and results
- Numeric keypad (0-9, decimal point)
- Operation buttons (+, -, *, /)
- Calculate button (form submit)
- Clear button (reset form)
- Error message display

**Key Elements**:
```html
<form method="post">
  <!-- Display -->
  <input asp-for="Result" readonly class="form-control display" />
  
  <!-- Hidden inputs for A, B -->
  <input asp-for="A" type="hidden" />
  <input asp-for="B" type="hidden" />
  
  <!-- Operation selection (radio buttons or hidden with JS) -->
  <input asp-for="Operation" type="hidden" />
  
  <!-- Keypad (grid 4x4 buttons) -->
  <div class="keypad">
    <button type="button" data-value="7">7</button>
    <button type="button" data-value="8">8</button>
    ...
  </div>
  
  <!-- Operation buttons -->
  <button type="button" data-operation="add">+</button>
  ...
  
  <!-- Calculate -->
  <button type="submit">Calculate</button>
  
  <!-- Clear -->
  <button type="button" onclick="clearForm()">Clear</button>
  
  <!-- Error -->
  <div class="text-danger">@Model.ErrorMessage</div>
</form>

<script>
  // JavaScript for keypad interaction
  // - Append digits to display
  // - Set A, B, Operation hidden fields
  // - Validate input (only digits and dot)
</script>
```

**UI Layout** (Bootstrap grid):
```
┌─────────────────────────────────┐
│         [Display/Result]         │ (readonly input, large font)
├─────────────────────────────────┤
│   7   │   8   │   9   │   /    │
│   4   │   5   │   6   │   *    │
│   1   │   2   │   3   │   -    │
│   0   │   .   │   =   │   +    │ (= is Calculate button)
├─────────────────────────────────┤
│      [Clear]                     │
└─────────────────────────────────┘
```

---

### 2. Calculator PageModel

**File**: `Pages/Calculator.cshtml.cs`

**Responsibilities**:
- Bind form data (A, B, Operation)
- Initialize default operation (add) on GET
- Validate input on POST
- Delegate calculation to CalculatorService
- Handle exceptions and set error messages
- Return result to view

**Code Structure**:
```csharp
public class CalculatorModel : PageModel
{
    private readonly ICalculatorService _calculatorService;
    
    public CalculatorModel(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }
    
    [BindProperty]
    public double? A { get; set; }
    
    [BindProperty]
    public double? B { get; set; }
    
    [BindProperty]
    public string Operation { get; set; } = "add";
    
    public double? Result { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public void OnGet()
    {
        // Initialize with default operation
        Operation = "add";
    }
    
    public IActionResult OnPost()
    {
        // Validate
        if (!A.HasValue || !B.HasValue)
        {
            ErrorMessage = "Please enter both numbers.";
            return Page();
        }
        
        try
        {
            // Delegate to service
            Result = _calculatorService.Calculate(A.Value, B.Value, Operation);
        }
        catch (DivideByZeroException)
        {
            ErrorMessage = "Cannot divide by zero.";
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        
        return Page();
    }
}
```

**Validation Rules**:
- A and B must have values (not null)
- ModelState validation (automatic via data annotations if added)
- Exception handling for business rule violations

---

### 3. Calculator Service

**File**: `Services/ICalculatorService.cs` + `Services/CalculatorService.cs`

**Responsibilities**:
- Validate input range (±10^15)
- Validate operation type
- Execute mathematical operations
- Handle division by zero
- Round result to 2 decimals
- Validate result range

**Interface**:
```csharp
public interface ICalculatorService
{
    double Calculate(double a, double b, string operation);
}
```

**Implementation**:
```csharp
public class CalculatorService : ICalculatorService
{
    private const double MaxValue = 1e15;
    private const double MinValue = -1e15;
    
    public double Calculate(double a, double b, string operation)
    {
        // 1. Validate input range
        ValidateRange(a, nameof(a));
        ValidateRange(b, nameof(b));
        
        // 2. Calculate based on operation
        double result = operation switch
        {
            "add" => a + b,
            "sub" => a - b,
            "mul" => a * b,
            "div" => Divide(a, b),
            _ => throw new ArgumentException($"Invalid operation: {operation}")
        };
        
        // 3. Round to 2 decimals
        result = Math.Round(result, 2);
        
        // 4. Validate result range
        ValidateRange(result, "result");
        
        return result;
    }
    
    private double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return a / b;
    }
    
    private void ValidateRange(double value, string paramName)
    {
        if (value > MaxValue || value < MinValue)
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                $"Value must be between {MinValue} and {MaxValue}."
            );
        }
    }
}
```

**Business Rules Implemented**:
- BR-01 to BR-04: Operations
- BR-05: Double precision
- BR-06: Server-side calculation
- BR-07: Rounding to 2 decimals
- BR-08: Range validation ±10^15
- BR-10: Decimal separator (handled by .NET double parsing)

---

### 4. Dependency Injection Configuration

**File**: `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

var app = builder.Build();

// Configure pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
```

---

## Data Flow

### Scenario: Successful Calculation (10 + 5)

1. **User loads page** (GET /Calculator)
   - `OnGet()` initializes `Operation = "add"`
   - View renders with empty display, keypad, operations

2. **User interacts with UI**
   - Clicks "1", "0" → JavaScript appends to display, sets `A = 10`
   - Clicks "+" → JavaScript sets `Operation = "add"`
   - Clicks "5" → JavaScript appends to display, sets `B = 5`
   - Clicks "=" (Calculate button) → Form submits (POST)

3. **Server processes POST**
   - `OnPost()` receives: `A=10, B=5, Operation="add"`
   - Validates: both values present ✓
   - Calls `_calculatorService.Calculate(10, 5, "add")`
   
4. **CalculatorService executes**
   - Validates range: 10 ✓, 5 ✓
   - Executes: `10 + 5 = 15`
   - Rounds: `15.00`
   - Validates result range: 15 ✓
   - Returns: `15.00`

5. **PageModel returns result**
   - Sets `Result = 15.00`
   - Returns `Page()`

6. **View renders**
   - Display shows: `15.00`
   - Form maintains A, B, Operation values
   - No error message

---

### Scenario: Error - Division by Zero

1. User enters: `A=10, B=0, Operation="div"`
2. Clicks Calculate
3. `OnPost()` calls `_calculatorService.Calculate(10, 0, "div")`
4. Service validates range ✓
5. Service calls `Divide(10, 0)`
6. `Divide` throws `DivideByZeroException`
7. `OnPost()` catches exception
8. Sets `ErrorMessage = "Cannot divide by zero."`
9. Returns `Page()`
10. View renders error message, form unchanged

---

### Scenario: Error - Out of Range

1. User enters: `A=1e16, B=1, Operation="add"`
2. Clicks Calculate
3. `OnPost()` calls `_calculatorService.Calculate(1e16, 1, "add")`
4. Service validates range: `1e16 > 1e15` → throws `ArgumentOutOfRangeException`
5. `OnPost()` catches exception
6. Sets `ErrorMessage = "Value must be between -1E+15 and 1E+15."`
7. Returns `Page()`
8. View renders error message

---

## Client-Side JavaScript

**Purpose**: Enhance UX by handling keypad interaction without full page reload for input.

**Functions**:
```javascript
let currentInput = '';
let operation = 'add';
let operandA = null;
let operandB = null;
let isEnteringB = false;

// Append digit or dot to display
function appendToDisplay(value) {
    if (value === '.' && currentInput.includes('.')) return; // Only one dot
    currentInput += value;
    updateDisplay();
}

// Set operation
function setOperation(op) {
    if (!isEnteringB && currentInput !== '') {
        operandA = parseFloat(currentInput);
        currentInput = '';
        isEnteringB = true;
    }
    operation = op;
    document.getElementById('operationInput').value = op;
    highlightOperation(op);
}

// Calculate (submit form)
function calculate() {
    if (currentInput !== '' && !isEnteringB) {
        operandA = parseFloat(currentInput);
    } else if (currentInput !== '' && isEnteringB) {
        operandB = parseFloat(currentInput);
    }
    
    // Set hidden inputs
    document.getElementById('aInput').value = operandA;
    document.getElementById('bInput').value = operandB;
    
    // Submit form
    document.getElementById('calculatorForm').submit();
}

// Clear form
function clearForm() {
    currentInput = '';
    operandA = null;
    operandB = null;
    isEnteringB = false;
    operation = 'add';
    updateDisplay();
    document.getElementById('resultDisplay').value = '';
    document.getElementById('errorMessage').innerText = '';
}

// Update display
function updateDisplay() {
    document.getElementById('currentDisplay').value = currentInput;
}
```

**Note**: This JavaScript is optional for MVP. The form can work without it using standard HTML inputs, but it improves UX.

---

## Error Handling Strategy

| Error Type | Validation Layer | Handling |
|------------|------------------|----------|
| Missing A or B | PageModel (OnPost) | ErrorMessage, return Page() |
| Invalid operation | CalculatorService | ArgumentException → ErrorMessage |
| Division by zero | CalculatorService | DivideByZeroException → ErrorMessage |
| Out of range input | CalculatorService | ArgumentOutOfRangeException → ErrorMessage |
| Out of range result | CalculatorService | ArgumentOutOfRangeException → ErrorMessage |
| Invalid characters | Client (JavaScript keypad) | Prevent input |
| Invalid format (paste) | Model binding | ModelState.IsValid → ErrorMessage |

---

## Bootstrap Styling

**CSS Classes** (Bootstrap 5):
- `.form-control` for inputs
- `.btn btn-primary` for Calculate button
- `.btn btn-secondary` for keypad buttons
- `.btn btn-warning` for Clear button
- `.text-danger` for error messages
- `.container`, `.row`, `.col` for layout grid

**Custom CSS** (optional):
```css
.calculator-display {
    font-size: 2rem;
    text-align: right;
    background-color: #f0f0f0;
    border: 2px solid #333;
    padding: 10px;
    margin-bottom: 10px;
}

.keypad-button {
    width: 60px;
    height: 60px;
    font-size: 1.5rem;
    margin: 5px;
}

.operation-button {
    background-color: #ffa500;
    color: white;
}

.operation-button.active {
    background-color: #ff6600;
}
```

---

## Testing Strategy

### Manual Testing Checklist

1. ✅ Page loads with empty display and keypad
2. ✅ Default operation is addition (highlighted)
3. ✅ Keypad buttons append digits to display
4. ✅ Only digits and one dot allowed in display
5. ✅ Operation buttons change selection
6. ✅ Calculate performs correct operation:
   - Addition: 10 + 5 = 15.00 ✓
   - Subtraction: 10 - 5 = 5.00 ✓
   - Multiplication: 10 * 5 = 50.00 ✓
   - Division: 10 / 5 = 2.00 ✓
7. ✅ Division by zero shows error
8. ✅ Out of range values show error
9. ✅ Result rounded to 2 decimals (e.g., 10 / 3 = 3.33)
10. ✅ Clear button resets form
11. ✅ Error messages displayed clearly

### Automated Testing (Future)

- **Unit tests for CalculatorService**:
  - Test each operation (add, sub, mul, div)
  - Test division by zero exception
  - Test range validation
  - Test rounding logic

- **Integration tests for Calculator Page**:
  - Test OnPost with valid inputs
  - Test OnPost with invalid inputs
  - Test error handling

---

## Security Considerations

- **Input Validation**: Server-side validation mandatory (never trust client)
- **No SQL Injection risk**: No database interaction
- **No XSS risk**: Razor auto-escapes output
- **CSRF Protection**: Razor Pages includes anti-forgery tokens by default
- **No sensitive data**: Calculator doesn't handle PII or financial data

---

## Performance Considerations

- **Service lifetime**: Singleton (no state, thread-safe for simple math operations)
- **Calculation performance**: O(1) for all operations
- **Memory**: Minimal (no data persistence, no caching needed)
- **Network**: Standard HTTP POST, negligible payload

---

## Accessibility

- **Keyboard navigation**: Ensure all buttons are keyboard-accessible
- **Screen reader**: Use `aria-label` for keypad buttons
- **Focus management**: Clear visual focus indicators
- **Color contrast**: Ensure WCAG AA compliance

---

## Future Enhancements (Out of Scope)

- Scientific calculator mode (sin, cos, sqrt, pow)
- Calculation history (session-based or persistent)
- Keyboard input support (in addition to on-screen keypad)
- Themes (light/dark mode)
- Export results (CSV, PDF)
- Multi-language support
- Advanced operations (percentages, memory functions)

---

**Status**: Design Complete  
**Ready for Implementation**: Yes  
**Next Steps**: Proceed to Tasks for implementation breakdown
