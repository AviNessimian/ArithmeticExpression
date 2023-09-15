using ArithmeticExpression.Core.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace ArithmeticExpression.Host.Api;

public static class ApiRouteHandlersExtension
{
    public static RouteHandlerBuilder MapCalculatorRoute(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/api/calculator", 
            ([FromBody] string expression, 
            ILoggerFactory loggerFactory, 
            ICalculator calculator) =>
        {
            try
            {
                var result = calculator.Evaluate(expression);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger("Calculator");
                logger.LogError(ex, "Calculator error");
                return Results.BadRequest();
            }
        })
       .WithName("Calculator")
       .WithOpenApi();
    }
}