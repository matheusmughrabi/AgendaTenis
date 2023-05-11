using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Jogadores.Aplicacao.CompletarPerfil;

public class CompletarPerfilResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
