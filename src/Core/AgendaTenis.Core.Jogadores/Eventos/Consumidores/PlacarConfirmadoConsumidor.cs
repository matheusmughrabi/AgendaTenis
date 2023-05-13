using AgendaTenis.BuildingBlocks.EventBus.Base;
using AgendaTenis.BuildingBlocks.EventBus.Servicos;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using AgendaTenis.BuildingBlocks.EventBus.Constantes;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Text;
using AgendaTenis.BuildingBlocks.EventBus.Mensagens;
using System.Text.Json;
using AgendaTenis.Core.Jogadores.Aplicacao.AtualizarPontuacao;

namespace AgendaTenis.Core.Jogadores.Eventos.Consumidores;

public class PlacarConfirmadoConsumidor : IEventConsumer
{
    private const string EXCHANGENAME = ExchangeConstantes.PartidaExchange;
    private const string QUEUENAME = QueueConstantes.Jogadores_PlacarConfirmado_Queue;

    private readonly IMessageBus _messageBus;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PlacarConfirmadoConsumidor(
        IMessageBus messageBus,
        IServiceScopeFactory serviceScopeFactory)
    {
        _messageBus = messageBus;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(CancellationToken stoppingToken)
    {
        using (var connection = _messageBus.GetConnection())
        using (var channel = _messageBus.GetChannel(connection))
        {
            channel.ExchangeDeclare(exchange: EXCHANGENAME, type: ExchangeType.Fanout, durable: true, autoDelete: false);
            var queueArgs = new Dictionary<string, object>
            {
                { "x-max-delivery-attempts", 3 } // Define o máximo de 3 retentativas
            };
            channel.QueueDeclare(queue: QUEUENAME, durable: true, exclusive: false, autoDelete: false, arguments: queueArgs);
            channel.QueueBind(queue: QUEUENAME, exchange: EXCHANGENAME, routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IMediator scopedMediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Se a mensagem não chegar no formato correto, então vamos rejeitar e nem devolver pra fila
                    PlacarConfirmadoMensagem objetoDaMensagem;
                    try
                    {
                        objetoDaMensagem = JsonSerializer.Deserialize<PlacarConfirmadoMensagem>(message);
                    }
                    catch (Exception)
                    {
                        channel.BasicReject(ea.DeliveryTag, false);
                        throw;
                    }

                    // Se der tudo certo, então vamos fazer o ack manual.
                    // Se a chamada ao AtualizarPontuacaoCommand lançar alguma exception, então não vamos fazer o ack e vamos fazer o reque para tentar de novo
                    try
                    {
                        var vencedorId = objetoDaMensagem.VencedorId;
                        var perdedorId = objetoDaMensagem.DesafianteId == objetoDaMensagem.VencedorId ? objetoDaMensagem.AdversarioId : objetoDaMensagem.DesafianteId;

                        var command = new AtualizarPontuacaoCommand()
                        {
                            VencedorId = Guid.Parse(vencedorId),
                            PerdedorId = Guid.Parse(perdedorId),
                        };

                        await scopedMediator.Send(command);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                        throw;
                    }


                }
            };

            channel.BasicConsume(queue: QUEUENAME,
                                 autoAck: false,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
