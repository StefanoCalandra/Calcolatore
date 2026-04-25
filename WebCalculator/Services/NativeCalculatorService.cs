using System.Runtime.InteropServices;

namespace WebCalculator.Services;

public sealed class NativeCalculatorService
{
    private const string NativeLibraryName = "NativeCalculator";

    [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Add(double left, double right);

    [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Subtract(double left, double right);

    [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Multiply(double left, double right);

    [DllImport(NativeLibraryName, CallingConvention = CallingConvention.Cdecl)]
    private static extern double Divide(double left, double right);

    public double Calculate(double left, double right, string op)
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
}
