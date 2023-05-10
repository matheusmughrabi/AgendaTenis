using AgendaTenis.Core.Jogadores.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Core.Jogadores.AcessoDados.Mapeamentos;

public class CaracteristicaDeJogoMapping : IEntityTypeConfiguration<CaracteristicaDeJogoEntity>
{
    public void Configure(EntityTypeBuilder<CaracteristicaDeJogoEntity> builder)
    {
        builder.ToTable("CaracteristicaDeJogo");

        builder.HasKey(c => c.Id);

        builder
            .HasOne(s => s.Jogador)
            .WithOne(ad => ad.CaracteristicaDeJogo)
            .HasForeignKey<JogadorEntity>(ad => ad.CaracteristicaDeJogoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
