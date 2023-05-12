using AgendaTenis.WebApi.Bootstrapper;
using AgendaTenis.WebApi.Bootstrapper.Tipos;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Bootstrapper
{
    public static void RegistrarBootstrappers(this IServiceCollection services)
    {
        services.AddScoped<BootstrapperService>();
        services.AddScoped<IBootstrapper, IdentityBootstrapper>();
    }
}
