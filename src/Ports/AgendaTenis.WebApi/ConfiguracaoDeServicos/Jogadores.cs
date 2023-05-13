using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Eventos.Consumidores;
using AgendaTenis.WebApi.Workers;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Jogadores
{
    public static void RegistrarJogadores(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JogadoresDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Jogadores")));

        services.AddHostedService<PlacarConfirmadoWorker>();
        services.AddSingleton<PlacarConfirmadoConsumidor>();
    }
}
