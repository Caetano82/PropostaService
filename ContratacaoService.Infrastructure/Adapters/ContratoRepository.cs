using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Infrastructure.Data;

namespace ContratacaoService.Infrastructure.Adapters;

public class ContratoRepository : IContratoRepository
{
    private readonly AppDbContext _db;
    public ContratoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Contrato contrato, CancellationToken ct)
    {
        _db.Contratos.Add(contrato);
        await _db.SaveChangesAsync(ct);
    }
}
