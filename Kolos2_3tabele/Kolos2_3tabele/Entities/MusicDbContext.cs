using Kolos2_3tabele.Entities.Configs;
using Microsoft.EntityFrameworkCore;

namespace Kolos2_3tabele.Entities;

public class MusicDbContext : DbContext
{
    public virtual DbSet<Utwor> Utwory { get; set; }
    public virtual DbSet<Muzyk> Muzycy { get; set; }

    public MusicDbContext()
    {
    }

    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MuzykEfConfig).Assembly);
    }
}