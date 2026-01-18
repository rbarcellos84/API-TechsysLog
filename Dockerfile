FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copia a solução e TODOS os projetos de Src (onde está o código real)
COPY ["TechsysLog.sln", "./"]
COPY ["Src/", "Src/"]

# 2. Restaura apenas a API (isso vai puxar as dependências das outras camadas automaticamente)
RUN dotnet restore "Src/TechsysLog.Api/TechsysLog.Web.Api.csproj"

# 3. Publica a aplicação
RUN dotnet publish "Src/TechsysLog.Api/TechsysLog.Web.Api.csproj" -c Release -o /app/publish

# Estágio de Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TechsysLog.Web.Api.dll"]