using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using Shared.Contracts;


namespace ContratacaoService.Application.Contratar;

public class ContratarPropostaHandler
{
    private readonly IPropostaSnapshotRepository _snapshots;
    private readonly IContratoRepository _repo;

    public ContratarPropostaHandler(IPropostaSnapshotRepository snapshots, IContratoRepository repo)
    {
        _snapshots = snapshots;
        _repo = repo;
    }

    public async Task<ContratarPropostaResult> HandleAsync(ContratarPropostaCommand cmd, CancellationToken ct)
    {
        var snap = await _snapshots.GetAsync(cmd.PropostaId, ct)
            ?? throw new KeyNotFoundException("Proposta não encontrada (ainda não recebemos eventos)");

        if (snap.Status != PropostaStatus.Aprovada)
            throw new InvalidOperationException($"Não é possível contratar. Status atual: {snap.Status}");

        var contrato = new Contrato(snap.PropostaId);
        await _repo.AddAsync(contrato, ct);

        return new ContratarPropostaResult(contrato.Id, contrato.PropostaId, contrato.DataContratacaoUtc);
    }
}
