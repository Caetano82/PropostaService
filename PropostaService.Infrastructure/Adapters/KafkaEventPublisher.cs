using MassTransit;
using PropostaService.Domain.Ports;
using Shared.Contracts;

namespace PropostaService.Infrastructure.Adapters;

public class KafkaEventPublisher : IEventPublisher
{
    private readonly ITopicProducer<PropostaCriada> _createdProducer;
    private readonly ITopicProducer<PropostaStatusAlterado> _statusProducer;

    public KafkaEventPublisher(
        ITopicProducer<PropostaCriada> createdProducer,
        ITopicProducer<PropostaStatusAlterado> statusProducer)
    {
        _createdProducer = createdProducer;
        _statusProducer = statusProducer;
    }

    public Task PublishAsync(PropostaCriada evt, CancellationToken ct) => _createdProducer.Produce(evt, ct);
    public Task PublishAsync(PropostaStatusAlterado evt, CancellationToken ct) => _statusProducer.Produce(evt, ct);
}
