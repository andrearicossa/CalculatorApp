# Change Proposal: Calculator UI Fix

## What

Correggere bug critici nella pagina Calculator che impediscono il corretto funzionamento dell'interfaccia utente:

**Problemi Attuali**:
1. ⚠️ **Stili Bootstrap non applicati** - interfaccia "spartana" senza formattazione
2. ⚠️ **Tastierino numerico non funzionante** - click sui pulsanti non aggiorna il display
3. ⚠️ **Errore al submit del form** - click su "=" genera errore sistema

**Soluzione**:
- Creare `Pages/Shared/_Layout.cshtml` per Razor Pages con Bootstrap locale
- Creare `Pages/_ViewStart.cshtml` per applicare layout automaticamente
- Copiare navbar da Views layout per coerenza UI
- Supportare @section Scripts e @section Styles

## Why

**Causa Radice**: Calculator è una Razor Page (Pages/Calculator.cshtml) ma non esiste un layout dedicato per Razor Pages. Il progetto ha solo `Views/Shared/_Layout.cshtml` che viene usato da MVC Views, non da Razor Pages.

**Impatto Attuale**:
- Calculator viene renderizzata SENZA layout → no Bootstrap CSS/JS
- JavaScript del tastierino in @section Scripts non viene caricato → funzioni non definite
- UX completamente compromessa, calcolatrice inutilizzabile

**Valore della Fix**:
- **Ripristina funzionalità**: Calculator torna utilizzabile
- **Coerenza UI**: Stesso aspetto di Home e Privacy
- **Architettura corretta**: Separazione pulita Razor Pages vs MVC
- **Zero breaking changes**: Non impatta pagine esistenti

## Scope

**In scope:**
- Creare `Pages/Shared/_Layout.cshtml` copiando da `Views/Shared/_Layout.cshtml`
- Creare `Pages/_ViewStart.cshtml` con configurazione layout
- Verificare che Calculator.cshtml funzioni correttamente con nuovo layout
- Test manuale funzionalità (stili, tastierino, calcolo)

**Out of scope:**
- Modifiche a Calculator.cshtml (già corretto, serve solo il layout)
- Modifiche a CalculatorService (business logic funziona)
- Modifiche a Views/Shared/_Layout.cshtml (già aggiornato con navbar FEAT-002)
- Altre Razor Pages (se esistono, beneficeranno automaticamente)

## Success Criteria

1. ✅ Pages/Shared/_Layout.cshtml esiste e include Bootstrap locale
2. ✅ Pages/_ViewStart.cshtml esiste e specifica Layout = "_Layout"
3. ✅ Navigando a /Calculator: stili Bootstrap applicati correttamente
4. ✅ Tastierino numerico funzionante: click su cifre aggiorna display
5. ✅ Operazioni funzionanti: click su +,-,*,/ memorizza operazione
6. ✅ Calcolo funzionante: click su "=" esegue calcolo e mostra risultato
7. ✅ Navbar visibile e funzionante (Calculator, Home, Privacy)
8. ✅ Nessun errore console JavaScript
9. ✅ Nessun errore server-side

## Risks & Unknowns

**Risks:**
- **R1**: Possibile esistenza di Pages/Shared/_Layout.cshtml parziale → Mitigation: Verifica prima di creare, sovrascrivi se necessario
- **R2**: Conflitto _ViewStart esistente con configurazioni custom → Mitigation: Verifica contenuto, integra se necessario
- **R3**: Altre Razor Pages nel progetto potrebbero essere impattate → Mitigation: Test anche altre pagine Razor se esistono (es. Privacy)

**Unknowns:**
- **U1**: Esistono altre Razor Pages oltre a Calculator? (probabile solo Calculator data l'architettura mista)
- **U2**: Bootstrap versione esatta installata? (verificabile da file package se necessario)

## Open Questions

- OQ-01: Serve documentare questa fix in FEAT-001 (SimpleWebCalculator) spec/design?
- OQ-02: Serve aggiungere automated test per verificare layout loading? (out of scope MVP)

## Dependencies

- ✅ Bootstrap 5.x presente in wwwroot/lib/bootstrap (verificato)
- ✅ Views/Shared/_Layout.cshtml aggiornato con navbar (FEAT-002, verificato)
- ✅ Calculator.cshtml con @section Scripts (già presente e corretto)
- ❌ Pages/Shared/ directory NON esiste (da creare)
- ❌ Pages/_ViewStart.cshtml NON esiste (da creare)

## Estimated Effort

- **Analysis & Verification**: DONE (già completato in FEAT-003 structuring/refinement)
- **Implementation**: 30-45 minutes
  - Create Pages/Shared directory: 1 min
  - Copy & verify layout: 15-20 min
  - Create _ViewStart: 2 min
  - Manual testing: 10-15 min
  - Documentation: 5 min
- **TOTAL**: ~45 minutes

## Related Changes

- **Depends on**: FEAT-001 (SimpleWebCalculator) - deve essere implementata
- **Depends on**: FEAT-002 (CalculatorNavigation) - navbar deve essere aggiornato
- **Fixes**: Bug critico introdotto architetturalmente (Razor Page senza layout)

---

**Status**: Proposto  
**Priority**: CRITICAL (Calculator non funzionante)  
**Created**: 2026-06-04  
**Feature**: FEAT-003 - CalculatorUIFix
