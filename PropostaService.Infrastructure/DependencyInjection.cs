using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaService.Application.CreateProposta;
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

        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, busCfg) => { /* busCfg.ConfigureEndpoints(context); */ });

            x.AddRider(r =>
            {
                r.AddProducer<PropostaCriada>(cfg["Kafka:Topics:Orders"] ?? "orders");

                r.UsingKafka((context, k) =>
                {
                    k.Host(cfg["Kafka:BootstrapServers"]);
                });
            });
        });

        return services;
    }
}
