# Technical Specification: Simple Web Calculator

## Overview

**Feature ID**: FEAT-001  
**Feature Name**: SimpleWebCalculator  
**Technology Stack**: ASP.NET Core Razor Pages (.NET 10), Bootstrap 5, Vanilla JavaScript  
**Architecture**: MVC-inspired (PageModel + View), Service-oriented business logic

---

## System Architecture

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                        Presentation Layer                    │
│  ┌────────────────────────────────────────────────────────┐ │
│  │  Calculator.cshtml (Razor View)                        │ │
│  │  - HTML structure with Bootstrap                       │ │
│  │  - Form with keypad, display, operation buttons        │ │
│  │  - Client-side JavaScript for UX enhancement           │ │
│  └────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
                            │ ▲
                            │ │ HTTP POST/GET
                            ▼ │
┌─────────────────────────────────────────────────────────────┐
│                      Application Layer                       │
│  ┌────────────────────────────────────────────────────────┐ │
│  │  CalculatorModel : PageModel                           │ │
│  │  - OnGet(): Initialize form state                      │ │
│  │  - OnPost(): Validate, delegate, handle errors         │ │
│  │  - Properties: A, B, Operation, Result, ErrorMessage   │ │
│  └────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
                            │ ▲
                            │ │ DI (ICalculatorService)
                            ▼ │
┌─────────────────────────────────────────────────────────────┐
│                       Business Logic Layer                   │
│  ┌────────────────────────────────────────────────────────┐ │
│  │  CalculatorService : ICalculatorService                │ │
│  │  - Calculate(double a, double b, string operation)     │ │
│  │  - Validates range, executes operations, rounds result │ │
│  │  - Throws exceptions for invalid inputs/operations     │ │
│  └────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

---

## Component Specifications

### 1. ICalculatorService Interface

**File**: `Services/ICalculatorService.cs`  
**Namespace**: `Calcuator.Services` (or project namespace)

```csharp
namespace Calcuator.Services;

/// <summary>
/// Service for performing mathematical calculations.
/// </summary>
public interface ICalculatorService
{
    /// <summary>
    /// Calculates the result of a mathematical operation.
    /// </summary>
    /// <param name="a">First operand (must be in range ±10^15).</param>
    /// <param name="b">Second operand (must be in range ±10^15).</param>
    /// <param name="operation">Operation type: "add", "sub", "mul", "div".</param>
    /// <returns>Result rounded to 2 decimal places.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when operands or result exceed ±10^15.
    /// </exception>
    /// <exception cref="DivideByZeroException">
    /// Thrown when operation is "div" and b is zero.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when operation is not recognized.
    /// </exception>
    double Calculate(double a, double b, string operation);
}
```

---

### 2. CalculatorService Implementation

**File**: `Services/CalculatorService.cs`  
**Namespace**: `Calcuator.Services`

```csharp
namespace Calcuator.Services;

public class CalculatorService : ICalculatorService
{
    private const double MaxValue = 1e15;
    private const double MinValue = -1e15;
    private const int DecimalPlaces = 2;

    public double Calculate(double a, double b, string operation)
    {
        // Step 1: Validate input range
        ValidateRange(a, nameof(a));
        ValidateRange(b, nameof(b));

        // Step 2: Execute operation
        double result = operation switch
        {
            "add" => a + b,
            "sub" => a - b,
            "mul" => a * b,
            "div" => Divide(a, b),
            _ => throw new ArgumentException(
                $"Invalid operation: {operation}. " +
                $"Supported operations: add, sub, mul, div.",
                nameof(operation))
        };

        // Step 3: Round result
        result = Math.Round(result, DecimalPlaces);

        // Step 4: Validate result range
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
        if (double.IsInfinity(value) || double.IsNaN(value))
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                $"Value is not a valid number.");
        }

        if (value > MaxValue || value < MinValue)
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                $"Value must be between {MinValue} and {MaxValue}.");
        }
    }
}
```

