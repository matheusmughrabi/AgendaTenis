using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.HistoricoDePartidas;

public class ObterHistoricoDePartidasCommand : IRequest<ObterHistoricoDePartidasResponse>
{
    public string UsuarioId { get; set; }
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }
}
