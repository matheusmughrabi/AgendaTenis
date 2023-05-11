using AgendaTenis.BuildingBlocks.Notificacoes;
using AgendaTenis.Core.Partidas.Dominio;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderConvite;

public class ResponderConviteHandler : IRequestHandler<ResponderConviteCommand, ResponderConviteResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ResponderConviteHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ResponderConviteResponse> Handle(ResponderConviteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var partida = await _partidaRepositorio.ObterPorIdAsync(request.Id.ToString());

            var notificacoes = ExecutarValidacoes(partida);
            if (notificacoes.Any(c => c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro || c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso))
            {
                return new ResponderConviteResponse()
                {
                    Sucesso = false,
                    Notificacoes = notificacoes
                };
            }

            partida.ResponderConvite(request.StatusConvite);

            var atualizou = await _partidaRepositorio.Update(partida);

            if (atualizou)
            {
                return new ResponderConviteResponse()
                {
                    Sucesso = true
                };
            }
            else
            {
                return new ResponderConviteResponse()
                {
                    Sucesso = false,
                    Notificacoes = new List<BuildingBlocks.Notificacoes.Notificacao>()
                    {
                        new BuildingBlocks.Notificacoes.Notificacao()
                        {
                            Mensagem = "Partida não foi atualizada",
                            Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
                        }
                    }
                };
            }
        }
        catch (Exception)
        {
            return new ResponderConviteResponse()
            {
                Sucesso = false,
                Notificacoes = new List<BuildingBlocks.Notificacoes.Notificacao>()
                    {
                        new BuildingBlocks.Notificacoes.Notificacao()
                        {
                            Mensagem = "Erro ao atualizar partida",
                            Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro
                        }
                    }
            };
        }
        
    }

    private List<Notificacao> ExecutarValidacoes(Partida partida)
    {
        var notificacoes = new List<Notificacao>();

        if (partida is null)
        {
            notificacoes.Add(new BuildingBlocks.Notificacoes.Notificacao()
            {
                Mensagem = "Partida não encontrada",
                Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
            });

            return notificacoes;
        }

        return notificacoes;
    }
}
