using AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConfirmacoesDePlacarPendentes;

public class ObterConfirmacoesDePlacarPendentesHandler : IRequestHandler<ObterConfirmacoesDePlacarPendentesCommand, ObterConfirmacoesDePlacarPendentesResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ObterConfirmacoesDePlacarPendentesHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ObterConfirmacoesDePlacarPendentesResponse> Handle(ObterConfirmacoesDePlacarPendentesCommand request, CancellationToken cancellationToken)
    {
        var partidas = await _partidaRepositorio.ObterConfirmacoesDePlacarPendentes(request.UsuarioId);
        return new ObterConfirmacoesDePlacarPendentesResponse()
        {
            Partidas = partidas.Select(p => new ObterConfirmacoesDePlacarPendentesResponse.Partida()
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
