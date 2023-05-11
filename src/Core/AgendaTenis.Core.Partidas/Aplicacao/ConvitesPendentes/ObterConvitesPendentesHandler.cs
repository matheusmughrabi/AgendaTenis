using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;

public class ObterConvitesPendentesHandler : IRequestHandler<ObterConvitesPendentesCommand, ObterConvitesPendentesResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ObterConvitesPendentesHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ObterConvitesPendentesResponse> Handle(ObterConvitesPendentesCommand request, CancellationToken cancellationToken)
    {
        var partidas = await _partidaRepositorio.ObterPartidasPendentes(request.UsuarioId, Enums.StatusConviteEnum.Pendente);
        return new ObterConvitesPendentesResponse()
        {
            Partidas = partidas.Select(p => new ObterConvitesPendentesResponse.Partida()
            {
                Id = p.Id,
                DesafianteId = p.DesafianteId,
                AdversarioId = p.AdversarioId,
                DataDaPartida = p.DataDaPartida,
                DescricaoLocal = p.DescricaoLocal,
                ModeloDaPartida = p.ModeloDaPartida
            }).ToList()
        };
    }
}
