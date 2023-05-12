using AgendaTenis.BuildingBlocks.EventBus.Configuracao;
using AgendaTenis.BuildingBlocks.EventBus.Servicos;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class MessageBus
{
    public static void RegistrarMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQConfiguracao>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<IMessageBus, RabbitMessageBus>();
    }
}
