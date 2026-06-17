# Spec: MutuoCalculator

## Acceptance Criteria

### AC-01 — Form con combo frequenza
- Il campo frequenza è un `<select>` con esattamente 4 opzioni: mensile, trimestrale, semestrale, annuale
- Il valore selezionato è correttamente passato al servizio

### AC-02 — Tabella paginata
- La tabella mostra al massimo 20 righe per pagina
- Sono presenti i controlli di navigazione (Precedente, Successivo, indice pagina corrente)
- Per piani ≤ 20 rate non vengono mostrati i controlli di paginazione

### AC-03 — Tasso = 0 non supportato
- Se l'utente inserisce TassoInteresse = 0, il sistema non esegue il calcolo
- Viene mostrato il messaggio: "Il tasso di interesse deve essere maggiore di zero."

### AC-04 — Durata in anni
- Il campo DurataAnni accetta un intero positivo (anni)
- Il numero totale di rate = DurataAnni × periodiPerAnno (freq)
- Esempio: 10 anni mensile → 120 rate

### AC-05 — Formula e coerenza totali
- La rata è calcolata con ammortamento alla francese
- TotaleCapitale = ImportoMutuo (a meno di arrotondamento)
- TotaleComplessivo = TotaleCapitale + TotaleInteressi
- CapitaleResiduo ultima rata = 0 (o valore irrilevante per arrotondamento)

### AC-06 — Validazione campi
- Tutti i campi obbligatori: messaggio di errore se mancanti
- ImportoMutuo > 0, TassoInteresse > 0, DurataAnni > 0

### AC-07 — Navigazione
- Voce "Mutuo" presente nella navbar in entrambi i layout
- Il link porta correttamente alla pagina `/Mutuo`
