# Sobe somente os serviços de infraestrutura
docker compose up -d mysql kafka kafdrop

# (opcional) ver status/health
docker compose ps
docker compose logs -f kafka
docker compose logs -f mysql