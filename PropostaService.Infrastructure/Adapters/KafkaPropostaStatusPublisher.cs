using MassTransit;
using PropostaService.Domain.Ports;
using Shared.Contracts;

namespace PropostaService.Infrastructure.Adapters;

public class KafkaPropostaStatusPublisher : IPropostaStatusPublisher
{
    private readonly ITopicProducer<PropostaStatusAlterado> _producer;
    public KafkaPropostaStatusPublisher(ITopicProducer<PropostaStatusAlterado> producer) => _producer = producer;

    public Task PublishAsync(PropostaStatusAlterado evt, CancellationToken ct)
        => _producer.Produce(evt, ct);
}
