using MassTransit;
using PropostaService.Domain.Ports;
using Shared.Contracts;

namespace PropostaService.Infrastructure.Adapters;

public class KafkaEventPublisher : IEventPublisher
{
    private readonly ITopicProducer<PropostaCriada> _producer;
    public KafkaEventPublisher(ITopicProducer<PropostaCriada> producer) => _producer = producer;

    public Task PublishAsync(PropostaCriada evt, CancellationToken ct) => _producer.Produce(evt, ct);
}
