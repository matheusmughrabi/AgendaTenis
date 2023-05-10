using AgendaTenis.Core.Identity.AcessoDados.Repositorios;
using AgendaTenis.Core.Identity.Validators;
using FluentValidation;
using MediatR;

namespace AgendaTenis.Core.Identity.Aplicacao.CriarConta;

public class CriarContaCommand : IRequest<CriarContaResponse>
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string SenhaConfirmacao { get; set; }
}

public class CriarContaCommandValidator : AbstractValidator<CriarContaCommand>
{
    private readonly IIdentityRepositorio _identityRepositorio;

    public CriarContaCommandValidator(IIdentityRepositorio identityRepositorio)
    {
        _identityRepositorio = identityRepositorio;

        RuleFor(x => x.Email)
            .CustomAsync(async (email, contexto, CancellationToken) =>
            {
                var emailValidator = new EmailValidator();
                var resultadoValidacao = emailValidator.Validate(email);
                if (!resultadoValidacao.IsValid)
                {
                    foreach (var erro in resultadoValidacao.Errors)
                    {
                        contexto.AddFailure(erro);
                    }
                    return;
                }

                var emailExiste = await _identityRepositorio.EmailJaExiste(email);
                if (emailExiste)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure()
                    {
                        PropertyName = "Email",
                        ErrorMessage = "E-mail já cadastrado."
                    });
                }
            });

        RuleFor(x => x.Senha)
            .SetValidator(new SenhaValidator());

        RuleFor(x => x.SenhaConfirmacao)
            .Equal(x => x.Senha)
            .WithMessage("A senha e a confirmação de senha devem ser iguais.");
    }
}
