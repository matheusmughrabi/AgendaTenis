using AgendaTenis.Core.Partidas.Eventos.Publishers;
using AgendaTenis.Core.Partidas.Repositorios;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Partidas
{
    public static void RegistrarPartidas(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoClient>(c =>
        {
            return new MongoClient(configuration.GetConnectionString("Partidas"));
        });

        services.AddScoped<IPartidasRepositorio, PartidasRepositorio>();

        services.AddScoped<PlacarConfirmadoPublisher>();
    }
}
