namespace ArithmeticExpression.Core.Calculator;

public class CalculationResponse
{
    public required bool IsSuccess { get; set; }
    public string Error { get; set; }
    public double Result { get; set; }
}