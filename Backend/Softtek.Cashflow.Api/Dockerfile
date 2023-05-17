#Arquivo de configuração para rodar a webapi via docker.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Softtek.Cashflow.Api.csproj", "."]
RUN dotnet restore "./Softtek.Cashflow.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Softtek.Cashflow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Softtek.Cashflow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Softtek.Cashflow.Api.dll"]