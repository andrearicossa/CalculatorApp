# ANALISI FUNZIONALE STRUTTURATA

### 📌 Feature

**ID:** FEAT-011  
**Name:** HomePageCalculatorApp

---

### 🧭 Business Context

**Description:**  
Pagina di default dell'applicazione Calculator App, accessibile al primo accesso dell'utente. Ha finalità informative e introduttive: accoglie l'utente, descrive le funzionalità del sistema e visualizza in modo comprensibile il processo di sviluppo e rilascio tramite un diagramma delle pipeline.

**Goal:**
- Accogliere l'utente con una presentazione sintetica del sistema e delle sue funzionalità
- Fornire una rappresentazione visiva chiara del processo di sviluppo e rilascio (pipeline)
- Comunicare in modo immediato il flusso operativo attraverso almeno due pipeline distinte (Build e Deploy)

---

### 👥 Actors

- **ACT-01:** Utente generico — accede all'applicazione per la prima volta o dalla home
- **ACT-02:** Utente interno — membro del team di sviluppo o stakeholder che consulta le informazioni di processo

---

### 🔎 Functional View

All'accesso dell'applicazione, l'utente viene indirizzato automaticamente alla pagina di benvenuto. La pagina si articola in due sezioni principali: una sezione descrittiva che introduce l'applicazione (titolo, finalità, funzionalità principali) e una sezione visiva che mostra il diagramma delle pipeline di sviluppo e rilascio. Il diagramma deve essere suddiviso in almeno due pipeline (Build e Deploy), ognuna composta da step sequenziali ciascuno con nome e descrizione. L'intero contenuto è di natura informativa, non interattiva.

---

## 🧱 DOMAIN MODEL

### 🧩 Entities

- **ENT-01 — Pagina di Benvenuto**
  - **Description:** Pagina principale dell'applicazione, visualizzata all'accesso. Contiene le due sezioni informative.
  - **Key Attributes:**
    - Titolo applicazione
    - Descrizione finalità
    - Lista funzionalità principali
  - **Related User Stories:** US-01

- **ENT-02 — Pipeline**
  - **Description:** Rappresentazione di un processo di sviluppo o rilascio suddiviso in fasi sequenziali. Ogni pipeline ha un nome e un tipo (Build / Deploy).
  - **Key Attributes:**
    - Nome pipeline
    - Tipo (Build | Deploy)
    - Lista di Step
  - **Related User Stories:** US-02

- **ENT-03 — Step Pipeline**
  - **Description:** Singola fase di una pipeline. Ha un nome identificativo e una breve descrizione del proprio scopo.
  - **Key Attributes:**
    - Nome step
    - Descrizione scopo
    - Ordine sequenziale
  - **Related User Stories:** US-02, US-03

- **ENT-04 — Diagramma Pipeline**
  - **Description:** Elemento grafico/visivo che rappresenta in modo sequenziale le pipeline e i loro step. Visualizzato nella sezione apposita della pagina.
  - **Key Attributes:**
    - Elenco pipeline da rappresentare
    - Modalità di visualizzazione (sequenziale)
  - **Related User Stories:** US-02, US-03

---

### 🔗 Relationships

- **REL-01 — Pagina di Benvenuto → Diagramma Pipeline**
  - **Description:** La pagina di benvenuto contiene il diagramma delle pipeline come sezione autonoma

- **REL-02 — Diagramma Pipeline → Pipeline**
  - **Description:** Il diagramma è composto da almeno due pipeline distinte (Build, Deploy)

- **REL-03 — Pipeline → Step Pipeline**
  - **Description:** Ogni pipeline è composta da uno o più step ordinati sequenzialmente

---

### 📜 Constraints

- **CON-01:** La pagina è visualizzata come pagina di default all'apertura dell'applicazione
- **CON-02:** Il contenuto è esclusivamente informativo e non prevede interazione da parte dell'utente
- **CON-03:** Il diagramma deve rappresentare almeno due pipeline distinte
- **CON-04:** Ogni step del diagramma deve includere nome e descrizione
- **CON-05:** La rappresentazione grafica deve essere semplice e intuitiva
- **CON-06:** Coerenza visiva tra sezione testuale e diagramma

---

## 🧩 USER STORIES

