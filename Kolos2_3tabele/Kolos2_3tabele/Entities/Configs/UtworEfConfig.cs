using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolos2_3tabele.Entities.Configs;

public class UtworEfConfig : IEntityTypeConfiguration<Utwor>
{
    public void Configure(EntityTypeBuilder<Utwor> builder)
    {
        builder.HasKey(u => u.IdUtwor).HasName("IdUtwor");
        builder.Property(u => u.IdUtwor).UseIdentityColumn();

        builder.Property(u => u.NazwaUtworu).IsRequired().HasMaxLength(30);
        builder.Property(u => u.CzasTrwania).IsRequired();

        builder.ToTable("Utwor");

        Utwor[] utwors =
        {
            new Utwor()
            {
                IdUtwor = 1, NazwaUtworu = "autor1utwor1", CzasTrwania = 2.20F
            },
            new Utwor()
            {
                IdUtwor = 2, NazwaUtworu = "autor1utwor2", CzasTrwania = 3.42F
            },
            new Utwor()
            {
                IdUtwor = 3, NazwaUtworu = "autor2utwor1", CzasTrwania = 5.50F
            }
        };

        builder.HasData(utwors);
    }
}