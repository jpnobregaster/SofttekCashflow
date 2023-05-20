#Arquivo de configuração para rodar a webapi via docker.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Softtek.Cashflow.Api/Softtek.Cashflow.Api.csproj", "Softtek.Cashflow.Api/"]
COPY ["Softtek.Cashflow.Domain/Softtek.Cashflow.Domain.csproj", "Softtek.Cashflow.Domain/"]
COPY ["Softtek.Cashflow.Application/Softtek.Cashflow.Application.csproj", "Softtek.Cashflow.Application/"]
COPY ["Softtek.Cashflow.Infra/Softtek.Cashflow.Infra.csproj", "Softtek.Cashflow.Infra/"]
COPY ["Softtek.Cashflow.Infra.IoC/Softtek.Cashflow.Infra.IoC.csproj", "Softtek.Cashflow.Infra.IoC/"]

RUN dotnet restore "./Softtek.Cashflow.Api/Softtek.Cashflow.Api.csproj"
RUN dotnet restore "./Softtek.Cashflow.Domain/Softtek.Cashflow.Domain.csproj"
RUN dotnet restore "./Softtek.Cashflow.Application/Softtek.Cashflow.Application.csproj"
RUN dotnet restore "./Softtek.Cashflow.Infra/Softtek.Cashflow.Infra.csproj"
RUN dotnet restore "./Softtek.Cashflow.Infra.IoC/Softtek.Cashflow.Infra.IoC.csproj"

COPY . .
WORKDIR "/src/Softtek.Cashflow.Api"
RUN dotnet build "Softtek.Cashflow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Softtek.Cashflow.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Softtek.Cashflow.Api.dll"]
