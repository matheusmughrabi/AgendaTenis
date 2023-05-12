using AgendaTenis.BuildingBlocks.EventBus.Base;

namespace AgendaTenis.BuildingBlocks.EventBus.Mensagens;

public class PlacarConfirmadoMensagem : IEventMessage
{
    public string Id { get; set; }
    public string DesafianteId { get; set; }
    public string AdversarioId { get; set; }
    public DateTime DataDaPartida { get; set; }
    public int ModeloDaPartida { get; set; }
    public string? VencedorId { get; set; }
}