**Design Decisions**:
- **Constants**: `MaxValue`, `MinValue`, `DecimalPlaces` for maintainability
- **Switch Expression**: Modern C# syntax for operation routing
- **Separate Divide Method**: Explicit division by zero handling
- **ValidateRange**: Reusable validation logic with clear error messages
- **Round Result**: Ensures consistent precision (BR-07)

**Exception Handling**:
| Exception Type | Trigger | Message |
|----------------|---------|---------|
| `ArgumentOutOfRangeException` | Operand > 10^15 or < -10^15 | "Value must be between -1E+15 and 1E+15." |
| `ArgumentOutOfRangeException` | Result > 10^15 or < -10^15 | Same as above |
| `DivideByZeroException` | Division with b = 0 | "Cannot divide by zero." |
| `ArgumentException` | Invalid operation string | "Invalid operation: {op}. Supported operations: add, sub, mul, div." |

---

### 3. Calculator PageModel

**File**: `Pages/Calculator.cshtml.cs`  
**Namespace**: `Calcuator.Pages`

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Calcuator.Services;

namespace Calcuator.Pages;

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
        // Initialize with default operation (BR-09)
        Operation = "add";
    }

    public IActionResult OnPost()
    {
        // Validate required fields
        if (!A.HasValue || !B.HasValue)
        {
            ErrorMessage = "Please enter both numbers.";
            return Page();
        }

        if (string.IsNullOrWhiteSpace(Operation))
        {
            ErrorMessage = "Please select an operation.";
            return Page();
        }

        try
        {
            // Delegate to service (BR-06)
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
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            // Catch-all for unexpected errors
            ErrorMessage = $"An unexpected error occurred: {ex.Message}";
        }

        return Page();
    }
}
```

**Design Decisions**:
- **Nullable Properties**: `A?` and `B?` allow detection of missing inputs
- **Default Operation**: `"add"` set in both property initializer and `OnGet` (defensive)
- **Explicit Validation**: Check nulls before calling service
- **Exception Mapping**: Each exception type maps to user-friendly error message
- **Return Page()**: Re-render same page with result or error (standard Razor Pages pattern)

---

### 4. Calculator Razor View

**File**: `Pages/Calculator.cshtml`

#### Page Directives

```cshtml
@page
@model Calcuator.Pages.CalculatorModel
@{
    ViewData["Title"] = "Calculator";
}
```

#### HTML Structure

```cshtml
<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <h2 class="text-center mb-4">Simple Calculator</h2>
            
            <form id="calculatorForm" method="post" class="calculator-form">
                <!-- Display Area -->
                <div class="mb-3">
                    <input id="currentDisplay" 
                           type="text" 
                           class="form-control calculator-display" 
                           readonly 
                           value="@(Model.Result?.ToString("F2") ?? "0")" />
                </div>

                <!-- Hidden Inputs for Form Binding -->
                <input id="aInput" type="hidden" asp-for="A" />
                <input id="bInput" type="hidden" asp-for="B" />
                <input id="operationInput" type="hidden" asp-for="Operation" />

                <!-- Error Message Display -->
                @if (!string.IsNullOrEmpty(Model.ErrorMessage))
                {
                    <div class="alert alert-danger" role="alert">
                        @Model.ErrorMessage
                    </div>
                }

                <!-- Keypad Grid (4x4) -->
                <div class="keypad">
                    <div class="row g-2">
                        <!-- Row 1: 7, 8, 9, / -->
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('7')">7</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('8')">8</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('9')">9</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-warning operation-button w-100" onclick="setOperation('div')">÷</button>
                        </div>

                        <!-- Row 2: 4, 5, 6, * -->
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('4')">4</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('5')">5</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('6')">6</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-warning operation-button w-100" onclick="setOperation('mul')">×</button>
                        </div>

                        <!-- Row 3: 1, 2, 3, - -->
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('1')">1</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('2')">2</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('3')">3</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-warning operation-button w-100" onclick="setOperation('sub')">−</button>
                        </div>

                        <!-- Row 4: 0, ., =, + -->
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('0')">0</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-secondary keypad-button w-100" onclick="appendToDisplay('.')">.</button>
                        </div>
                        <div class="col-3">
                            <button type="submit" class="btn btn-primary keypad-button w-100">=</button>
                        </div>
                        <div class="col-3">
                            <button type="button" class="btn btn-warning operation-button w-100" onclick="setOperation('add')">+</button>
                        </div>
                    </div>
                </div>

                <!-- Clear Button -->
                <div class="mt-3">
                    <button type="button" class="btn btn-danger w-100" onclick="clearForm()">Clear</button>
                </div>
            </form>
        </div>
    </div>
