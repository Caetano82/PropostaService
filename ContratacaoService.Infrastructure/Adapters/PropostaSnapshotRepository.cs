using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Adapters;

public class PropostaSnapshotRepository : IPropostaSnapshotRepository
{
    private readonly AppDbContext _db;
    public PropostaSnapshotRepository(AppDbContext db) => _db = db;

    public async Task UpsertAsync(PropostaSnapshot snapshot, CancellationToken ct)
    {
        var existing = await _db.PropostasSnapShot.FindAsync(new object?[] { snapshot.PropostaId }, ct);
        if (existing is null)
            _db.PropostasSnapShot.Add(snapshot);
        else
            _db.Entry(existing).CurrentValues.SetValues(snapshot);

        await _db.SaveChangesAsync(ct);
    }

    public Task<PropostaSnapshot?> GetAsync(Guid propostaId, CancellationToken ct)
        => _db.PropostasSnapShot.AsNoTracking().FirstOrDefaultAsync(p => p.PropostaId == propostaId, ct);
}
