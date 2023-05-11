using AgendaTenis.Core.Partidas.Enums;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvidarParaPartida;

public class ConvidarParaPartidaCommand : IRequest<ConvidarParaPartidaResponse>
{
    public Guid DesafianteId { get; set; }
    public Guid AdversarioId { get; set; }
    public DateTime DataDaPartida { get; set; }
    public string DescricaoLocal { get; set; }
    public ModeloPartidaEnum ModeloDaPartida { get; set; }
}
