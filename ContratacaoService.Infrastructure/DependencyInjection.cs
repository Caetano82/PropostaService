using ContratacaoService.Application.Contratar;
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
        // DbContext
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(cfg.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(cfg.GetConnectionString("MySql"))));

        // Repositórios e handlers
        services.AddScoped<IContratoRepository, ContratoRepository>();
        services.AddScoped<IPropostaSnapshotRepository, PropostaSnapshotRepository>(); // essencial
        services.AddScoped<ProcessarPropostaHandler>();
        services.AddScoped<ContratarPropostaHandler>();

        // MassTransit (bus in-memory + rider Kafka)
        services.AddMassTransit(x =>
        {
            // Bus para resolver IBus (não usado para tráfego; apenas dependências internas)
            x.UsingInMemory((context, busCfg) => { });

            x.AddRider(r =>
            {
                // Consumers REGISTRADOS no rider
                r.AddConsumer<PropostaCriadaConsumer>();
                r.AddConsumer<PropostaStatusAlteradoConsumer>();

                r.UsingKafka((context, k) =>
                {
                    k.Host(cfg["Kafka:BootstrapServers"]);

                    var topic = cfg["Kafka:Topics:PropostasEvents"] ?? "propostas-events";

                    // >>> groupIds DIFERENTES para evitar "same key"
                    k.TopicEndpoint<PropostaCriada>(
                        topic,
                        "contratacao-service-proposta-criada",
                        e => e.ConfigureConsumer<PropostaCriadaConsumer>(context));

                    k.TopicEndpoint<PropostaStatusAlterado>(
                        topic,
                        "contratacao-service-status-alterado",
                        e => e.ConfigureConsumer<PropostaStatusAlteradoConsumer>(context));
                });
            });
        });

        return services;
    }
}
