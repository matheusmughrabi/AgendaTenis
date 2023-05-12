using AgendaTenis.Core.Partidas.Dominio;
using AgendaTenis.Core.Partidas.Enums;

namespace AgendaTenis.Core.Partidas.Repositorios;

public interface IPartidasRepositorio
{
    Task<Partida> ObterPorIdAsync(string id);
    Task<List<Partida>> ObterPartidasPendentes(string usuarioId, StatusConviteEnum? statusConvite = null);
    Task<List<Partida>> ObterConfirmacoesDePlacarPendentes(string usuarioId);
    Task<List<Partida>> ObterPartidasPaginado(string usuarioId, int pagina, int itemsPorPagina);
    Task InsertAsync(Partida partida);
    Task<bool> Update(Partida partida);
}
