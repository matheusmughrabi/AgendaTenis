using AgendaTenis.WebApi.Bootstrapper.Tipos;

namespace AgendaTenis.WebApi.Bootstrapper;

public class BootstrapperService
{
    private IEnumerable<IBootstrapper> _bootstrappers;

    public BootstrapperService(IEnumerable<IBootstrapper> bootstrappers)
    {
        _bootstrappers = bootstrappers;
    }

    public void Executar()
    {
        foreach (var bootstrapper in _bootstrappers)
        {
            bootstrapper.Inicializar().GetAwaiter().GetResult();    
        }
    }
}
