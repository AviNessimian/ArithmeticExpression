using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class AdditionOperator : IOperator
{
    public char Symbol => '+';
    public int Precedence => 1;

    public double Execute(double num1, double num2) => num1 + num2;
}
