using AgendaTenis.Core.Jogadores.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Core.Jogadores.AcessoDados.Mapeamentos;

public class PontuacaoMapping : IEntityTypeConfiguration<PontuacaoEntity>
{
    public void Configure(EntityTypeBuilder<PontuacaoEntity> builder)
    {
        builder.ToTable("Pontuacao");

        builder.HasKey(c => c.Id);

        builder
          .HasOne(pontuacao => pontuacao.Jogador)
          .WithOne(jogador => jogador.Pontuacao)
          .HasForeignKey<JogadorEntity>(jogador => jogador.PontuacaoId)
          .OnDelete(DeleteBehavior.NoAction);
    }
}
