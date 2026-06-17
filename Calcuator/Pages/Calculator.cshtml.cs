using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Calcuator.Models;
using Calcuator.Services;

namespace Calcuator.Pages;

public class CalculatorModel : PageModel
{
    private const string HistorySessionKey = "CalculatorHistory";
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

    public IReadOnlyList<CalculatorHistoryEntry> History { get; private set; } = [];

    public void OnGet()
    {
        // Initialize with default operation (BR-09)
        Operation = "add";
        LoadHistory();
    }

    public IActionResult OnPost()
    {
        // Validate required fields
        if (!A.HasValue || !B.HasValue)
        {
            ErrorMessage = "Please enter both numbers.";
            LoadHistory();
            return Page();
        }

        if (string.IsNullOrWhiteSpace(Operation))
        {
            ErrorMessage = "Please select an operation.";
            LoadHistory();
            return Page();
        }

        try
        {
            // Delegate to service (BR-06)
            Result = _calculatorService.Calculate(A.Value, B.Value, Operation);

            AppendHistory(A.Value, Operation, B.Value, Result.Value);
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

        LoadHistory();

        return Page();
    }

    public IActionResult OnPostClearHistory()
    {
        //HttpContext.Session.Remove(HistorySessionKey);
        HttpContext.Session.Clear();
        LoadHistory();
        return Page();
    }

    private void LoadHistory()
    {
        History = HttpContext.Session.GetJson<List<CalculatorHistoryEntry>>(HistorySessionKey) ?? [];
    }

    private void AppendHistory(double a, string operation, double b, double result)
    {
        var history = HttpContext.Session.GetJson<List<CalculatorHistoryEntry>>(HistorySessionKey) ?? [];
        history.Add(new CalculatorHistoryEntry(a, operation, b, result, DateTimeOffset.UtcNow));
        HttpContext.Session.SetJson(HistorySessionKey, history);
    }
}
