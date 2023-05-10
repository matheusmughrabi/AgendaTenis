using MediatR;

namespace AgendaTenis.Core.Identity.Aplicacao.GerarToken;

public class GerarTokenCommand : IRequest<GerarTokenResponse>
{
    public string Email { get; set; }
    public string Senha { get; set; }
}