</div>
```

#### JavaScript Logic

```cshtml
@section Scripts {
<script>
    // State management
    let currentInput = '';
    let operandA = null;
    let operandB = null;
    let operation = '@Model.Operation';
    let isEnteringB = false;

    // Append digit or decimal point to current input
    function appendToDisplay(value) {
        // Prevent multiple decimal points
        if (value === '.' && currentInput.includes('.')) {
            return;
        }
        
        currentInput += value;
        updateDisplay();
    }

    // Set mathematical operation
    function setOperation(op) {
        if (currentInput !== '' && !isEnteringB) {
            // First operand entered, store it
            operandA = parseFloat(currentInput);
            currentInput = '';
            isEnteringB = true;
        }
        
        operation = op;
        document.getElementById('operationInput').value = op;
        highlightOperation(op);
    }

    // Calculate result (form submission handled by server)
    // Set hidden inputs before form submits
    document.getElementById('calculatorForm').addEventListener('submit', function(e) {
        if (currentInput !== '') {
            if (!isEnteringB) {
                // Only one operand entered, use it for both
                operandA = parseFloat(currentInput);
                operandB = parseFloat(currentInput);
            } else {
                // Second operand entered
                operandB = parseFloat(currentInput);
            }
        }
        
        if (operandA !== null && operandB !== null) {
            document.getElementById('aInput').value = operandA;
            document.getElementById('bInput').value = operandB;
        } else {
            // Prevent form submission if operands missing
            e.preventDefault();
            alert('Please enter both numbers.');
        }
    });

    // Clear form and reset state
    function clearForm() {
        currentInput = '';
        operandA = null;
        operandB = null;
        operation = 'add';
        isEnteringB = false;
        
        document.getElementById('currentDisplay').value = '0';
        document.getElementById('aInput').value = '';
        document.getElementById('bInput').value = '';
        document.getElementById('operationInput').value = 'add';
        
        // Clear error messages
        const errorAlert = document.querySelector('.alert-danger');
        if (errorAlert) {
            errorAlert.remove();
        }
        
        highlightOperation('add');
    }

    // Update display with current input
    function updateDisplay() {
        document.getElementById('currentDisplay').value = currentInput || '0';
    }

    // Highlight active operation button
    function highlightOperation(op) {
        document.querySelectorAll('.operation-button').forEach(btn => {
            btn.classList.remove('active');
        });
        // Add highlight logic if needed (e.g., add 'active' class)
    }

    // Initialize on page load
    window.addEventListener('DOMContentLoaded', function() {
        highlightOperation(operation);
        if (document.getElementById('currentDisplay').value === '') {
            document.getElementById('currentDisplay').value = '0';
        }
    });
</script>
}
```

**Design Decisions**:
- **State Management**: JavaScript variables track calculator state (operands, operation, entry mode)
- **Decimal Validation**: Prevent multiple dots in one number
- **Form Submission**: Hidden inputs populated on submit, server handles calculation
- **Clear Function**: Resets all state and UI elements
- **Operation Highlighting**: Visual feedback for selected operation (optional enhancement)

---

### 5. Custom CSS (Optional Enhancement)

**File**: `wwwroot/css/calculator.css` (or inline in Calculator.cshtml)

```css
.calculator-display {
    font-size: 2rem;
    text-align: right;
    background-color: #f8f9fa;
    border: 2px solid #343a40;
    padding: 15px;
    margin-bottom: 15px;
    font-family: 'Courier New', monospace;
}

