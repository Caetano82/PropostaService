Proposta & Contratação — Hexagonal + MassTransit (Kafka) + MySQL

Dois microserviços em Arquitetura Hexagonal (Ports & Adapters):

- PropostaService: cria/atualiza propostas e publica eventos:
  - PropostaCriada → tópico propostas-created
  - PropostaStatusAlterado → tópico propostas-status-changed
- ContratacaoService: consome os eventos e atualiza Contrato + PropostaSnapshot.

Infra local: MySQL, Kafka (KRaft) e Kafdrop (UI).

------------------------------------------------------------
PRÉ-REQUISITOS
------------------------------------------------------------
- Docker Desktop ou Docker Engine + Compose
- .NET 8 SDK
- (Opcional) Cliente MySQL

------------------------------------------------------------
CONFIGURAÇÃO
------------------------------------------------------------
Crie um arquivo .env na raiz:
MYSQL_ROOT_PASSWORD=devroot
MYSQL_DATABASE=appdb
MYSQL_USER=app
MYSQL_PASSWORD=apppwd

KAFKA_EXTERNAL_PORT=9094
PROPOSTA_PORT=8081
CONTRATACAO_PORT=8082

------------------------------------------------------------
SUBIR INFRAESTRUTURA (DOCKER)
------------------------------------------------------------
docker compose up -d --build

- Kafdrop: http://localhost:9000
- MySQL: localhost:3306 (appdb/app/apppwd)

------------------------------------------------------------
RODAR SERVIÇOS NO VISUAL STUDIO (HOST)
------------------------------------------------------------
1. Abra a solution, marque Multiple startup projects:
   - PropostaService.Api = Start
   - ContratacaoService.Api = Start
2. Garanta BootstrapServers = localhost:9094
3. F5

------------------------------------------------------------
RODAR TUDO VIA DOCKER (OPCIONAL)
------------------------------------------------------------
docker compose up -d --build

------------------------------------------------------------
MIGRATIONS (EF CORE)
------------------------------------------------------------
# Proposta
dotnet ef migrations add Init --project PropostaService.Infrastructure --startup-project PropostaService.Api --output-dir Migrations
dotnet ef database update --project PropostaService.Infrastructure --startup-project PropostaService.Api

# Contratação
dotnet ef migrations add Init --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api --output-dir Migrations
dotnet ef database update --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api

------------------------------------------------------------
TESTES RÁPIDOS
------------------------------------------------------------
Criar proposta:
curl -X POST http://localhost:8081/propostas -H "Content-Type: application/json" -d "{\"cliente\":\"ACME\",\"valor\":1234.56}"

Alterar status:
curl -X PATCH http://localhost:8081/propostas/{propostaId}/status -H "Content-Type: application/json" -d "{\"novoStatus\":\"Aprovada\"}"

------------------------------------------------------------
TROUBLESHOOTING
------------------------------------------------------------
- Unknown topic or partition: verifique se os tópicos foram criados.
- Failed to resolve 'kafka:9092': no host → troque para localhost:9094 quando rodar no host.
- Unknown column 'p.Id': aplique migrations no banco correto.
