# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os .csproj (camadas do serviço e contratos) para cache de restore
COPY Shared.Contracts/Shared.Contracts.csproj Shared.Contracts/
COPY Proposta/PropostaService.Domain/PropostaService.Domain.csproj Proposta/PropostaService.Domain/
COPY Proposta/PropostaService.Application/PropostaService.Application.csproj Proposta/PropostaService.Application/
COPY Proposta/PropostaService.Infrastructure/PropostaService.Infrastructure.csproj Proposta/PropostaService.Infrastructure/
COPY Proposta/PropostaService.Api/PropostaService.Api.csproj Proposta/PropostaService.Api/

# Restore
RUN dotnet restore Proposta/PropostaService.Api/PropostaService.Api.csproj

# Copia o restante do código
COPY . .

# Publica
RUN dotnet publish Proposta/PropostaService.Api/PropostaService.Api.csproj \
    -c Release -o /app/out /p:UseAppHost=false

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Porta padrão (pode sobrescrever com -e PORT=8081)
ARG PORT=8081
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

COPY --from=build /app/out ./

# Se você tiver endpoint de health (/health), pode ativar:
# HEALTHCHECK --interval=15s --timeout=3s --retries=10 CMD wget -qO- http://localhost:$PORT/health || exit 1

ENTRYPOINT ["dotnet", "PropostaService.Api.dll"]
