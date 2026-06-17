using Calcuator.Models;

namespace Calcuator.Services;

public class MutuoService : IMutuoService
{
    private static readonly Dictionary<string, int> FrequenzaMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "mensile",      12 },
        { "trimestrale",   4 },
        { "semestrale",    2 },
        { "annuale",       1 }
    };

    public PianoAmmortamento CalcolaPiano(decimal importo, decimal tassoAnnuoPerc, int durataAnni, string frequenza, DateOnly dataInizio)
    {
        if (importo <= 0)
            throw new ArgumentException("L'importo del mutuo deve essere maggiore di zero.");

        if (tassoAnnuoPerc <= 0)
            throw new ArgumentException("Il tasso di interesse deve essere maggiore di zero.");

        if (durataAnni <= 0)
            throw new ArgumentException("La durata deve essere un valore intero positivo.");

        if (!FrequenzaMap.TryGetValue(frequenza, out int periodi))
            throw new ArgumentException("Frequenza non valida. Valori ammessi: mensile, trimestrale, semestrale, annuale.");

        int n = durataAnni * periodi;
        decimal i = tassoAnnuoPerc / 100m / periodi;

        decimal rata = importo * (i / (1m - (decimal)Math.Pow((double)(1m + i), -n)));
        rata = Math.Round(rata, 2);

        var righe = new List<RataDettaglio>(n);
        decimal capitaleResiduo = importo;
        decimal totaleInteressi = 0m;

        for (int k = 1; k <= n; k++)
        {
            decimal quotaInteressi = Math.Round(capitaleResiduo * i, 2);
            decimal quotaCapitale = rata - quotaInteressi;

            if (k == n)
            {
                quotaCapitale = capitaleResiduo;
                rata = quotaCapitale + quotaInteressi;
            }

            capitaleResiduo = Math.Round(capitaleResiduo - quotaCapitale, 2);
            totaleInteressi += quotaInteressi;

            righe.Add(new RataDettaglio
            {
                NumeroRata = k,
                Data = AvanzaData(dataInizio, periodi, k),
                ImportoRata = Math.Round(quotaCapitale + quotaInteressi, 2),
                QuotaCapitale = Math.Round(quotaCapitale, 2),
                QuotaInteressi = quotaInteressi,
                CapitaleResiduo = capitaleResiduo < 0 ? 0m : capitaleResiduo
            });
        }

        decimal totaleCapitale = righe.Sum(r => r.QuotaCapitale);
        totaleInteressi = righe.Sum(r => r.QuotaInteressi);

        return new PianoAmmortamento
        {
            Rata = Math.Round(righe[0].ImportoRata, 2),
            TotaleInteressi = Math.Round(totaleInteressi, 2),
            TotaleCapitale = Math.Round(totaleCapitale, 2),
            TotaleComplessivo = Math.Round(totaleCapitale + totaleInteressi, 2),
            Righe = righe
        };
    }

    private static DateOnly AvanzaData(DateOnly dataInizio, int periodi, int k)
    {
        int mesi = (12 / periodi) * k;
        return dataInizio.AddMonths(mesi);
    }
}
