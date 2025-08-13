using PropostaService.Domain.Ports;

namespace PropostaService.Application.AlterarStatus;

public class AlterarStatusHandler
{
    private readonly IPropostaRepository _repo;
    public AlterarStatusHandler(IPropostaRepository repo) => _repo = repo;

    public async Task HandleAsync(AlterarStatusCommand cmd, CancellationToken ct)
    {
        var proposta = await _repo.GetByIdAsync(cmd.PropostaId, ct)
            ?? throw new KeyNotFoundException("Proposta não encontrada");

        proposta.AlterarStatus(cmd.NovoStatus);
        await _repo.UpdateAsync(proposta, ct);
    }
}
