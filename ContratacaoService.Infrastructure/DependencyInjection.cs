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

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(cfg.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(cfg.GetConnectionString("MySql"))));
        
        services.AddScoped<IContratoRepository, ContratoRepository>();
        services.AddScoped<IPropostaSnapshotRepository, PropostaSnapshotRepository>(); 
        services.AddScoped<ProcessarPropostaHandler>();
        services.AddScoped<ContratarPropostaHandler>();


        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, busCfg) => { });

            x.AddRider(r =>
            {
                r.AddConsumer<PropostaCriadaConsumer>();
                r.AddConsumer<PropostaStatusAlteradoConsumer>();

                r.UsingKafka((context, k) =>
                {
                    k.Host(cfg["Kafka:BootstrapServers"]);

                    var topicCreated = cfg["Kafka:Topics:PropostasCriadas"] ?? "propostas-created";
                    var topicStatus = cfg["Kafka:Topics:PropostasStatusAlterados"] ?? "propostas-status-changed";

                    k.TopicEndpoint<PropostaCriada>(
                        topicCreated,
                        "contratacao-service-proposta-criada",
                        e =>
                        {
                            e.ConfigureConsumer<PropostaCriadaConsumer>(context);
                            e.CreateIfMissing(o => { o.NumPartitions = 3; o.ReplicationFactor = 1; });
                        });

                    k.TopicEndpoint<PropostaStatusAlterado>(
                        topicStatus,
                        "contratacao-service-status-alterado",
                        e =>
                        {
                            e.ConfigureConsumer<PropostaStatusAlteradoConsumer>(context);
                            e.CreateIfMissing(o => { o.NumPartitions = 3; o.ReplicationFactor = 1; });
                        });
                });
            });
        });

        return services;
    }
}
