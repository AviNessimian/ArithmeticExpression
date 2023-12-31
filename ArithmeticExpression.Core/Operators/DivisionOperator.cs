﻿using ArithmeticExpression.Core.Abstractions;

namespace ArithmeticExpression.Core.Operators;

public class DivisionOperator : IOperator
{
    public char Symbol => '/';
    public int Precedence => 2;

    public double Execute(double op1, double op2) => op1 / op2;
}