### US-01 — Visualizzazione sezione descrittiva

**Descrizione:**
Come utente voglio accedere alla pagina di benvenuto e leggere titolo, finalità e funzionalità principali dell'applicazione, così da comprendere immediatamente a cosa serve il sistema.

**Related Flow:** MF-01, MF-02

---

### US-02 — Visualizzazione diagramma pipeline

**Descrizione:**
Come utente voglio vedere un diagramma che rappresenti visivamente le pipeline di sviluppo e rilascio, così da comprendere il flusso complessivo del processo.

**Related Flow:** MF-01, MF-03

---

### US-03 — Lettura dettaglio step pipeline

**Descrizione:**
Come utente voglio vedere, per ogni fase di ogni pipeline, il nome dello step e una breve descrizione del suo scopo, così da comprendere cosa accade in ogni passaggio del processo.

**Related Flow:** MF-03

---

### US-04 — Accesso diretto dalla navigazione

**Descrizione:**
Come utente voglio raggiungere la pagina di benvenuto come pagina di default all'apertura dell'applicazione, senza dover navigare manualmente.

**Related Flow:** MF-01

---

## 🔄 MAIN FLOW

- **MF-01:** L'utente accede all'applicazione; il sistema mostra la pagina di benvenuto come pagina di default
- **MF-02:** Il sistema visualizza la sezione descrittiva con titolo, finalità e funzionalità principali
- **MF-03:** Il sistema visualizza la sezione diagramma con le pipeline (Build e Deploy) e i rispettivi step in ordine sequenziale

---

## 🔁 ALTERNATIVE FLOWS

*Nessun flusso alternativo rilevante: la pagina è puramente informativa e non prevede input utente.*

---

## 📜 BUSINESS RULES

- **BR-01:** La pagina di benvenuto è la pagina di default dell'applicazione (route `/` o equivalente)
- **BR-02:** Il diagramma deve contenere almeno due pipeline: una di Build e una di Deploy
- **BR-03:** Ogni pipeline deve contenere almeno uno step con nome e descrizione
- **BR-04:** Gli step devono essere rappresentati in ordine sequenziale all'interno di ogni pipeline
- **BR-05:** Il contenuto della pagina è statico; non è prevista interazione né modifica da parte dell'utente

---

## ⚠️ EDGE CASES

- **EC-01:** Utente che naviga direttamente alla URL della home → la pagina viene mostrata normalmente
- **EC-02:** Utente interno (stakeholder) che consulta il diagramma → il contenuto deve essere comprensibile anche senza conoscenza tecnica del sistema

---

## 🧪 TECHNICAL NOTES

- **TN-01:** La pagina è visualizzata come default route dell'applicazione (es. redirect `/` → `/Home` o pagina Index)
- **TN-02:** Il diagramma può essere implementato come HTML/CSS (es. con Bootstrap o elementi SVG/ASCII) o tramite libreria di diagrammi; la scelta implementativa è a discrezione
- **TN-03:** Il contenuto delle pipeline (step, descrizioni) può essere statico o configurabile; nessun vincolo imposto a questo livello
- **TN-04:** L'applicazione è ASP.NET Core con Razor Pages; la home è probabilmente la pagina Index del Controller Home o una Razor Page dedicata

---

## ❓ OPEN QUESTIONS

- **OQ-01:** Il contenuto delle pipeline (nomi e descrizioni degli step) è fisso/hard-coded o deve essere configurabile da un'origine dati?
- **OQ-02:** La sezione descrittiva deve includere link diretti alle funzionalità (es. pulsante "Vai alla Calcolatrice") o è puramente testuale?
- **OQ-03:** Il diagramma deve essere interattivo (es. tooltip al passaggio del mouse) o esclusivamente statico?
- **OQ-04:** La pagina di benvenuto è accessibile anche tramite una voce nel menu di navigazione, oltre che come default?

---

## 📊 Confidence

**Level:** medium  
**Notes:** L'analisi è basata su un documento funzionale chiaro negli obiettivi ma vago su alcuni dettagli (contenuto pipeline, interattività, navigazione). Le open questions individuate sono rilevanti per la progettazione. Il dominio è semplice e il rischio di errori funzionali è basso.
