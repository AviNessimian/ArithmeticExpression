namespace ArithmeticExpression.Core.Abstractions;

public abstract class Operator : IOperator
{
    public abstract char Symbol { get; }
    public abstract int Order { get; }

    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        var op = (Operator)obj;
        return Symbol == op.Symbol
            &&
            Order == op.Order;
    }

    public abstract double Execute(double num1, double num2);

    public override int GetHashCode()
    {
        var hc = new HashCode();
        new HashCode().Add(Symbol);
        new HashCode().Add(Order);
        return hc.ToHashCode();
    }
}