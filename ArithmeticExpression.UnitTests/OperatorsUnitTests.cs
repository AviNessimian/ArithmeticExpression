using ArithmeticExpression.Core.Abstractions;
using ArithmeticExpression.Core.Operators;

namespace ArithmeticExpression.UnitTests;

public class OperatorsUnitTests
{

    [Fact]
    public void AdditionOperator_Execute_ShouldReturnCaculationResult()
    {
        IOperator @operator = new AdditionOperator();
        double num1 = 10;
        double num2 = 20;
        var result = @operator.Execute(num1, num2);

        result.Should().Be(num1 + num2);
    }
}