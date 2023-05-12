using AgendaTenis.Core.Identity.AcessoDados.Repositorios;
using AgendaTenis.Core.Identity.AcessoDados;
using AgendaTenis.Core.Identity.Dominio;
using AgendaTenis.Core.Identity.Token;
using AgendaTenis.Core.Identity.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Identity
{
    public static void RegistrarIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityRepositorio, IdentityRepositorio>();
        services.AddScoped<ISenhaHasher<UsuarioEntity>, SenhaHasher<UsuarioEntity>>();
        services.AddScoped<IPasswordHasher<UsuarioEntity>, PasswordHasher<UsuarioEntity>>();

        var section = configuration.GetSection(nameof(JwtOptions));
        var chave = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(section["ChaveSecreta"]));

        services.AddScoped(c => new JwtOptions()
        {
            ExpiracaoEmSegundos = int.Parse(section["ExpiracaoEmSegundos"]),
            SigningCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256)
        });

        services.AddScoped<GeradorDeToken>();

        services.AddDbContext<IdentityDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("Identity")));

    }
}
