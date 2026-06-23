# Architecture Context – SIMEST Core Banking

## Overview
Sistema Core Banking basato su architettura modulare:
- Servizi applicativi .NET
- Database relazionali (Oracle / SQL Server)
- Integrazione tramite API

## Principles
- Modularizzazione tramite librerie (NuGet)
- Comunicazione tra componenti via API (no accesso diretto DB tra moduli)
- Separazione ambienti: sviluppo, collaudo, produzione
- Versionamento completo (codice + DB + pipeline)

## Development Lifecycle
- Git branching:
  - feature/<id>_<descrizione>
  - bugfix/<id>_<descrizione>
  - release/<version>
- Workflow:
  - sviluppo → collaudo → release/cdp → master
  - CI/CD con deploy automatico in sviluppo e manuale in collaudo

## Architecture Decisions
- Unit test obbligatori per ogni feature
- Componenti riusabili distribuiti via NuGet
- Comunicazione tra servizi via API

## Non-Functional Requirements
- Alta affidabilità
- Tracciabilità (Jira + pipeline)
- Test obbligatori prima del rilascio