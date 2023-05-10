using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Identity.Aplicacao.CriarConta;

public class CriarContaResponse
{
    public Guid? Id { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
