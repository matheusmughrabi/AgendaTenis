using AgendaTenis.BuildingBlocks.Notificacoes;
using AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;
using AgendaTenis.Core.Partidas.Dominio;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;
using static AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar.RegistrarPlacarCommand;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderPlacar;

public class ResponderPlacarHandler : IRequestHandler<ResponderPlacarCommand, ResponderPlacarResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ResponderPlacarHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ResponderPlacarResponse> Handle(ResponderPlacarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var partida = await _partidaRepositorio.ObterPorIdAsync(request.Id);

            var notificacoes = ExecutarValidacoes(partida);
            if (notificacoes.Any(c => c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro || c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso))
            {
                return new ResponderPlacarResponse()
                {
                    Sucesso = false,
                    Notificacoes = notificacoes
                };
            }

            partida.ResponderPlacar(request.ConfirmarPlacar);

            var atualizou = await _partidaRepositorio.Update(partida);

            if (atualizou)
            {
                return new ResponderPlacarResponse()
                {
                    Sucesso = true
                };
            }
            else
            {
                return new ResponderPlacarResponse()
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
            return new ResponderPlacarResponse()
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

        if (partida.StatusPlacar != Enums.StatusPlacarEnum.AguardandoConfirmacao)
            notificacoes.Add(new BuildingBlocks.Notificacoes.Notificacao()
            {
                Mensagem = "Não é possível atualizar o placar, pois o status não está Aguardando Confirmação.",
                Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
            });

        return notificacoes;
    }
}
