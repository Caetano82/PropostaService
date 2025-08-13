using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using PropostaService.Domain.Entities;

namespace PropostaService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    public DbSet<Proposta> Propostas => Set<Proposta>();

    // Update-Database -Project PropostaService.Infrastructure -StartupProject PropostaService.Api

   // Add-Migration v1 -Project PropostaService.Infrastructure -StartupProject PropostaService.Api -OutputDir Migrations




    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Proposta>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Cliente).IsRequired().HasMaxLength(200);
            e.Property(x => x.Valor).HasColumnType("decimal(18,2)");
            e.Property(x => x.Status).HasMaxLength(50);
        });
    }
}
