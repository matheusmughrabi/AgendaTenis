using AgendaTenis.Core.Identity.AcessoDados.Repositorios;
using AgendaTenis.Core.Identity.Dominio;
using AgendaTenis.Core.Identity.Token;
using AgendaTenis.Core.Identity.Utils;
using MediatR;
using System.Security.Claims;

namespace AgendaTenis.Core.Identity.Aplicacao.GerarToken;

public class GerarTokenHandler : IRequestHandler<GerarTokenCommand, GerarTokenResponse>
{
    private readonly IIdentityRepositorio _identityRepositorio;
    private readonly ISenhaHasher<UsuarioEntity> _hasher;
    private readonly GeradorDeToken _geradorDeToken;

    public GerarTokenHandler(IIdentityRepositorio identityRepositorio, ISenhaHasher<UsuarioEntity> hasher, GeradorDeToken geradorDeToken)
    {
        _identityRepositorio = identityRepositorio;
        _hasher = hasher;
        _geradorDeToken = geradorDeToken;
    }

    public async Task<GerarTokenResponse> Handle(GerarTokenCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _identityRepositorio.ObterUsuario(request.Email);

        if (usuario is null)
            return CriarResponseErro();

        var resultadoHash = _hasher.VerificarHashSenha(usuario, usuario.Senha, request.Senha);

        if (resultadoHash)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", usuario.Id.ToString())
            };

            var token = _geradorDeToken.Gerar(claims);
            return new GerarTokenResponse()
            {
                Token = token
            };
        }
        else
        {
            return CriarResponseErro();
        }
    }

    private GerarTokenResponse CriarResponseErro()
    {
        return new GerarTokenResponse()
        {
            Notificacoes = new List<BuildingBlocks.Notificacoes.Notificacao>()
            {
                new BuildingBlocks.Notificacoes.Notificacao()
                {
                    Mensagem = "E-mail ou senha incorretos",
                    Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro
                }
            }
        };
    }
}
