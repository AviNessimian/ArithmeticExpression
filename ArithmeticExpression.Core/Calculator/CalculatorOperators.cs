using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Calculator;

public class CalculatorOperators : ICalculatorOperators
{
    public readonly Dictionary<char, IOperator> _operators;

    public CalculatorOperators(IEnumerable<IOperator> operators)
    {
        _operators = operators.ToDictionary(k => k.Symbol, v => v);
        AllowedOperators = _operators.Select(_ => _.Value.Symbol).ToArray();
    }

    public char[] AllowedOperators { get; }

    public double RunOpration(char operation, double operand1, double operand2)
        => _operators[operation].Execute(operand1, operand2);


    public int GetOrder(char operation)
    {
        if (_operators.TryGetValue(operation, out var value))
        {
            return value.Order;
        }

        return 0;
    }
}