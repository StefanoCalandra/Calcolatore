# Calcolatore Web con Visual Studio (C++ + C#)

Questo progetto crea un **sito web calcolatore** usando:

- **C# / ASP.NET Core MVC** per interfaccia web e API.
- **C++ (DLL nativa)** per la logica matematica.

## Struttura

- `Calcolatore.sln`: soluzione Visual Studio.
- `WebCalculator`: progetto web in C#.
- `NativeCalculator`: libreria C++ con funzioni `Add`, `Subtract`, `Multiply`, `Divide`.

## Come aprirlo in Visual Studio

1. Apri `Calcolatore.sln` in **Visual Studio 2022**.
2. Verifica di avere i workload:
   - **ASP.NET and web development**
   - **Desktop development with C++**
3. Imposta la soluzione su **x64**.
4. Compila `NativeCalculator` (Debug o Release x64).
5. Copia `NativeCalculator.dll` nella cartella di output di `WebCalculator` (es. `WebCalculator\\bin\\Debug\\net8.0`) oppure nella cartella da cui avvii il processo web.
6. Avvia `WebCalculator` come startup project.

## Risoluzione errore "Unable to load DLL 'NativeCalculator'"

Se compare l'errore `Unable to load DLL 'NativeCalculator' ... (0x8007007E)`, significa che la DLL non è in un percorso risolvibile dal processo web o manca una sua dipendenza C++ runtime.

Controlli rapidi:

1. Conferma che `NativeCalculator.dll` esista davvero.
2. Copia la DLL vicino a `WebCalculator.exe`/`dotnet` output (`bin\\...\\net8.0`).
3. Assicurati che architettura sia coerente: **web x64 + DLL x64**.
4. Installa il **Microsoft Visual C++ Redistributable** se necessario.

> Nota: se la DLL non viene trovata, l'app usa automaticamente un fallback C# per non bloccare il calcolo.

## Note

- La divisione per zero in C++ ritorna `NaN`; l'API risponde con errore leggibile.
- Lato browser, il calcolo viene inviato a `POST /api/calculator/calculate`.
