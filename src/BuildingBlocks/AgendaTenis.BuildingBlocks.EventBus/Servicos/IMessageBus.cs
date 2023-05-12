using RabbitMQ.Client;

namespace AgendaTenis.BuildingBlocks.EventBus.Servicos;

public interface IMessageBus
{
    public IConnection GetConnection();
    IModel GetChannel(IConnection connection);
}
