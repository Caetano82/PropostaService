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

    public async Task<List<Proposta>> ListAsync(int? take, int? skip, CancellationToken ct)
    {
        IQueryable<Proposta> q = _db.Propostas.AsNoTracking().OrderByDescending(p => p.Id);
        if (skip.HasValue && skip.Value > 0) q = q.Skip(skip.Value);
        if (take.HasValue && take.Value > 0) q = q.Take(take.Value);
        return await q.ToListAsync(ct);
    }

    public async Task UpdateAsync(Proposta proposta, CancellationToken ct)
    {
        _db.Propostas.Update(proposta);
        await _db.SaveChangesAsync(ct);
    }
}
