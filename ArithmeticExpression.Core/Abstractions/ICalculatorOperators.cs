namespace ArithmeticExpression.Core.Abstractions;

public interface ICalculatorOperators
{
    char[] AllowedOperators { get; }

    double RunOpration(char operation, double operand1, double operand2);
    int GetOrder(char operation);
}