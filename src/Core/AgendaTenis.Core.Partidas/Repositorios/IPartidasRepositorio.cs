using AgendaTenis.Core.Partidas.Dominio;

namespace AgendaTenis.Core.Partidas.Repositorios;

public interface IPartidasRepositorio
{
    Task<Partida> ObterPorIdAsync(string id);
    Task InsertAsync(Partida partida);
    Task<bool> Update(Partida partida);
}
