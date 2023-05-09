using System.Reflection;

namespace AgendaTenis.WebApi.ContainerDI;

public static class Mediator
{
    public static void RegistrarMediator(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.Load("AgendaTenis.Core.Identity")));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.Load("AgendaTenis.Core.Jogadores")));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.Load("AgendaTenis.Core.Partidas")));
    }
}
