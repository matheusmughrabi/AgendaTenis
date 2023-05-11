using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvidarParaPartida;

public class ConvidarParaPartidaResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
