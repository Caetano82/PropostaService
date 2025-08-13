using Microsoft.EntityFrameworkCore;
using PropostaService.Domain.Entities;
using PropostaService.Domain.Ports;
using PropostaService.Infrastructure.Data;

namespace PropostaService.Infrastructure.Adapters;

public class PropostaRepository : IPropostaRepository
{
    private readonly AppDbContext _db;
    public PropostaRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Proposta proposta, CancellationToken ct)
    {
        _db.Propostas.Add(proposta);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Proposta?> GetByIdAsync(Guid id, CancellationToken ct)
        => _db.Propostas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
}
