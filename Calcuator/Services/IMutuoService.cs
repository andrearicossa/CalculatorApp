using Calcuator.Models;

namespace Calcuator.Services;

public interface IMutuoService
{
    PianoAmmortamento CalcolaPiano(decimal importo, decimal tassoAnnuoPerc, int durataAnni, string frequenza, DateOnly dataInizio);
}
