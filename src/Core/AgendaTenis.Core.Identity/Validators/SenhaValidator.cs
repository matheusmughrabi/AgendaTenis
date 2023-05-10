using FluentValidation;

namespace AgendaTenis.Core.Identity.Validators;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha)
                .NotNull()
                .WithMessage("Senha deve ser preenchida.")
                .Length(10, 20)
                .WithMessage("Senha deve ter entre 10 e 20 caracteres.");

    }
}
