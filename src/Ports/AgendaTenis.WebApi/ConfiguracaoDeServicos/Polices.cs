using AgendaTenis.WebApi.Polices;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Polices
{
    public static void RegistrarPolices(this IServiceCollection services)
    {
        services.AddScoped<DesafianteDaPartidaPoliceHandler>();
        services.AddScoped<AdversarioDaPartidaPoliceHandler>();
    }
}
