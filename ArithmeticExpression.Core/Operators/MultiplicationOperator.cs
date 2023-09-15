using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class MultiplicationOperator : Operator
{
    public override char Symbol => '*';
    public override int Order => 2;

    public override double Execute(double num1, double num2) => num1 * num2;
}
