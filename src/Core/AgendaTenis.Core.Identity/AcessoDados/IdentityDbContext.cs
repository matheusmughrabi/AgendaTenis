using AgendaTenis.Core.Identity.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Identity.AcessoDados;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    public DbSet<UsuarioEntity> Usuario { get; set; }
}
