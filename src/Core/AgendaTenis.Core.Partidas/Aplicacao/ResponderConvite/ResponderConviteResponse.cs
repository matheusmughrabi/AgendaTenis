using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderConvite;

public class ResponderConviteResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
