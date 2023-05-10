using AgendaTenis.Core.Identity.Dominio;

namespace AgendaTenis.Core.Identity.AcessoDados.Repositorios;

public interface IIdentityRepositorio
{
    Task<bool> EmailJaExiste(string email);
    Task<Guid> Inserir(UsuarioEntity entity);
    Task<UsuarioEntity> ObterUsuario(string email);
}
