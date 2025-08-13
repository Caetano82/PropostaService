using PropostaService.Domain.Entities;

namespace PropostaService.Domain.Ports;

public interface IPropostaRepository
{
    Task AddAsync(Proposta proposta, CancellationToken ct);
    Task<Proposta?> GetByIdAsync(Guid id, CancellationToken ct);
}
