# FEATURE: SimpleWebCalculator

## 🎯 OBIETTIVO
Realizzare una web application basata su ASP.NET Core Razor Pages che permetta all'utente di eseguire operazioni matematiche di base tramite un'interfaccia web semplice con tastierino numerico integrato.

---

## 👤 ATTORI
- Utente web

---

## 🧩 FUNZIONALITÀ

### 1. Navigazione
L'utente deve poter:
- accedere alla calcolatrice come pagina predefinita (homepage)
- navigare tra le sezioni dell'applicazione tramite menu di navigazione
- visualizzare sempre il menu nelle pagine (Calculator, Home, Privacy)
- identificare visivamente la pagina corrente tramite highlight nel menu

---

### 2. Inserimento input
L'utente deve poter:
- inserire un primo numero (A) tramite tastierino numerico
- inserire un secondo numero (B) tramite tastierino numerico
- utilizzare cifre 0-9 e punto decimale
- visualizzare i numeri inseriti nel display della calcolatrice

**Stato iniziale del display:**
- quando la calcolatrice è in stato iniziale (nessun valore inserito e nessun risultato), il display mostra un testo guida: **"Digit Here..."**

---

### 3. Selezione operazione
L'utente deve poter scegliere una delle seguenti operazioni:
- Addizione (operazione default)
- Sottrazione
- Moltiplicazione
- Divisione

---

### 4. Calcolo risultato
Il sistema deve:
- ricevere i valori A e B
- applicare l'operazione selezionata
- restituire il risultato arrotondato a 2 cifre decimali
- validare che i valori siano nel range ±10^15

---

### 5. Visualizzazione
Il sistema deve:
- mostrare il risultato nella stessa pagina (nel display)
- aggiornare la UI dopo il submit
- mantenere l'interfaccia simile a una calcolatrice standard
- funzionare su desktop e mobile (responsive)

**Regola di visualizzazione del display:**
- alla prima digitazione il testo guida scompare e viene mostrato il valore inserito
- quando viene mostrato un risultato, il display mostra sempre il risultato (anche se il risultato è 0.00)
- dopo "Clear" la calcolatrice torna allo stato iniziale e il display torna a mostrare **"Digit Here..."**

---

### 6. Gestione errori
Il sistema deve gestire:
- divisione per zero
- input non validi
- campi vuoti
- valori fuori range (overflow/underflow)
- caratteri non numerici

---

### 7. Storico operazioni (History)
L'utente deve poter:
- memorizzare lo storico delle operazioni effettuate durante la sessione di utilizzo della calcolatrice
- aprire lo storico tramite un pulsante "History" vicino alla calcolatrice
- visualizzare lo storico in un popup con scrollbar quando le operazioni sono molte
- chiudere il popup e tornare alla calcolatrice senza perdere lo stato corrente

Il sistema deve:
- aggiungere allo storico solo le operazioni completate con successo
- mostrare un messaggio "No history" quando lo storico è vuoto
- cancellare lo storico quando l'utente preme "Clear"
- rendere visibile immediatamente la cancellazione dello storico (subito dopo Clear, senza richiedere nuove operazioni)

---

## 🔁 FLUSSO UTENTE

### Flusso Principale
1. L'utente accede all'applicazione (URL root /)
2. Il sistema reindirizza automaticamente a /Calculator
3. L'utente visualizza la pagina calcolatrice con menu di navigazione e tastierino
4. Il display mostra "Digit Here..." finché l'utente non inizia a digitare
5. L'utente inserisce i valori numerici tramite tastierino
6. L'utente seleziona l'operazione (default: addizione)
7. L'utente clicca su "=" (Calcola)
8. Il sistema elabora la richiesta lato server
9. Il risultato viene mostrato nel display della calcolatrice

### Navigazione
1. L'utente visualizza il menu con tre voci: Calculator, Home, Privacy
2. L'utente clicca su una voce del menu
3. Il sistema naviga alla pagina corrispondente
4. Il menu evidenzia la voce attiva per orientare l'utente

