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
3. Compila prima `NativeCalculator` (x64).
4. Copia `NativeCalculator.dll` nella cartella di output di `WebCalculator` (es. `WebCalculator\\bin\\Debug\\net8.0`).
5. Avvia `WebCalculator` come startup project.

## Note

- La divisione per zero in C++ ritorna `NaN`; l'API risponde con errore leggibile.
- Lato browser, il calcolo viene inviato a `POST /api/calculator/calculate`.
