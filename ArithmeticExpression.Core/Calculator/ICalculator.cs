namespace ArithmeticExpression.Core.Calculator;

public interface ICalculator
{
    CalculationResponse Evaluate(string expression);
}


public class CalculationResponse
{
    public required bool IsSuccess { get; set; }
    public string Error { get; set; }
    public double Result { get; set; }
}