using AgendaTenis.Core.Jogadores.Eventos.Consumidores;

namespace AgendaTenis.WebApi.Workers;

public class PlacarConfirmadoWorker : BackgroundService
{
    private readonly PlacarConfirmadoConsumidor _consumidor;

    public PlacarConfirmadoWorker(PlacarConfirmadoConsumidor consumidor)
    {
        _consumidor = consumidor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumidor.Consume(stoppingToken);
    }
}
