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
             .HasColumnName("Sobrenome")
             .HasColumnType("nvarchar(100)");

        builder
            .HasOne(jogador => jogador.Pontuacao)
            .WithOne(pontuacao => pontuacao.Jogador)
            .HasForeignKey<PontuacaoEntity>(pontuacao => pontuacao.JogadorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
