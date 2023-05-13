using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderConvite;

public class ResponderConviteCommand : IRequest<ResponderConviteResponse>
{
    public string Id { get; set; }
    public bool Aceitar { get; set; }
}
