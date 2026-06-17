namespace Calcuator.Models;

public record CalculatorHistoryEntry(
    double A,
    string Operation,
    double B,
    double Result,
    DateTimeOffset CreatedAt
);