### Consultazione History
1. L'utente esegue una o più operazioni con successo
2. L'utente preme "History"
3. Il sistema mostra un popup con l'elenco delle operazioni eseguite (scorribile)
4. L'utente chiude il popup e continua a usare la calcolatrice
5. L'utente preme "Clear"
6. Il sistema resetta la calcolatrice e svuota lo storico; se il popup è aperto o viene riaperto subito dopo, mostra "No history"

---

## 🏗️ COMPONENTI

### NAVIGATION (Nuovo)
- Menu navbar nel layout condiviso (_Layout.cshtml)
- Voci: Calculator, Home, Privacy
- Helper method per evidenziare pagina attiva
- Responsive: hamburger menu su mobile (< 992px)

### ROUTING (Nuovo)
- Redirect root "/" → "/Calculator"
- Configurazione in Program.cs
- Supporto sia Razor Pages che Controller-based pages

### PAGE MODEL (Calculator)
- A: double (operando A)
- B: double (operando B)
- Operation: string (codice operazione: add, sub, mul, div)
- Result: double (risultato arrotondato 2 decimali)
- ErrorMessage: string (messaggi errore)

### SERVICE (Business Logic)
- CalculatorService (separato dal PageModel)
- Responsabilità:
  - validazione range ±10^15
  - esecuzione operazioni matematiche
  - arrotondamento risultato a 2 decimali
  - gestione divisione per zero

### VIEW (Calculator)
- Display (visualizzazione numeri e risultati)
- Testo guida nel display in stato iniziale: "Digit Here..."
- Tastierino numerico (grid con cifre 0-9 e punto decimale)
- Pulsanti operazioni (+, −, ×, ÷)
- Pulsante Calcola (=)
- Pulsante Clear
- Pulsante History
- Popup History con scrollbar
- Gestione errori (alert)

### VIEW (Home)
- Pagina informativa di presentazione
- Descrizione funzionalità Calculator App
- Link prominente "Go to Calculator"

---

## ⚙️ BUSINESS RULES

### Operazioni Matematiche
- add → A + B
- sub → A - B
- mul → A * B
- div:
  - se B != 0 → A / B
  - se B == 0 → errore gestito con messaggio specifico

### Validazione e Precisione
- Valori A, B e risultato: range ±10^15
- Risultato arrotondato a 2 cifre decimali
- Input limitato a cifre 0-9 e punto decimale
- Operazione default: addizione

### Navigazione
- Root "/" reindirizza a "/Calculator"
- Menu presente in tutte le pagine
- Voce menu attiva evidenziata visivamente
- Menu responsive con hamburger su mobile (< 992px)

### Display
- In stato iniziale il display mostra "Digit Here..."
- Alla prima digitazione viene mostrato il valore inserito
- Il risultato viene sempre mostrato nel display (anche se 0.00)
- Dopo Clear, il display torna a mostrare "Digit Here..."

### History
- Ogni operazione completata con successo viene aggiunta allo storico
- Lo storico è consultabile tramite popup
- Lo storico viene cancellato da Clear
- La cancellazione dello storico deve essere immediatamente visibile

---

## 📌 OUTPUT ATTESO

- ✅ Pagina web funzionante accessibile da root "/"
- ✅ Calcolatrice con interfaccia simile a calcolatrice standard
- ✅ Display con testo guida "Digit Here..." in stato iniziale
- ✅ Calcolo corretto con operazioni base
- ✅ Gestione errori completa
- ✅ History consultabile tramite popup
- ✅ Clear svuota history immediatamente

---

## 🔄 EVOLUZIONI

- FEAT-002: Navigazione e Homepage
- FEAT-004: Percentuale
- FEAT-006: Operazioni sequenziali
- FEAT-007: History in sessione con popup
- FEAT-008: Clear history immediato
- FEAT-009: Placeholder display "Digit Here..."

---

**Status**: ✅ Implementato  
**Versione**: 1.3 (include placeholder display)  
**Data Ultimo Aggiornamento**: 2026-06-05