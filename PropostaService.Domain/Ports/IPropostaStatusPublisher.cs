using Shared.Contracts;

namespace PropostaService.Domain.Ports;

public interface IPropostaStatusPublisher
{
    Task PublishAsync(PropostaStatusAlterado evt, CancellationToken ct);
}
