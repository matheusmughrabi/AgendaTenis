using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Identity.Aplicacao.GerarToken;

public class GerarTokenResponse
{
    public string Token { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
