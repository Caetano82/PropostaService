using ContratacaoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    public DbSet<Contrato> Contratos => Set<Contrato>();
    public DbSet<PropostaSnapshot> Propostas => Set<PropostaSnapshot>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Contrato>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.PropostaId).IsRequired();
            e.Property(x => x.DataUtc).IsRequired();
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
        });

        mb.Entity<PropostaSnapshot>(e =>
        {
            e.HasKey(x => x.PropostaId);
            e.Property(x => x.Cliente).IsRequired().HasMaxLength(200);
            e.Property(x => x.Valor).HasColumnType("decimal(18,2)");
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            e.Property(x => x.UpdatedAtUtc).IsRequired();
        });
    }
}
