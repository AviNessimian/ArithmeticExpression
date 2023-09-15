namespace ArithmeticExpression.Core.Abstractions;

public interface IOperator
{
    public char Symbol { get; }
    public int Order { get; }
    public double Execute(double num1, double num2);

}
