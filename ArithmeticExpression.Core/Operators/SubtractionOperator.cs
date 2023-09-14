using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class SubtractionOperator : Operator, IOperator
{
    public override char Symbol => '-';
    public override int Order => 1;

    public double Execute(double op1, double op2) => op1 - op2;
}
