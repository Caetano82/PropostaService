using PropostaService.Domain.Ports;
using Shared.Contracts;

namespace PropostaService.Application.AlterarStatus;

public class AlterarStatusHandler
{
    private readonly IPropostaRepository _repo;
    private readonly IPropostaStatusPublisher _statusPublisher;

    public AlterarStatusHandler(IPropostaRepository repo, IPropostaStatusPublisher statusPublisher)
    {
        _repo = repo;
        _statusPublisher = statusPublisher;
    }

    public async Task HandleAsync(AlterarStatusCommand cmd, CancellationToken ct)
    {
        var proposta = await _repo.GetByIdAsync(cmd.PropostaId, ct)
            ?? throw new KeyNotFoundException("Proposta não encontrada");

        proposta.AlterarStatus(cmd.NovoStatus);
        await _repo.UpdateAsync(proposta, ct);

        var evt = new PropostaStatusAlterado(proposta.Id, proposta.Status, DateTime.UtcNow);
        await _statusPublisher.PublishAsync(evt, ct);
    }
}
