# Change Proposal: Simple Web Calculator

## What

Implementare una web application basata su ASP.NET Core Razor Pages (.NET 10) che fornisca un'interfaccia di calcolatrice web con le seguenti caratteristiche:

- **Interfaccia simile a calcolatrice standard**: display per visualizzare numeri e risultati, tastierino numerico (0-9, punto decimale), pulsanti per operazioni (+, -, *, /)
- **Operazioni matematiche base**: addizione, sottrazione, moltiplicazione, divisione
- **Validazione robusta**: range numerico ±10^15, blocco input non validi, gestione divisione per zero e overflow/underflow
- **Precisione controllata**: risultati arrotondati a 2 cifre decimali
- **Elaborazione server-side**: logica di calcolo delegata a un servizio dedicato (CalculatorService)
- **UX migliorata**: operazione di default (addizione), tastierino che previene input invalidi

## Why

**Obiettivo di business**: Fornire uno strumento accessibile via browser per calcoli matematici elementari con un'esperienza utente intuitiva e robusta gestione degli errori.

**Valore per l'utente**:
- Interfaccia familiare (simile a calcolatrice fisica)
- Prevenzione errori tramite tastierino controllato
- Risultati leggibili (2 decimali) e affidabili (validazione range)
- Nessuna installazione richiesta (web-based)

**Requisiti tecnici**:
- Architettura Razor Pages (fit perfetto per form-based interaction)
- Separazione business logic dal PageModel (testabilità e manutenibilità)
- Validazione multi-livello (client + server)
- Bootstrap per UI responsiva

## Scope

**In scope:**
- Pagina Razor Calculator con form e tastierino
- CalculatorService per operazioni matematiche
- Validazione input (range, formato, division by zero)
- Gestione errori con messaggi chiari
- Arrotondamento risultati a 2 decimali
- UI con Bootstrap

**Out of scope (future iterations):**
- Storico calcoli
- Operazioni avanzate (potenze, radici, trigonometria)
- Modalità scientifica
- Localizzazione multi-lingua
- Persistenza dati
- Autenticazione utente
- Test automatici (da valutare)

## Success Criteria

1. ✅ Utente può accedere alla pagina e vedere calcolatrice con display vuoto e tastierino
2. ✅ Utente può inserire numeri tramite tastierino (solo cifre 0-9 e punto decimale consentiti)
3. ✅ Utente può selezionare un'operazione (addizione pre-selezionata di default)
4. ✅ Utente può cliccare "Calcola" e vedere risultato arrotondato a 2 decimali
5. ✅ Sistema blocca input fuori range (±10^15) con messaggio errore chiaro
6. ✅ Sistema gestisce divisione per zero con messaggio errore specifico
7. ✅ Sistema previene overflow/underflow con validazione e messaggio errore
8. ✅ Logica di calcolo risiede in CalculatorService separato dal PageModel
9. ✅ UI è responsiva e funziona su desktop/mobile tramite Bootstrap

## Risks & Unknowns

**Risks:**
- **R1**: Comportamento tastierino JavaScript potrebbe essere bypassato (mitigazione: validazione server-side obbligatoria)
- **R2**: Arrotondamento a 2 decimali potrebbe non essere sufficiente per alcuni calcoli (mitigazione: documentato come requisito funzionale, estendibile in futuro)
- **R3**: Range ±10^15 potrebbe essere troppo restrittivo/permissivo (mitigazione: valore configurabile nel service)

**Unknowns:**
- **U1**: Layout tastierino: griglia 3x4 o layout lineare? (decisione: griglia 4x4 come calcolatrice standard)
- **U2**: Pulsante Clear necessario? (decisione: sì, utile per reset form)
- **U3**: Gestire operazioni in sequenza (risultato diventa operando A per nuovo calcolo)? (decisione: no per MVP, form resetta dopo ogni calcolo)

## Open Questions

- OQ-01: Serve logging delle operazioni per debugging? → da valutare post-MVP
- OQ-02: Test automatici obbligatori o opzionali? → da valutare
- OQ-07: Serve pulsante Clear esplicito? → sì, aggiungere a tasks
- OQ-08: Storico calcoli in sessione? → no per MVP

## Dependencies

- **.NET 10 SDK**: già presente nel workspace
- **ASP.NET Core Razor Pages**: framework target
- **Bootstrap 5.x**: per UI (CDN o locale)
- **Nessuna dipendenza esterna**: calcoli matematici con Math standard library

## Estimated Effort

- **Design**: 1 hour (UI mockup, architecture decision)
- **Implementation**: 4-6 hours
  - CalculatorService: 1-2h
  - Calculator Razor Page (PageModel + View): 2-3h
  - Tastierino UI + validation: 1-2h
- **Testing manuale**: 1 hour
- **TOTAL**: ~6-8 hours per un developer esperto in Razor Pages

## Related Changes

- Nessun change correlato (feature standalone)

---

**Status**: Proposto  
**Created**: 2026-06-04  
**Feature**: FEAT-001 - SimpleWebCalculator
