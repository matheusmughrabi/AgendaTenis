using AgendaTenis.Core.Identity.AcessoDados.Mapeamentos;
using AgendaTenis.Core.Jogadores.AcessoDados.Mapeamentos;
using AgendaTenis.Core.Jogadores.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Jogadores.AcessoDados;

public class JogadoresDbContext : DbContext
{
    public JogadoresDbContext(DbContextOptions<JogadoresDbContext> options) : base(options)
    {
    }

    public DbSet<JogadorEntity> Jogador { get; set; }
    public DbSet<PontuacaoEntity> Pontuacao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new JogadorMapping());
        modelBuilder.ApplyConfiguration(new PontuacaoMapping());

        base.OnModelCreating(modelBuilder);
    }
}
