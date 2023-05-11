using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;

public class ObterConvitesPendentesCommand : IRequest<ObterConvitesPendentesResponse>
{
    public string UsuarioId { get; set; }
}
