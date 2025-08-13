using ContratacaoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    public DbSet<Contrato> Contratos => Set<Contrato>();
}
