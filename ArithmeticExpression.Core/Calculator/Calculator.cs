using ArithmeticExpression.Core.Abstractions;
using ArithmeticExpression.Core.Operators;
using System.Text;

namespace ArithmeticExpression.Core.Calculator;

/*
var expression = "200+100-100*4-200/10"; // 10
var calculator = new Calculator();
var result = calculator.Evaluate(expression);
Console.WriteLine(result);

Console.ReadLine();
 */
public class Calculator
{
    public readonly ICalculatorOperators _operators;

    public Calculator(ICalculatorOperators operators)
    {
        _operators = operators;
    }

    public double Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression) || expression.Length <= 2)
        {
            return 0;
        }

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

        return numbersStack.Pop();
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
