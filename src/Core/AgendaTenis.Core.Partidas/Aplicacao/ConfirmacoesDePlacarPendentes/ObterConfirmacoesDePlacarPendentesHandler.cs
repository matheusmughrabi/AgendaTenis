﻿using AgendaTenis.BuildingBlocks.Cache;
using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConfirmacoesDePlacarPendentes;

public class ObterConfirmacoesDePlacarPendentesHandler : IRequestHandler<ObterConfirmacoesDePlacarPendentesCommand, ObterConfirmacoesDePlacarPendentesResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;
    private readonly IDistributedCache _cache;

    public ObterConfirmacoesDePlacarPendentesHandler(IPartidasRepositorio partidaRepositorio, IDistributedCache cache)
    {
        _partidaRepositorio = partidaRepositorio;
        _cache = cache;
    }

    public async Task<ObterConfirmacoesDePlacarPendentesResponse> Handle(ObterConfirmacoesDePlacarPendentesCommand request, CancellationToken cancellationToken)
    {
        string recordId = $"_partidas_confirmacoesdeplacar_{request.UsuarioId}";
        var confirmacoesDePlacar = await _cache.GetRecordAsync<ObterConfirmacoesDePlacarPendentesResponse>(recordId);

        if (confirmacoesDePlacar is null)
        {
            var partidas = await _partidaRepositorio.ObterConfirmacoesDePlacarPendentes(request.UsuarioId);
            
            confirmacoesDePlacar = new ObterConfirmacoesDePlacarPendentesResponse()
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

            await _cache.SetRecordAsync(recordId, confirmacoesDePlacar, TimeSpan.FromMinutes(2));
        }

        return confirmacoesDePlacar;
    }
}
