namespace Calcuator.Models;

public class PianoAmmortamento
{
    public decimal Rata { get; set; }
    public decimal TotaleInteressi { get; set; }
    public decimal TotaleCapitale { get; set; }
    public decimal TotaleComplessivo { get; set; }
    public List<RataDettaglio> Righe { get; set; } = [];
}
