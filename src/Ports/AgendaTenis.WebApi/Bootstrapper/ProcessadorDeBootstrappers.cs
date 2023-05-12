using AgendaTenis.WebApi.Bootstrapper.Tipos;

namespace AgendaTenis.WebApi.Bootstrapper;

public class ProcessadorDeBootstrappers
{
    private IEnumerable<IBootstrapper> _bootstrappers;

    public ProcessadorDeBootstrappers(IEnumerable<IBootstrapper> bootstrappers)
    {
        _bootstrappers = bootstrappers;
    }

    public void Executar()
    {
        foreach (var bootstrapper in _bootstrappers)
        {
            bootstrapper.Inicializar();
        }
    }
}
