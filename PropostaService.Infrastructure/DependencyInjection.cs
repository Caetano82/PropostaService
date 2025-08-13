using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaService.Application.AlterarStatus;
using PropostaService.Application.CreateProposta;
using PropostaService.Application.GetProposta;
using PropostaService.Domain.ListPropostas;
using PropostaService.Domain.Ports;
using PropostaService.Infrastructure.Adapters;
using PropostaService.Infrastructure.Data;
using Shared.Contracts;

namespace PropostaService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(cfg.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(cfg.GetConnectionString("MySql"))));

        services.AddScoped<IPropostaRepository, PropostaRepository>();

        services.AddScoped<IEventPublisher, KafkaEventPublisher>();
        services.AddScoped<CreatePropostaHandler>();
        services.AddScoped<ListPropostasHandler>();
        services.AddScoped<GetPropostaHandler>();
        services.AddScoped<AlterarStatusHandler>();
        services.AddScoped<IPropostaStatusPublisher, KafkaPropostaStatusPublisher>();

        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, busCfg) => { });

            x.AddRider(r =>
            {
                var topicCreated = cfg["Kafka:Topics:PropostasCriadas"] ?? "propostas-created";
                var topicStatus = cfg["Kafka:Topics:PropostasStatusAlterados"] ?? "propostas-status-changed";

                r.AddProducer<PropostaCriada>(topicCreated);
                r.AddProducer<PropostaStatusAlterado>(topicStatus);

                r.UsingKafka((context, k) =>
                {
                    k.Host(cfg["Kafka:BootstrapServers"]);
                });
            });
        });

        return services;
    }
}
