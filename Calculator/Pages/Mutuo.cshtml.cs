using System.ComponentModel.DataAnnotations;
using Calcuator.Models;
using Calcuator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calcuator.Pages;

public class MutuoModel : PageModel
{
    private readonly IMutuoService _mutuoService;

    public MutuoModel(IMutuoService mutuoService)
    {
        _mutuoService = mutuoService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Inserire l'importo del mutuo.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "L'importo deve essere maggiore di zero.")]
    public decimal? ImportoMutuo { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Inserire il tasso di interesse.")]
    [Range(0.01, 99.99, ErrorMessage = "Il tasso di interesse deve essere strettamente positivo e inferiore a 100.")]
    public decimal? TassoInteresse { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Inserire la durata in anni.")]
    [Range(1, 100, ErrorMessage = "La durata deve essere un valore intero positivo.")]
    public int? DurataAnni { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Selezionare la frequenza delle rate.")]
    public string FrequenzaRate { get; set; } = "mensile";

    [BindProperty]
    [Required(ErrorMessage = "Inserire la data di inizio.")]
    public DateOnly? DataInizio { get; set; }

    [BindProperty]
    public int PageIndex { get; set; } = 0;

    public const int PageSize = 20;

    public PianoAmmortamento? Piano { get; private set; }
    public IReadOnlyList<RataDettaglio> RighePagina { get; private set; } = [];
    public int TotalPages { get; private set; }
    public string? ErrorMessage { get; private set; }

    public IReadOnlyList<(string Value, string Label)> FrequenzeDisponibili { get; } =
    [
        ("mensile",      "Mensile"),
        ("trimestrale",  "Trimestrale"),
        ("semestrale",   "Semestrale"),
        ("annuale",      "Annuale")
    ];

    public void OnGet()
    {
        DataInizio = DateOnly.FromDateTime(DateTime.Today);
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            Piano = _mutuoService.CalcolaPiano(
                ImportoMutuo!.Value,
                TassoInteresse!.Value,
                DurataAnni!.Value,
                FrequenzaRate,
                DataInizio!.Value);

            ApplicaPaginazione();
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Errore inatteso: {ex.Message}";
        }

        return Page();
    }

    private void ApplicaPaginazione()
    {
        if (Piano is null) return;

        int totalRighe = Piano.Righe.Count;
        TotalPages = (int)Math.Ceiling(totalRighe / (double)PageSize);

        if (PageIndex < 0) PageIndex = 0;
        if (PageIndex >= TotalPages) PageIndex = TotalPages - 1;

        RighePagina = Piano.Righe
            .Skip(PageIndex * PageSize)
            .Take(PageSize)
            .ToList();
    }
}
