using ArithmeticExpression.Core.Abstractions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ArithmeticExpression.Core.Calculator;

public class Calculator : ICalculator
{
    private readonly ILogger<Calculator> _logger;
    public readonly Dictionary<char, IOperator> _operators;
    private readonly char[] _allowedOperatorSymbols;

    public Calculator(ILogger<Calculator> logger, IEnumerable<IOperator> operators)
    {
        _logger = logger;
        _operators = operators.ToDictionary(k => k.Symbol, v => v);
        _allowedOperatorSymbols = _operators.Select(_ => _.Value.Symbol).ToArray();
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
            if (IsAllowedOperatorSymbols(character))
            {
                while (operatorsStack.Count > 0 
                    && GetOrder(operatorsStack.Peek()) >= GetOrder(character))
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
            || expression.Length > double.MaxValue;
    }

    private void ApplyOperation(Stack<double> numbers, Stack<char> operators)
    {
        var operation = operators.Pop();
        var secondNumber = numbers.Pop();
        var firstNumber = numbers.Pop();
        var opResult = _operators[operation].Execute(firstNumber, secondNumber);
        numbers.Push(opResult);
    }

    private static bool IsValidDigit(char character)
        => char.IsDigit(character) || character == '.';

    private bool IsAllowedOperatorSymbols(char character) => _allowedOperatorSymbols.Contains(character);

    private double Execute(char operation, double operand1, double operand2)
        => _operators[operation].Execute(operand1, operand2);

    private int GetOrder(char operation)
    {
        if (_operators.TryGetValue(operation, out var value))
        {
            return value.Precedence;
        }

        return 0;
    }
}
