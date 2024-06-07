using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolos2_3tabele.Entities.Configs;

public class MuzykEfConfig : IEntityTypeConfiguration<Muzyk>
{
    public void Configure(EntityTypeBuilder<Muzyk> builder)
    {
        builder.HasKey(m => m.IdMuzyk).HasName("IdMuzyk");
        builder.Property(m => m.IdMuzyk).UseIdentityColumn();

        builder.Property(m => m.Imie).IsRequired().HasMaxLength(30);
        builder.Property(m => m.Nazwisko).IsRequired().HasMaxLength(40);
        builder.Property(m => m.Pseudonim).HasMaxLength(50);

        builder.HasMany(t => t.IdUtwory)
            .WithMany(t => t.IdMuzycy)
            .UsingEntity<Dictionary<string, object>>(
                "WykonawcaUtworu",
                j => j.HasOne<Utwor>().WithMany().HasForeignKey("IdUtwor"),
                j => j.HasOne<Muzyk>().WithMany().HasForeignKey("IdMuzyk")).HasData(
                new { IdMuzyk = 1, IdUtwor = 1 },
                new { IdMuzyk = 1, IdUtwor = 2 },
                new { IdMuzyk = 2, IdUtwor = 3 });
        

        builder.ToTable("Muzyk");

        Muzyk[] muzyks =
        {
            new Muzyk()
            {
                IdMuzyk = 1, Imie = "Adam", Nazwisko = "Nowak", Pseudonim = "AdNo"
            },
            new Muzyk()
            {
                IdMuzyk = 2, Imie = "Alicja", Nazwisko = "Malinowska"
            }
        };

        builder.HasData(muzyks);

        
    }
}