using ArithmeticExpression.Core.Abstractions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ArithmeticExpression.Core.Calculator;

public class Calculator : ICalculator
{
    private readonly ILogger<Calculator> _logger;
    public readonly ICalculatorOperators _operators;

    public Calculator(ILogger<Calculator> logger, ICalculatorOperators operators)
    {
        _logger = logger;
        _operators = operators;
    }

    public CalculationResponse Evaluate(string expression)
    {
        if (IsInValidExpression(expression))
        {
            _logger.LogInformation("invalid expression length: {Expression}", expression);

            return new CalculationResponse
            {
                IsSuccess = false,
                Error = "invalid expression length"
            }; ;
        }

        _logger.LogInformation("Evaluating expression: {Expression}", expression);

        var numbersStack = new Stack<double>();
        var operatorsStack = new Stack<char>();

        for (int i = 0; i < expression.Length; i++)
        {
            var character = expression[i];
            if (_operators.AllowedOperators.Contains(character))
            {
                while (operatorsStack.Count > 0 && _operators.GetOrder(operatorsStack.Peek()) >= _operators.GetOrder(character))
                {
                    ApplyOperation(numbersStack, operatorsStack);
                }

                operatorsStack.Push(character);
            }
            else if (IsValidDigit(character))
            {
                var numberAsString = new StringBuilder();
                while (IsValidDigit(character))
                {
                    numberAsString.Append(character);
                    i++;
                    if (i == expression.Length) break;
                    character = expression[i];
                }

                i--;

                numbersStack.Push(double.Parse(numberAsString.ToString()));
            }
        }

        while (operatorsStack.Count > 0)
        {
            ApplyOperation(numbersStack, operatorsStack);
        }

        var result = numbersStack.Pop();

        _logger.LogInformation(
            "Evaluation result: {Result}, expression: {Expression}",
            result, expression);

        return new CalculationResponse
        {
            IsSuccess = true,
            Result = result
        };
    }

    private static bool IsInValidExpression(string expression)
    {
        return string.IsNullOrWhiteSpace(expression)
            || expression.Length <= 2
            || expression.Length > 200;
    }

    private void ApplyOperation(Stack<double> numbers, Stack<char> operators)
    {
        var op = operators.Pop();
        var secondNumber = numbers.Pop();
        var firstNumber = numbers.Pop();
        var opResult = _operators.RunOpration(op, firstNumber, secondNumber);
        numbers.Push(opResult);
    }

    private static bool IsValidDigit(char character)
        => char.IsDigit(character) || character == '.';
}
