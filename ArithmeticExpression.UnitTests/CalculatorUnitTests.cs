using ArithmeticExpression.Core.Abstractions;
using ArithmeticExpression.Core.Calculator;
using ArithmeticExpression.Core.Operators;

namespace ArithmeticExpression.UnitTests;

public sealed class CalculatorUnitTests
{
    private readonly Mock<ILogger<Calculator>> _logger;
    private readonly Mock<IEnumerable<IOperator>> _operators;

    public CalculatorUnitTests()
    {
        _operators = new Mock<IEnumerable<IOperator>>();
        var ops = new List<IOperator>
        {
             new AdditionOperator(),
             new SubtractionOperator(),
             new MultiplicationOperator(),
             new DivisionOperator()
        };

        _operators.Setup(_ => _.GetEnumerator()).Returns(ops.GetEnumerator());
        _logger = new Mock<ILogger<Calculator>>();
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("8*")]
    public void Evaluate_IsInValidExpression_ShouldReturnError(string expression)
    {
        var calculator = new Calculator(_logger.Object, _operators.Object);
        var output = calculator.Evaluate(expression);

        output.Should().NotBeNull();
        output.IsSuccess.Should().BeFalse();
        output.Error.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("1+2", 3)]
    [InlineData("8-2", 6)]
    [InlineData("8+5", 13)]
    [InlineData("12+45", 57)]
    [InlineData("123+35", 158)]
    [InlineData("2+2*2", 6)]
    [InlineData("12+5*7/4-2", 18.75)]
    [InlineData("3.5+12.3 ", 15.8)]
    [InlineData("0.5*8 ", 4)]
    public void Evaluate_ArithmeticOperations_ShouldReturnSuccess(string expression, double result)
    { 
        var calculator = new Calculator(_logger.Object, _operators.Object);
        var output = calculator.Evaluate(expression);

        output.Should().NotBeNull();
        output.IsSuccess.Should().BeTrue();
        output.Error.Should().BeNullOrEmpty();
        output.Result.Should().Be(result);
    }
}