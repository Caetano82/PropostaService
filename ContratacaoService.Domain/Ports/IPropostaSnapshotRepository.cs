using ContratacaoService.Domain.Entities;

namespace ContratacaoService.Domain.Ports;

public interface IPropostaSnapshotRepository
{
    Task UpsertAsync(PropostaSnapshot snapshot, CancellationToken ct);
    Task<PropostaSnapshot?> GetAsync(Guid propostaId, CancellationToken ct);
}
