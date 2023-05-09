using MediatR;

namespace AgendaTenis.Core.Identity.Aplicacao.CriarConta;

public class CriarContaHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
{
    public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
