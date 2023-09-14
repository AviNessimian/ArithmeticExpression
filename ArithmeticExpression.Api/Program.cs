using ArithmeticExpression.Api.Calculator;
using ArithmeticExpression.Api.Calculator.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var handlersChain = new ParenthesesHandler(new MultiplyHandler(
    new DivisionHandler(
        new AdditionHandler(
            new SubtractionHandler(null)))));


builder.Services.AddSingleton<ICalculatorHandler>(handlersChain);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
