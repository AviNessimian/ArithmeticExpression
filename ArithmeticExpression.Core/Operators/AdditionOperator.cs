using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class AdditionOperator : Operator
{
    public override char Symbol => '+';
    public override int Order => 1;

    public override double Execute(double num1, double num2) => num1 + num2;
}
