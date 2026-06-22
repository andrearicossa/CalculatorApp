# Patterns & Best Practices – SIMEST Core Banking

## Application Patterns
- Service-based architecture
- Separation of concerns (API / Service / Data)
- Dependency Injection
- Modular libraries (NuGet)

## API Design
- Esporre sempre funzionalità via API
- Contratti chiari (input/output)
- Validazione input

## Testing Patterns
- AAA pattern (Arrange, Act, Assert)
- Test isolati (uso di Moq)
- Naming descrittivo dei test

## Database Patterns
- Script idempotenti
- Versionamento degli script
- Separazione schema/dati
- Ordinamento esecuzione script

## Anti-Patterns
- Hardcoded values
- Accesso diretto al DB tra componenti
- Codice duplicato
- Naming non significativo
- Mancanza di test

## Copilot Behavior
Quando generi codice:
- Rispetta naming convention
- Genera unit test automaticamente
- Usa pattern modulari
- Evita duplicazioni
- Produci codice pronto per produzione