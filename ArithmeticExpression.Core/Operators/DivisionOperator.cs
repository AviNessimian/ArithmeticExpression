using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class DivisionOperator : Operator
{
    public override char Symbol => '/';
    public override int Order => 2;

    public override double Execute(double op1, double op2) => op1 / op2;
}