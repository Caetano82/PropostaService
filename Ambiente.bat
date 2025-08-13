@echo off
echo ==========================================
echo 🚀 Subindo infraestrutura: MySQL + Kafka + Kafdrop
echo ==========================================

REM Sobe apenas MySQL, Kafka e Kafdrop
docker compose up -d mysql kafka kafdrop

echo.
echo 🔍 Aguardando alguns segundos para inicializar...
timeout /t 5 >nul

echo.
echo ==========================================
echo 📌 Status dos serviços
echo ==========================================
docker compose ps

echo.
echo ==========================================
echo 📜 Logs Kafka (CTRL+C para sair)
echo ==========================================
docker compose logs -f kafka
