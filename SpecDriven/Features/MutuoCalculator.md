# FEATURE: MutuoCalculator

## 🎯 OBIETTIVO
Fornire uno strumento semplice e intuitivo per la simulazione del piano di ammortamento di un mutuo, consentendo all'utente di inserire i dati principali, ottenere il calcolo automatico delle rate e visualizzare il dettaglio completo del piano.

---

## 👤 ATTORI
- Privati che desiderano simulare un mutuo
- Consulenti finanziari
- Operatori del settore
- Utenti a scopo educativo

---

## 🧩 FUNZIONALITÀ

### 1. Inserimento dati
L'utente deve poter inserire i dati necessari alla simulazione:
- Importo del mutuo
- Tasso di interesse (strettamente positivo, > 0)
- Durata in anni (valore intero positivo)
- Frequenza delle rate (selezionabile tramite combo box: mensile, trimestrale, semestrale, annuale)
- Data di inizio

---

### 2. Calcolo
Il sistema deve:
- Calcolare l'importo della rata (ammortamento alla francese)
- Determinare il numero totale di rate (durata anni × periodi per anno)
- Generare il piano di ammortamento completo

---

### 3. Riepilogo risultati
L'utente deve poter visualizzare un riepilogo contenente:
- Importo della rata
- Totale interessi
- Totale capitale restituito
- Totale complessivo pagato

---

### 4. Dettaglio piano di ammortamento
Il sistema deve mostrare una tabella con, per ogni rata:
- Numero rata
- Data
- Importo rata
- Quota capitale
- Quota interessi
- Capitale residuo

---

### 5. Gestione errori e validazioni
Il sistema deve verificare che:
- I dati inseriti siano completi
- I valori siano coerenti e validi
- Il tasso di interesse sia strettamente positivo (tasso = 0 non è supportato)

In caso contrario, vengono mostrati messaggi informativi.

---

### 6. Esportazione (opzionale)
L'utente può esportare i risultati in formati utilizzabili esternamente o procedere alla stampa.

---

## 🔁 FLUSSO UTENTE

### Flusso Principale
1. L'utente accede alla pagina di simulazione mutuo
2. Inserisce i dati richiesti (importo, tasso, durata in anni, frequenza tramite combo box, data di inizio)
3. Avvia il calcolo
4. Il sistema valida i dati ed elabora il piano
5. Viene mostrato il riepilogo con importo rata, totali e interessi
6. Viene mostrata la tabella del piano di ammortamento con paginazione

### Flusso Alternativo — Tasso uguale a zero
1. L'utente inserisce tasso = 0
2. Il sistema mostra un messaggio informativo; il calcolo non viene eseguito

---

## 🏗️ COMPONENTI

### FORM INSERIMENTO DATI
- Campo importo mutuo
- Campo tasso di interesse
- Campo durata (in anni)
- Combo box per la selezione della frequenza delle rate (mensile, trimestrale, semestrale, annuale)
- Campo data di inizio
- Pulsante di avvio calcolo

### RIEPILOGO RISULTATI
- Importo rata calcolata
- Totale interessi complessivi
- Totale capitale restituito
- Totale complessivo pagato

### TABELLA PIANO DI AMMORTAMENTO
- Una riga per ogni rata
- Colonne: numero rata, data, importo rata, quota capitale, quota interessi, capitale residuo
- Paginazione per piani con molte rate (20 righe per pagina)

### ESPORTAZIONE (Opzionale)
- Esportazione risultati in formato esterno
- Stampa del piano

---

## ⚙️ BUSINESS RULES

### Calcolo Rata
- La rata viene calcolata con la formula di ammortamento alla francese: `R = C × (i / (1 − (1+i)^−n))` dove C = capitale, i = tasso periodico (tasso annuo / 100 / periodi per anno), n = durata anni × periodi per anno
- La durata è espressa in anni interi; il numero totale di rate = durata anni × periodi per anno
- La frequenza delle rate determina il numero totale e la periodicità; i valori ammessi sono: mensile (12/anno), trimestrale (4/anno), semestrale (2/anno), annuale (1/anno)

### Validazione Dati
- L'importo del mutuo deve essere un valore positivo
- Il tasso di interesse deve essere strettamente positivo (> 0); il tasso uguale a zero non è supportato
- La durata deve essere un valore intero positivo, espresso in anni
- La frequenza deve essere uno dei valori ammessi dal combo box
- La data di inizio deve essere una data valida

### Piano di Ammortamento
- Ogni rata è composta da una quota capitale e una quota interessi
- Il capitale residuo si riduce ad ogni rata fino ad azzerarsi
- I totali del riepilogo devono essere coerenti con la somma delle rate

---

## 🔒 CONSTRAINTS

- **Ambito**: simulazione di mutui con piano di ammortamento standard (ammortamento alla francese)
- **Rendering**: server-side
- **UI**: semplice, intuitiva e di facile lettura
- **Responsività**: funzionante su desktop e dispositivi mobili

---

## 📌 OUTPUT ATTESO

- ✅ Form di inserimento dati funzionante con combo box per la frequenza
- ✅ Calcolo corretto della rata e del piano di ammortamento
- ✅ Riepilogo con totali coerenti
- ✅ Tabella dettaglio con paginazione (20 righe per pagina)
- ✅ Gestione errori e messaggi informativi (incluso rifiuto tasso = 0)
- ✅ Esportazione risultati (opzionale)

---

## 🔄 EVOLUZIONI FUTURE

Sono previste evoluzioni che potranno includere:
- Simulazioni avanzate
- Confronto tra scenari
- Visualizzazioni grafiche
- Salvataggio delle simulazioni
- Frequenza quindicinale
- Esportazione in formato esterno

---

**Status**: ✅ Implementata  
**Versione**: 1.1  
**Data Ultimo Aggiornamento**: 2026-06-05