namespace ArithmeticExpression.Core.Calculator;

public interface ICalculator
{
    CalculationResponse Evaluate(string expression);
}
