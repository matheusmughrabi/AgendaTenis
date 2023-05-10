using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AgendaTenis.Core.Identity.Token;

public class GeradorDeToken
{
    private readonly JwtOptions _jwtOptions;

    public GeradorDeToken(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string Gerar(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpiracaoEmSegundos),
            SigningCredentials = _jwtOptions.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
