﻿using AgendaTenis.Core.Partidas.Repositorios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using AgendaTenis.BuildingBlocks.Cache;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;

public class ObterConvitesPendentesHandler : IRequestHandler<ObterConvitesPendentesCommand, ObterConvitesPendentesResponse>
{
    private readonly IPartidasRepositorio _partidaRepositorio;
    private readonly IDistributedCache _cache;

    public ObterConvitesPendentesHandler(IPartidasRepositorio partidaRepositorio, IDistributedCache cache)
    {
        _partidaRepositorio = partidaRepositorio;
        _cache = cache;
    }

    public async Task<ObterConvitesPendentesResponse> Handle(ObterConvitesPendentesCommand request, CancellationToken cancellationToken)
    {
        string recordId = $"_partidas_convitespendentes_{request.UsuarioId}";
        var convites = await _cache.GetRecordAsync<ObterConvitesPendentesResponse>(recordId);

        if (convites is null)
        {
            var partidas = await _partidaRepositorio.ObterPartidasPendentes(request.UsuarioId, Enums.StatusConviteEnum.Pendente);
            
            convites = new ObterConvitesPendentesResponse()
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

            await _cache.SetRecordAsync(recordId, convites, TimeSpan.FromMinutes(2));
        }

        return convites;
    }
}
