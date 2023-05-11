using AgendaTenis.BuildingBlocks.Notificacoes;

namespace AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;

public class RegistrarPlacarResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
