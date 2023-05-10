using AgendaTenis.Core.Jogadores.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Core.Jogadores.AcessoDados.Mapeamentos;

public class LocalizacaoMapping : IEntityTypeConfiguration<LocalizacaoEntity>
{
    public void Configure(EntityTypeBuilder<LocalizacaoEntity> builder)
    {
        builder.ToTable("Localizacao");

        builder.HasKey(c => c.Id);

        builder
            .HasOne(s => s.Jogador)
            .WithOne(ad => ad.Localizacao)
            .HasForeignKey<JogadorEntity>(ad => ad.LocalizacaoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
