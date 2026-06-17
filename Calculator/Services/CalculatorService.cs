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
