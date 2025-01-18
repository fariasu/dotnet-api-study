# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar o arquivo da solução
COPY TaskManager.sln ./

# Copiar todos os projetos para dentro do container
COPY src/Backend/ ./src/Backend/
COPY tests/ ./tests/

# Restaurar dependências
RUN dotnet restore TaskManager.sln

# Publicar a aplicação em modo Release
WORKDIR /app/src/Backend/TaskManager.API
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar arquivos do estágio de build
COPY --from=build-env /app/out ./

# Expõe a porta usada pela API
EXPOSE 8080

# Inicia a aplicação
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
