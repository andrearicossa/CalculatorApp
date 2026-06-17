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
