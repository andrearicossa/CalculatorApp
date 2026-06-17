# Change Proposal: Calculator Navigation

## What

Integrare la pagina Calculator nel menu di navigazione principale dell'applicazione e impostarla come homepage predefinita con le seguenti caratteristiche:

- **Menu di navigazione aggiornato** con tre voci: Calculator (prima), Home, Privacy
- **Calculator come homepage**: Redirect automatico da "/" a "/Calculator"
- **Evidenziazione voce attiva**: Highlight visivo della pagina corrente nel menu
- **Menu responsive**: Navbar Bootstrap con hamburger toggler su mobile (breakpoint lg)
- **Pagina Home informativa**: HomeController.Index diventa pagina di presentazione app
- **Layout condiviso**: Implementazione in Pages/Shared/_Layout.cshtml (o Views/Shared se necessario)

## Why

**Obiettivo di business**: Migliorare l'esperienza utente rendendo la funzionalità principale (Calculator) immediatamente accessibile e facilmente raggiungibile da qualsiasi punto dell'applicazione.

**Valore per l'utente**:
- **Accesso diretto**: Landing immediata sulla calcolatrice senza click aggiuntivi
- **Navigazione chiara**: Menu sempre visibile con indicazione posizione corrente
- **Coerenza UX**: Struttura di navigazione standard e familiare
- **Mobile-friendly**: Menu accessibile e funzionante su tutti i dispositivi

**Problemi risolti**:
- Attualmente Calculator è accessibile solo tramite URL diretto (/Calculator)
- Nessun menu di navigazione per tornare a Calculator da altre pagine
- Utente arriva su Index (Home) che non è la funzionalità principale
- Manca orientamento visivo su quale pagina si trova l'utente

## Scope

**In scope:**
- Aggiunta voce "Calculator" al menu navbar (prima posizione)
- Configurazione redirect "/" → "/Calculator" in Program.cs
- Implementazione helper method per evidenziare voce menu attiva
- Verifica/aggiornamento layout condiviso (Pages/Shared o Views/Shared)
- Configurazione navbar responsive con hamburger toggler
- Aggiornamento pagina Home (Index) come pagina informativa

**Out of scope (future iterations):**
- Aggiunta icone alle voci di menu
- Breadcrumb navigation
- Footer navigation
- Sitemap
- Localizzazione menu (italiano/inglese)
- Menu dropdown o mega menu
- Animazioni transizioni pagina

## Success Criteria

1. ✅ Utente accede a "/" e viene reindirizzato automaticamente a "/Calculator"
2. ✅ Menu visibile su tutte le pagine con voci: Calculator, Home, Privacy
3. ✅ Voce "Calculator" evidenziata quando utente è su pagina Calculator
4. ✅ Voce "Home" evidenziata quando utente è su pagina Index
5. ✅ Voce "Privacy" evidenziata quando utente è su pagina Privacy
6. ✅ Click su voce menu naviga correttamente alla pagina corrispondente
7. ✅ Su mobile (< 992px): hamburger menu visibile e funzionante
8. ✅ Click hamburger apre/chiude menu collapse
9. ✅ Pagina Home mostra informazioni app + link a Calculator
10. ✅ Navigation funziona con JavaScript disabilitato (link HTML standard)

## Risks & Unknowns

**Risks:**
- **R1**: Layout condiviso potrebbe esistere in due posizioni (Views/Shared e Pages/Shared); serve verifica path corretto (Mitigazione: verificare entrambi, priorità a Pages/Shared per Razor Pages)
- **R2**: Helper method per active state potrebbe non funzionare correttamente se mix Razor Pages e Controller (Mitigazione: gestire entrambi i casi con check su "page" e "action" in ViewContext)
- **R3**: Bootstrap versione installata potrebbe non supportare data-bs-* attributes (v4 vs v5) (Mitigazione: verificare versione Bootstrap, adattare syntax se v4)

**Unknowns:**
- **U1**: Contenuto attuale pagina Home/Index (potrebbe essere vuota o avere contenuto da preservare)
- **U2**: Esistenza di altre pagine/route non documentate che potrebbero necessitare voce menu
- **U3**: Preferenza utente finale su naming ("Calculator" vs "Calcolatrice" se app in italiano)

## Open Questions

- OQ-01: La pagina Home deve avere contenuto specifico o un semplice "Benvenuto" + link Calculator?
- OQ-02: Serve un pulsante "Torna alla Calcolatrice" prominente nella pagina Home oltre alla voce menu?
- OQ-03: Il nome "Home" va bene o preferisci "About", "Info", o "Informazioni"?
- OQ-04: Serve gestire il caso in cui utente clicca su voce menu già attiva? (es. scroll to top)

## Dependencies

- **Bootstrap 5.x**: Per navbar responsive e collapse (già presente nel progetto)
- **ASP.NET Core Razor Pages**: Framework target (già presente)
- **Layout condiviso esistente**: Da modificare (Pages/Shared/_Layout.cshtml o Views/Shared/_Layout.cshtml)
- **HomeController**: Da mantenere e aggiornare contenuto Index view

## Estimated Effort

- **Design & Planning**: 30 min (layout analysis, bootstrap verification)
- **Implementation**: 2-3 hours
  - Layout navbar update: 1-1.5h (menu + helper method + responsive)
  - Program.cs redirect: 15 min
  - Home page content update: 30-45 min
- **Testing**: 1 hour (desktop + mobile, navigation flows, active states)
- **TOTAL**: ~3.5-4.5 hours

## Related Changes

- Dipende da: FEAT-001 (SimpleWebCalculator) — deve essere implementata
- Blocca: Future navigation enhancements (breadcrumb, footer nav)

---

**Status**: Proposto  
**Created**: 2026-06-04  
**Feature**: FEAT-002 - CalculatorNavigation
