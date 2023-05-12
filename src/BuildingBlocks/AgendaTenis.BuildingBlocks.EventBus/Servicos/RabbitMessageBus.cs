﻿using AgendaTenis.BuildingBlocks.EventBus.Configuracao;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AgendaTenis.BuildingBlocks.EventBus.Servicos;

public class RabbitMessageBus : IMessageBus
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMQConfiguracao _rabbitMQConfig;

    public RabbitMessageBus(IOptions<RabbitMQConfiguracao> rabbitMQConfig)
    {
        _rabbitMQConfig = rabbitMQConfig.Value;
    }

    public IConnection GetConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQConfig.Host,
            Port = int.Parse(_rabbitMQConfig.Port),
            UserName = _rabbitMQConfig.Username,
            Password = _rabbitMQConfig.Password,
            ClientProvidedName = _rabbitMQConfig.CliendProvidedName
        };

        return factory.CreateConnection();
    }

    public IModel GetChannel(IConnection connection)
    {
        return connection.CreateModel();
    }

    public void Consume()
    {

    }
}
