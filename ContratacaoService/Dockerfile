# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os .csproj (camadas do serviço e contratos) para cache de restore
COPY Shared.Contracts/Shared.Contracts.csproj Shared.Contracts/
COPY Contratacao/ContratacaoService.Domain/ContratacaoService.Domain.csproj Contratacao/ContratacaoService.Domain/
COPY Contratacao/ContratacaoService.Application/ContratacaoService.Application.csproj Contratacao/ContratacaoService.Application/
COPY Contratacao/ContratacaoService.Infrastructure/ContratacaoService.Infrastructure.csproj Contratacao/ContratacaoService.Infrastructure/
COPY Contratacao/ContratacaoService.Api/ContratacaoService.Api.csproj Contratacao/ContratacaoService.Api/

# Restore
RUN dotnet restore Contratacao/ContratacaoService.Api/ContratacaoService.Api.csproj

# Copia o restante do código
COPY . .

# Publica
RUN dotnet publish Contratacao/ContratacaoService.Api/ContratacaoService.Api.csproj \
    -c Release -o /app/out /p:UseAppHost=false

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Porta padrão (pode sobrescrever com -e PORT=8082)
ARG PORT=8082
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

COPY --from=build /app/out ./

# Se você tiver endpoint de health (/health), pode ativar:
# HEALTHCHECK --interval=15s --timeout=3s --retries=10 CMD wget -qO- http://localhost:$PORT/health || exit 1

ENTRYPOINT ["dotnet", "ContratacaoService.Api.dll"]
