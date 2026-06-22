# Coding Rules – SIMEST Core Banking

## Naming
- Usare nomi chiari e significativi
- Evitare:
  - parole generiche (data, info)
  - nomi non pronunciabili
- Funzioni verb-based:
  - getValue, isValid, calculateAmount

## Code Quality
- No codice commentato
- Commentare logiche complesse
- Evitare hardcoding → usare costanti/config

## Testing
- Obbligatorio generare unit test
- Pattern AAA:
  - Arrange, Act, Assert
- Naming:
  Metodo_Scenario_RisultatoAtteso
- Framework:
  - xUnit/NUnit
  - Moq

## Database
- Script versionati su Git
- Idempotenza obbligatoria:
  - CREATE: check esistenza
  - ALTER: check presenza
  - INSERT: evitare duplicati

## SQL Naming
- createTable_<nome>
- alterTable_<nome>
- insert_<nome>
- Prefisso ordine:
  - 001_, 002_

## Struttura SQL
- Separare:
  - DDL (schema)
  - DML (dati)

## Sicurezza
- Azure AD / Entra ID
- OAuth2 / OpenID
- MSAL
- Secrets su Azure Key Vault, mai su file di configurazione se non in sviluppo