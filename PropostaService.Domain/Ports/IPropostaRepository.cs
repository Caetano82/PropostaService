using PropostaService.Domain.Entities;

namespace PropostaService.Domain.Ports;

public interface IPropostaRepository
{
    Task AddAsync(Proposta proposta, CancellationToken ct);
    Task<Proposta?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Proposta>> ListAsync(int? take, int? skip, CancellationToken ct);
    Task UpdateAsync(Proposta proposta, CancellationToken ct);
}
