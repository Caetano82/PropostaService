using ContratacaoService.Application.ProcessarProposta;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Infrastructure.Adapters;
using ContratacaoService.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts;

namespace ContratacaoService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(cfg.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(cfg.GetConnectionString("MySql"))));

        services.AddScoped<IContratoRepository, ContratoRepository>();
        services.AddScoped<ProcessarPropostaHandler>();

        services.AddMassTransit(x =>
        {

            x.UsingInMemory((context, busCfg) => {});

            x.AddRider(r =>
            {
              
                r.AddConsumer<PropostaCriadaConsumer>();

                r.UsingKafka((context, k) =>
                {
                    k.Host(cfg["Kafka:BootstrapServers"]);

                    k.TopicEndpoint<PropostaCriada>(
                        cfg["Kafka:Topics:Orders"] ?? "orders",
                        "contratacao-service",
                        e =>
                        {
                            e.ConfigureConsumer<PropostaCriadaConsumer>(context);
                        });
                });
            });
        });

        return services;
    }
}
