# Propose: MutuoCalculator

## What
Aggiunta della pagina "Mutuo" all'applicazione SimpleWebCalculator, che consente la simulazione del piano di ammortamento di un mutuo con ammortamento alla francese.

## Why
Gli utenti (privati, consulenti, operatori) necessitano di uno strumento web integrato per simulare il piano di ammortamento di un mutuo senza strumenti esterni. La feature si integra nella navbar esistente seguendo le stesse convenzioni architetturali del Calculator.

## Scope
- Nuova Razor Page `Pages/Mutuo.cshtml` + `Pages/Mutuo.cshtml.cs`
- Nuovi `Services/IMutuoService.cs` + `Services/MutuoService.cs`
- Registrazione del servizio in `Program.cs`
- Aggiunta voce "Mutuo" in entrambi i layout navbar
- Form con select combo per la frequenza (mensile/trimestrale/semestrale/annuale)
- Tabella del piano con paginazione
- Validazione: tasso = 0 non supportato (errore informativo)
- Durata espressa in anni; n. rate = DurataAnni × freq_annua

## Out of scope
- Esportazione/stampa (MVP)
- Frequenza quindicinale
- Simulazioni avanzate o confronto scenari
