using AgendaTenis.Core.Partidas.Dominio;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvidarParaPartida;

public class ConvidarParaPartidaHandler : IRequestHandler<ConvidarParaPartidaCommand, ConvidarParaPartidaResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ConvidarParaPartidaHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ConvidarParaPartidaResponse> Handle(ConvidarParaPartidaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var partida = new Partida(
            request.DesafianteId.ToString(),
            request.AdversarioId.ToString(),
            request.DataDaPartida,
            request.DescricaoLocal,
            request.ModeloDaPartida);

            await _partidaRepositorio.InsertAsync(partida);

            return new ConvidarParaPartidaResponse()
            {
                Sucesso = true
            };
        }
        catch (Exception)
        {
            return new ConvidarParaPartidaResponse()
            {
                Sucesso = false,
                Notificacoes = new List<BuildingBlocks.Notificacoes.Notificacao>()
                {
                    new BuildingBlocks.Notificacoes.Notificacao()
                    {
                        Mensagem = "Erro ao gravar partida",
                        Tipo = BuildingBlocks.Notificacoes.Enums.TipoNotificacaoEnum.Erro
                    }
                }
            };
        }
        
    }
}
