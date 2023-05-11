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

            if (partida is null)
                return new RegistrarPlacarResponse()
                {
                    Sucesso = false,
                    Notificacoes = new List<BuildingBlocks.Notificacoes.Notificacao>()
                    {
                        new BuildingBlocks.Notificacoes.Notificacao()
                        {
                            Mensagem = "Partida não encontrada",
                            Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Aviso
                        }
                    }
                };

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
}
