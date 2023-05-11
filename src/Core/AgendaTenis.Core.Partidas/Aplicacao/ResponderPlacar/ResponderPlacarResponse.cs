using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderPlacar;

public class ResponderPlacarResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
