namespace AgendaTenis.BuildingBlocks.EventBus.Base;

public interface IEventConsumer
{
    Task Consume(CancellationToken stoppingToken);
}
