using Shared.Contracts;

namespace PropostaService.Domain.Ports;

public interface IEventPublisher
{
    Task PublishAsync(PropostaCriada evt, CancellationToken ct);
}
