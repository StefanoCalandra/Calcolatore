using System.Reflection;
using System.Runtime.InteropServices;

namespace WebCalculator.Services;

public sealed class NativeCalculatorService
{
    private const string NativeLibraryBaseName = "NativeCalculator";
    private readonly bool _nativeLoaded;

    [DllImport(NativeLibraryBaseName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Add(double left, double right);

    [DllImport(NativeLibraryBaseName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Subtract(double left, double right);

    [DllImport(NativeLibraryBaseName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Multiply(double left, double right);

    [DllImport(NativeLibraryBaseName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Divide(double left, double right);

    public NativeCalculatorService(ILogger<NativeCalculatorService> logger)
    {
        _nativeLoaded = TryLoadNativeLibrary(logger);
    }

    public double Calculate(double left, double right, string op)
    {
        if (_nativeLoaded)
        {
            return op switch
            {
                "+" => Add(left, right),
                "-" => Subtract(left, right),
                "*" => Multiply(left, right),
                "/" => Divide(left, right),
                _ => throw new NotSupportedException($"Operatore non supportato: {op}")
            };
        }

        return op switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" => right == 0 ? double.NaN : left / right,
            _ => throw new NotSupportedException($"Operatore non supportato: {op}")
        };
    }

    private static bool TryLoadNativeLibrary(ILogger<NativeCalculatorService> logger)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var baseDirectory = AppContext.BaseDirectory;
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? baseDirectory;
        var searchDirectories = new[]
        {
            currentDirectory,
            baseDirectory,
            assemblyDirectory,
            Path.Combine(currentDirectory, "x64", "Debug"),
            Path.Combine(currentDirectory, "x64", "Release"),
            Path.Combine(currentDirectory, "NativeCalculator", "x64", "Debug"),
            Path.Combine(currentDirectory, "NativeCalculator", "x64", "Release")
        };

        var candidateNames = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new[] { "NativeCalculator.dll" }
            : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? new[] { "libNativeCalculator.so", "NativeCalculator.so" }
                : new[] { "libNativeCalculator.dylib", "NativeCalculator.dylib" };

        foreach (var directory in searchDirectories.Distinct())
        {
            foreach (var candidateName in candidateNames)
            {
                var candidatePath = Path.Combine(directory, candidateName);
                if (!File.Exists(candidatePath))
                {
                    continue;
                }

                try
                {
                    NativeLibrary.Load(candidatePath);
                    logger.LogInformation("Libreria nativa caricata da: {Path}", candidatePath);
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Trovata libreria nativa ma non caricabile: {Path}", candidatePath);
                }
            }
        }

        logger.LogWarning("Libreria nativa non trovata. Verrà usato il fallback C#.");
        return false;
    }
}
