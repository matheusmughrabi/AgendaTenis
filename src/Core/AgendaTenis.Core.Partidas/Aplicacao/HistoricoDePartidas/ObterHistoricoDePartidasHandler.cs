﻿using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.HistoricoDePartidas;

public class ObterHistoricoDePartidasHandler : IRequestHandler<ObterHistoricoDePartidasCommand, ObterHistoricoDePartidasResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;

    public ObterHistoricoDePartidasHandler(IPartidasRepositorio partidaRepositorio)
    {
        _partidaRepositorio = partidaRepositorio;
    }

    public async Task<ObterHistoricoDePartidasResponse> Handle(ObterHistoricoDePartidasCommand request, CancellationToken cancellationToken)
    {
        if (request.Pagina <= 0 )
        {
            throw new System.ArgumentOutOfRangeException("Página deve ser um número inteiro maior do que zero");
        }

        if (request.ItensPorPagina <= 0)
        {
            throw new System.ArgumentOutOfRangeException("Página deve ser um número inteiro maior do que zero");
        }

        var partidas = await _partidaRepositorio.ObterPartidasPaginado(request.UsuarioId, request.Pagina, request.ItensPorPagina);

        return new ObterHistoricoDePartidasResponse()
        {
            Partidas = partidas.Select(p => new ObterHistoricoDePartidasResponse.Partida()
            {
                Id = p.Id,
                DesafianteId = p.DesafianteId,
                AdversarioId = p.AdversarioId,
                DataDaPartida = p.DataDaPartida,
                DescricaoLocal = p.DescricaoLocal,
                ModeloDaPartida = p.ModeloDaPartida,
                StatusConvite = p.StatusConvite,
                StatusPlacar = p.StatusPlacar,
                VencedorId = p.VencedorId
            }).ToList()
        };
    }
}
