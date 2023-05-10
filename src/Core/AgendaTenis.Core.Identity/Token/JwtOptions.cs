using Microsoft.IdentityModel.Tokens;

namespace AgendaTenis.Core.Identity.Token;

public class JwtOptions
{
    public SigningCredentials SigningCredentials { get; set; }
    public int ExpiracaoEmSegundos { get; set; }
}
