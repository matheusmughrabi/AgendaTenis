using Microsoft.AspNetCore.Identity;

namespace AgendaTenis.Core.Identity.Utils;

public interface ISenhaHasher<TObjeto> where TObjeto : class
{
    string HashSenha(TObjeto objeto, string senha);
    bool VerificarHashSenha(TObjeto accountEntity, string hashedPassword, string providedPassword);
}

public class SenhaHasher<TObjeto> : ISenhaHasher<TObjeto> where TObjeto : class
{
    private readonly IPasswordHasher<TObjeto> _passwordHasher;

    public SenhaHasher(IPasswordHasher<TObjeto> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashSenha(TObjeto objeto, string senha)
    {
        return _passwordHasher.HashPassword(objeto, senha);
    }

    public bool VerificarHashSenha(TObjeto objeto, string hashedPassword, string providedPassword)
    {
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(objeto, hashedPassword, providedPassword);

        if (passwordVerificationResult == PasswordVerificationResult.Success)
            return true;

        return false;
    }
}