.keypad-button {
    font-size: 1.5rem;
    padding: 20px;
    min-height: 60px;
}

.operation-button {
    background-color: #ff9800;
    border-color: #ff9800;
    color: white;
}

.operation-button:hover {
    background-color: #e68900;
    border-color: #e68900;
}

.operation-button.active {
    background-color: #cc7a00;
    border-color: #cc7a00;
}

.calculator-form {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
```

---

### 6. Dependency Injection Configuration

**File**: `Program.cs`

```csharp
using Calcuator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
```

**Design Decisions**:
- **Singleton Lifetime**: CalculatorService is stateless, safe for singleton
- **Razor Pages**: Standard configuration for Razor Pages application

---

## Data Models

### PageModel Properties

| Property | Type | Purpose | Binding |
|----------|------|---------|---------|
| A | double? | First operand | [BindProperty] |
| B | double? | Second operand | [BindProperty] |
| Operation | string | Operation type ("add", "sub", "mul", "div") | [BindProperty] |
| Result | double? | Calculation result | Output only |
| ErrorMessage | string? | Error message for display | Output only |

---

## API Contracts

### ICalculatorService.Calculate

**Method Signature**:
```csharp
double Calculate(double a, double b, string operation)
```

**Parameters**:
- `a`: First operand (range: -10^15 to 10^15)
- `b`: Second operand (range: -10^15 to 10^15)
- `operation`: Operation code ("add", "sub", "mul", "div")

**Returns**:
- `double`: Result rounded to 2 decimal places

**Exceptions**:
- `ArgumentOutOfRangeException`: Operand or result out of range
- `DivideByZeroException`: Division by zero attempted
- `ArgumentException`: Invalid operation code

**Example Usage**:
```csharp
var service = new CalculatorService();

// Success
var result = service.Calculate(10, 5, "add"); // Returns 15.00

// Division
var result2 = service.Calculate(10, 3, "div"); // Returns 3.33

// Error: Division by zero
var result3 = service.Calculate(10, 0, "div"); // Throws DivideByZeroException

// Error: Out of range
var result4 = service.Calculate(1e16, 1, "add"); // Throws ArgumentOutOfRangeException
```

---

## Business Rules Implementation

| Rule ID | Description | Implementation Location |
|---------|-------------|-------------------------|
| BR-01 | Addizione: A + B | CalculatorService.Calculate (switch case "add") |
| BR-02 | Sottrazione: A - B | CalculatorService.Calculate (switch case "sub") |
| BR-03 | Moltiplicazione: A * B | CalculatorService.Calculate (switch case "mul") |
| BR-04 | Divisione: A / B (if B != 0) | CalculatorService.Divide method |
| BR-05 | Double precision | C# double type (System.Double) |
| BR-06 | Server-side calculation | CalculatorService (PageModel delegates) |
| BR-07 | Round to 2 decimals | Math.Round(result, 2) in CalculatorService |
| BR-08 | Range validation ±10^15 | ValidateRange method in CalculatorService |
| BR-09 | Default operation: add | CalculatorModel.OnGet sets Operation = "add" |
| BR-10 | Decimal separator: dot | .NET double parsing (culture-invariant) |

---

## Validation Rules

### Client-Side Validation (JavaScript)
- Only digits (0-9) and one decimal point allowed
- Prevent multiple decimal points in single number
- Form submission requires both operands

### Server-Side Validation (PageModel + Service)
- A and B must not be null (PageModel)
- A and B must be in range ±10^15 (Service)
- Operation must be valid ("add", "sub", "mul", "div") (Service)
- B must not be zero for division (Service)
- Result must be in range ±10^15 (Service)

---

## Error Handling Matrix

| Error Scenario | Detection Layer | Exception Type | User Message |
|----------------|-----------------|----------------|--------------|
| Missing operand A or B | PageModel | None (null check) | "Please enter both numbers." |
| Invalid operation | Service | ArgumentException | "Invalid operation: {op}. Supported operations: add, sub, mul, div." |
| Division by zero | Service | DivideByZeroException | "Cannot divide by zero." |
| Operand out of range | Service | ArgumentOutOfRangeException | "Value must be between -1E+15 and 1E+15." |
| Result out of range | Service | ArgumentOutOfRangeException | "Value must be between -1E+15 and 1E+15." |
| Invalid characters | Client (JS) | None (prevented) | N/A (input blocked) |
| Unexpected error | PageModel (catch-all) | Exception | "An unexpected error occurred: {message}" |

---

## Testing Specifications

### Unit Tests (CalculatorService)

**Test Cases**:
```csharp
[Fact]
public void Calculate_Add_ReturnsSum()
{
    var service = new CalculatorService();
    var result = service.Calculate(10, 5, "add");
    Assert.Equal(15.00, result);
}

[Fact]
public void Calculate_Divide_RoundsToTwoDecimals()
{
    var service = new CalculatorService();
    var result = service.Calculate(10, 3, "div");
    Assert.Equal(3.33, result);
}

[Fact]
public void Calculate_DivideByZero_ThrowsException()
{
    var service = new CalculatorService();
    Assert.Throws<DivideByZeroException>(() => service.Calculate(10, 0, "div"));
}

[Fact]
public void Calculate_OutOfRange_ThrowsException()
{
    var service = new CalculatorService();
    Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(1e16, 1, "add"));
}
```

### Integration Tests (Calculator Page)

**Test Scenarios**:
1. POST with valid inputs returns result
2. POST with missing operands returns error
3. POST with division by zero returns error
4. POST with out of range value returns error

---

## Performance Specifications

- **Service Call Latency**: < 1ms (pure mathematical operations)
- **Page Load Time**: < 500ms (server-side rendering)
- **Memory Footprint**: Minimal (stateless service, no caching)
- **Concurrency**: Thread-safe (singleton service with no shared state)

---

## Security Specifications

- **Input Validation**: Server-side validation mandatory (never trust client)
- **CSRF Protection**: Razor Pages anti-forgery tokens enabled by default
- **XSS Protection**: Razor auto-escapes HTML output
- **SQL Injection**: N/A (no database)
- **Authentication**: Not required (public calculator)
- **Authorization**: Not required (public calculator)

---

## Browser Compatibility

**Supported Browsers**:
- Chrome 90+
- Edge 90+
- Firefox 88+
- Safari 14+

**Responsive Design**:
- Desktop: Full layout (keypad grid visible)
- Tablet: Scaled layout
- Mobile: Stacked layout (Bootstrap responsive grid)

---

## Deployment Requirements

**Runtime**:
- .NET 10 SDK
- ASP.NET Core Runtime 10.x

**Configuration**:
- No database connection
- No external APIs
- No environment-specific settings

**Static Files**:
- Bootstrap 5.3.x (CDN or local)
- Custom CSS (optional)

---

## Future Enhancements (Out of Scope)

- Scientific calculator functions (sin, cos, sqrt, pow)
- Calculation history (session or persistent)
- Keyboard input support (in addition to on-screen keypad)
- Themes (light/dark mode)
- Multi-language support
- Memory functions (M+, M-, MR, MC)
- Percentage calculations
- Export results (CSV, PDF)
- Unit tests (optional for MVP)

---

**Status**: Specification Complete  
**Version**: 1.0  
**Date**: 2026-06-04  
**Author**: AI Spec-Driven Pipeline
