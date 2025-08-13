namespace Shared.Contracts;

public interface IEventPublisher
{
    Task PublishAsync(PropostaCriada evt, CancellationToken ct);
    Task PublishAsync(PropostaStatusAlterado evt, CancellationToken ct);
}
