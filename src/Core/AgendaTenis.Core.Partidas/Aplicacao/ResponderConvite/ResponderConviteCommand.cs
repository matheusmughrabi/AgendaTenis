using AgendaTenis.Core.Partidas.Enums;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ResponderConvite;

public class ResponderConviteCommand : IRequest<ResponderConviteResponse>
{
    public string Id { get; set; }
    public StatusConviteEnum StatusConvite { get; set; }
}
