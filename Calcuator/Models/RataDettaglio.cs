namespace Calcuator.Models;

public class RataDettaglio
{
    public int NumeroRata { get; set; }
    public DateOnly Data { get; set; }
    public decimal ImportoRata { get; set; }
    public decimal QuotaCapitale { get; set; }
    public decimal QuotaInteressi { get; set; }
    public decimal CapitaleResiduo { get; set; }
}
