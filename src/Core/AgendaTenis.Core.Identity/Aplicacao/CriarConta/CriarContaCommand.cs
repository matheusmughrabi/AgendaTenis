using MediatR;

namespace AgendaTenis.Core.Identity.Aplicacao.CriarConta;

public class CriarContaCommand : IRequest<CriarContaResponse>
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string SenhaConfirmacao { get; set; }
}
