using ContratacaoService.Domain.Entities;

namespace ContratacaoService.Domain.Ports;

public interface IContratoRepository
{
    Task AddAsync(Contrato contrato, CancellationToken ct);
}