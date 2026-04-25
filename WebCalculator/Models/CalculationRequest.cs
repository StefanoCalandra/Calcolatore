namespace WebCalculator.Models;

public sealed class CalculationRequest
{
    public double Left { get; set; }
    public double Right { get; set; }
    public string Operator { get; set; } = "+";
}
