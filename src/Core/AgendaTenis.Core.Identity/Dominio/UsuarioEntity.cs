using AgendaTenis.Core.Identity.Utils;
using AgendaTenis.Core.Identity.Validators;
using FluentValidation;

namespace AgendaTenis.Core.Identity.Dominio;

public class UsuarioEntity
{
    private readonly UsuarioEntityValidator _usuarioEntityValidator = new UsuarioEntityValidator();
    private readonly ISenhaHasher<UsuarioEntity> _hasher;

    private UsuarioEntity() { }

    public UsuarioEntity(string email, string senha, ISenhaHasher<UsuarioEntity> hasher)
    {
        _usuarioEntityValidator.ValidateAndThrow(this);
        _hasher = hasher;

        Id = Guid.NewGuid();
        Email = email;
        Senha = _hasher.HashSenha(this, senha);
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
}

public class UsuarioEntityValidator : AbstractValidator<UsuarioEntity>
{
    public UsuarioEntityValidator()
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator());

        RuleFor(x => x.Senha)
            .SetValidator(new SenhaValidator());
    }
}
