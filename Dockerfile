FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY  ["/ArithmeticExpression.Minimal.sln", "./"]
COPY  ["/ArithmeticExpression.Core/*.csproj", "./ArithmeticExpression.Core/"]
COPY  ["/ArithmeticExpression.Host/*.csproj", "./ArithmeticExpression.Host/"]

RUN dotnet restore 

# Copy everything else and build
COPY . ./

# Publish the app to the out directory
RUN dotnet publish "ArithmeticExpression.Minimal.sln" -c Release -o out


### Build Angular Client ### 
FROM node:18.12.0-alpine AS angular-build
WORKDIR /app

# npm install is costly command... (copy only when package.json changed)
COPY ./ArithmeticExpression.Host/ClientApp/package.json ./
COPY ./ArithmeticExpression.Host/ClientApp/package-lock.json ./
RUN npm install

COPY ./ArithmeticExpression.Host/ClientApp/ ./
RUN npm run build:prod


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

# update + upgrade system
RUN apt-get update -y && apt-get upgrade -y

WORKDIR /app

COPY --from=build-env /app/out .

# Remove Development environment appsetting.json
RUN rm -rf "appsettings.Development.json"


COPY --from=angular-build /app/dist ./wwwroot

ENV ASPNETCORE_URLS "http://+:5191"

ENTRYPOINT ["dotnet", "ArithmeticExpression.Host.dll"]



