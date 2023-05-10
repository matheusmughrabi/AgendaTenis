using AgendaTenis.Core.Identity.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Identity.AcessoDados.Repositorios;

public class IdentityRepositorio : IIdentityRepositorio
{
    private readonly IdentityDbContext _context;

    public IdentityRepositorio(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailJaExiste(string email)
    {
        return await _context.Usuario
            .AsNoTracking()
            .AnyAsync(usuario => usuario.Email == email);
    }

    public async Task<Guid> Inserir(UsuarioEntity entity)
    {
        var resultado = (await _context.Usuario
            .AddAsync(entity)).Entity;

        await _context.SaveChangesAsync();

        return resultado.Id;
    }

    public async Task<UsuarioEntity> ObterUsuario(string email)
    {
        return await _context.Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
    }
}
