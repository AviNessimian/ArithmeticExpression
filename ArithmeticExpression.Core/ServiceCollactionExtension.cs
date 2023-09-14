using ArithmeticExpression.Core.Abstractions;
using ArithmeticExpression.Core.Calculator;
using ArithmeticExpression.Core.Operators;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollactionExtension
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .AddSingleton<IOperator, AdditionOperator>()
            .AddSingleton<IOperator, SubtractionOperator>()
            .AddSingleton<IOperator, MultiplicationOperator>()
            .AddSingleton<IOperator, DivisionOperator>();

        services.AddSingleton<ICalculatorOperators, CalculatorOperators>();
        services.AddTransient<ICalculator, Calculator>();

        return services;
    }
}
