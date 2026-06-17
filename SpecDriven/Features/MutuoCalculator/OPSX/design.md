# Design: MutuoCalculator

## Architettura

Seguire il pattern esistente del Calculator:

```
Pages/Mutuo.cshtml               ← Razor Page (form + risultati)
Pages/Mutuo.cshtml.cs            ← MutuoModel : PageModel
Services/IMutuoService.cs        ← interfaccia servizio
Services/MutuoService.cs         ← implementazione calcolo
Program.cs                       ← registrazione servizio
```

---

## Models (interni alla Razor Page / servizio)

### MutuoInput
```
ImportoMutuo   decimal   required, > 0
TassoInteresse decimal   required, > 0 e < 100  (tasso = 0 → errore validazione)
DurataAnni     int       required, > 0  (anni interi)
FrequenzaRate  string    required, valori: "mensile" | "trimestrale" | "semestrale" | "annuale"
DataInizio     DateOnly  required, data valida
```

### RataDettaglio
```
NumeroRata      int
Data            DateOnly
ImportoRata     decimal
QuotaCapitale   decimal
QuotaInteressi  decimal
CapitaleResiduo decimal
```

### PianoAmmortamento
```
Rata               decimal
TotaleInteressi    decimal
TotaleCapitale     decimal
TotaleComplessivo  decimal
Righe              List<RataDettaglio>
```

---

## IMutuoService

```csharp
PianoAmmortamento CalcolaPiano(decimal importo, decimal tassoAnnuoPerc, int durataAnni, string frequenza, DateOnly dataInizio);
```

---

## MutuoService — logica di calcolo

### Mappatura frequenza → periodi/anno
```
mensile       → 12
trimestrale   →  4
semestrale    →  2
annuale       →  1
```

### Formula ammortamento alla francese
```
n = durataAnni × periodiPerAnno
i = (tassoAnnuoPerc / 100) / periodiPerAnno
R = importo × (i / (1 − (1+i)^−n))
```

### Generazione righe piano
Per ogni rata k = 1..n:
```
QuotaInteressi  = capitaleResiduo × i
QuotaCapitale   = R − QuotaInteressi
CapitaleResiduo = capitaleResiduo − QuotaCapitale
Data            = dataInizio + k × periodo
```
Ultima rata: arrotondamento per portare CapitaleResiduo a 0.
Tutti i valori monetari: round a 2 decimali.

---

## MutuoModel (PageModel)

### Proprietà bind
```
[BindProperty] MutuoInput Input
PageSize   int = 20       (righe per pagina)
PageIndex  int = 0        (pagina corrente, 0-based)
PianoAmmortamento? Piano
string? ErrorMessage
```

### OnGet
- Inizializza Input con valori default (FrequenzaRate = "mensile")
- Nessun piano visualizzato

### OnPost
1. Validazione: ImportoMutuo > 0, TassoInteresse > 0, DurataAnni > 0, DataInizio valida
2. Se TassoInteresse = 0 → ErrorMessage = "Il tasso di interesse deve essere maggiore di zero." → return Page()
3. Calcola con IMutuoService → assegna a Piano
4. Gestione eccezioni → ErrorMessage

### Paginazione (lato server)
```
TotalPages  = Math.Ceiling(Piano.Righe.Count / PageSize)
RighePagina = Piano.Righe.Skip(PageIndex × PageSize).Take(PageSize)
```
PageIndex gestito tramite query string o hidden field nel form.

---

## Razor Page — struttura UI

### Form
- `<select asp-for="Input.FrequenzaRate">` con 4 opzioni fisse
- Input numerici per ImportoMutuo, TassoInteresse, DurataAnni
- `<input type="date">` per DataInizio
- Pulsante "Calcola"

### Riepilogo (visibile se Piano != null)
- Rata, TotaleInteressi, TotaleCapitale, TotaleComplessivo

### Tabella piano paginata (visibile se Piano != null)
- Colonne: N°, Data, Rata, Quota Capitale, Quota Interessi, Capitale Residuo
- Paginazione: link/pulsanti Precedente / Pagina X di Y / Successivo
- Numero righe per pagina: 20 (default)

---

## Navbar

Aggiungere in entrambi i layout dopo la voce "Calculator":
```html
<li class="nav-item">
    <a asp-page="/Mutuo" class="nav-link text-dark @GetActiveClass("/Mutuo")">Mutuo</a>
</li>
```

---

## Program.cs

```csharp
builder.Services.AddSingleton<IMutuoService, MutuoService>();
```
