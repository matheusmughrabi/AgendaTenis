using AgendaTenis.Core.Jogadores.AcessoDados;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Jogadores
{
    public static void RegistrarJogadores(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JogadoresDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Jogadores")));
    }
}
