using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class MultiplicationOperator : IOperator
{
    public char Symbol => '*';
    public int Precedence => 2;

    public double Execute(double num1, double num2) => num1 * num2;
}
