# 📘 Analisi Funzionale
## Pagina di Benvenuto e Visualizzazione Pipeline

---

## 1. Obiettivo

Definire i requisiti funzionali della pagina iniziale dell’applicazione, con l’obiettivo di:

- Accogliere l’utente con una descrizione sintetica del sistema
- Fornire una rappresentazione visiva del processo di sviluppo e rilascio
- Comunicare in modo chiaro il flusso delle pipeline utilizzate

---

## 2. Ambito

La funzionalità riguarda la **pagina di default** dell’applicazione, accessibile al primo accesso dell’utente.

Questa pagina ha finalità informative e introduttive.

---

## 3. Attori

- Utente generico
- Utente interno (team di sviluppo, stakeholder)

---

## 4. Descrizione Funzionale

La pagina di benvenuto deve essere composta da due sezioni principali:

### 4.1 Sezione Descrittiva

Questa sezione deve contenere:

- Titolo dell’applicazione
- Breve descrizione delle finalità
- Indicazione delle principali funzionalità offerte

**Obiettivo:**
Fornire un’introduzione chiara e immediata all’utilizzo dell’applicazione.

---

### 4.2 Sezione Visualizzazione Pipeline

Questa sezione deve presentare un elemento grafico che rappresenti il processo di sviluppo e rilascio dell’applicazione.

#### Contenuto previsto:

- Diagramma delle pipeline utilizzate
- Rappresentazione delle fasi principali
- Suddivisione tra almeno due pipeline distinte

#### Ogni fase deve includere:

- Nome dello step
- Breve descrizione del suo scopo

---

## 5. Struttura del Diagramma

Il diagramma deve rappresentare un flusso sequenziale delle attività.

### Esempio logico delle fasi:

#### Pipeline di Build

- Recupero del codice
- Compilazione
- Esecuzione test
- Generazione artefatti

#### Pipeline di Deploy

- Distribuzione
- Configurazione ambiente
- Verifica finale

---

## 6. Requisiti Funzionali

- La pagina deve essere visibile all’apertura dell’applicazione
- Le informazioni devono essere leggibili e comprensibili
- Il diagramma deve rendere evidente il flusso delle operazioni
- Ogni fase deve essere accompagnata da una breve descrizione

---

## 7. Requisiti di Qualità

La soluzione deve garantire:

- Chiarezza espositiva
- Immediatezza nella comprensione
- Coerenza visiva tra testo e diagramma
- Navigazione semplice

---

## 8. Comportamento del Sistema

- All’accesso, l’utente visualizza la pagina di benvenuto
- Il sistema presenta il contenuto informativo
- Il diagramma delle pipeline viene mostrato come supporto visivo

---

## 9. Vincoli

- La rappresentazione grafica deve essere semplice e intuitiva
