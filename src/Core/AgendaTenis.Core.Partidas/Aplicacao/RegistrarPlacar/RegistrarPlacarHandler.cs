using AgendaTenis.BuildingBlocks.Notificacoes;
using AgendaTenis.Core.Partidas.Dominio;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;

public class RegistrarPlacarHandler : IRequestHandler<RegistrarPlacarCommand, RegistrarPlacarResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public RegistrarPlacarHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<RegistrarPlacarResponse> Handle(RegistrarPlacarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var partida = await _partidaRepositorio.ObterPorIdAsync(request.Id.ToString());

            var notificacoes = ExecutarValidacoes(partida);
            if (notificacoes.Any(c => c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro || c.Tipo == BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso))
            {
                return new RegistrarPlacarResponse()
                {
                    Sucesso = false,
                    Notificacoes = notificacoes
                };
            }

            var sets = request.Sets.Select(c => new Set(c.NumeroSet, c.GamesDesafiante, c.GamesAdversario, c.TiebreakDesafiante, c.TiebreakAdversario)).ToList();
            partida.RegistrarPlacar(request.VencedorId, sets, request.JogadorWO);

            var atualizou = await _partidaRepositorio.Update(partida);

            if (atualizou)
            {
                return new RegistrarPlacarResponse()
                {
                    Sucesso = true
                };
            }
            else
            {
                return new RegistrarPlacarResponse()
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
            return new RegistrarPlacarResponse()
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

        if (partida.StatusConvite != Enums.StatusConviteEnum.Aceito)
            notificacoes.Add(new BuildingBlocks.Notificacoes.Notificacao()
            {
                Mensagem = "Não é possível atualizar o placar, pois o convite para a partida não foi aceito",
                Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
            });

        if (DateTime.UtcNow < partida.DataDaPartida.ToUniversalTime())
            notificacoes.Add(new BuildingBlocks.Notificacoes.Notificacao()
            {
                Mensagem = "Não é possível atualizar o placar, pois a partida ainda não aconteceu",
                Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
            });

        if (partida.StatusPlacar != null)
            notificacoes.Add(new BuildingBlocks.Notificacoes.Notificacao()
            {
                Mensagem = "Placar já foi registrado.",
                Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
            });

        return notificacoes;
    }
}
