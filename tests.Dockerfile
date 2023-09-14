FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY  ["/ArithmeticExpression.sln", "./"]
COPY  ["/ArithmeticExpression.Core/*.csproj", "./ArithmeticExpression.Core/"]
COPY  ["/ArithmeticExpression.Host/*.csproj", "./ArithmeticExpression.Host/"]
COPY  ["/ArithmeticExpression.UnitTests/ArithmeticExpression.UnitTests.csproj", "./ArithmeticExpression.UnitTests/"]
COPY  ["/ArithmeticExpression.IntegrationTests/ArithmeticExpression.IntegrationTests.csproj", "./ArithmeticExpression.IntegrationTests/"]

RUN dotnet restore 

# Copy everything else and build
COPY . ./


