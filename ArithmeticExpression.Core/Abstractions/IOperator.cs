namespace ArithmeticExpression.Core.Abstractions;

public interface IOperator
{
    public char Symbol { get; }
    public int Order { get; }

    public double Execute(double op1, double op2);

}
