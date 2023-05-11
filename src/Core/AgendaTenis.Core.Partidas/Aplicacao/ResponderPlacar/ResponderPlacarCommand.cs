using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderPlacar;

public class ResponderPlacarCommand : IRequest<ResponderPlacarResponse>
{
    public string Id { get; set; }
    public bool ConfirmarPlacar { get; set; }
}
