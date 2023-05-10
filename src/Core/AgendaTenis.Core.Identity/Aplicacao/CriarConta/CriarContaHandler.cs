using AgendaTenis.Core.Identity.AcessoDados.Repositorios;
using AgendaTenis.Core.Identity.Dominio;
using AgendaTenis.Core.Identity.Utils;
using MediatR;

namespace AgendaTenis.Core.Identity.Aplicacao.CriarConta;

public class CriarContaHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
{
    private readonly IIdentityRepositorio _identityRepositorio;
    private readonly ISenhaHasher<UsuarioEntity> _hasher;

    public CriarContaHandler(
        IIdentityRepositorio identityRepositorio,
        ISenhaHasher<UsuarioEntity> hasher)
    {
        _identityRepositorio = identityRepositorio;
        _hasher = hasher;
    }

    public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
    {
        var commandValidator = new CriarContaCommandValidator(_identityRepositorio);
        var resultadoValidacao = await commandValidator.ValidateAsync(request);

        if (!resultadoValidacao.IsValid)
            return new CriarContaResponse()
            {
                Notificacoes = resultadoValidacao.Errors.ToNotificacao()
            };

        var entity = new UsuarioEntity(request.Email, request.Senha, _hasher);
        var id = await _identityRepositorio.Inserir(entity);

        return new CriarContaResponse()
        {
            Id = id
        };
    }
}
