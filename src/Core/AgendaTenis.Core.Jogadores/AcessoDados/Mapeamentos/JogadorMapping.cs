using AgendaTenis.Core.Jogadores.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Core.Identity.AcessoDados.Mapeamentos;

public class JogadorMapping : IEntityTypeConfiguration<JogadorEntity>
{
    public void Configure(EntityTypeBuilder<JogadorEntity> builder)
    {
        builder.ToTable("Jogador");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
             .IsRequired()
             .HasColumnName("Nome")
             .HasColumnType("nvarchar(100)");

        builder.Property(c => c.Sobrenome)
             .IsRequired()
             .HasColumnName("Nome")
             .HasColumnType("nvarchar(100)");

        builder
            .HasOne(s => s.Localizacao)
            .WithOne(ad => ad.Jogador)
            .HasForeignKey<LocalizacaoEntity>(ad => ad.JogadorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(s => s.CaracteristicaDeJogo)
            .WithOne(ad => ad.Jogador)
            .HasForeignKey<CaracteristicaDeJogoEntity>(ad => ad.JogadorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
