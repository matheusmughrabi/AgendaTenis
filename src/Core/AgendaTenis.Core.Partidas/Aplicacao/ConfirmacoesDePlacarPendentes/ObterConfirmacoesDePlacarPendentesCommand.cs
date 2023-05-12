using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConfirmacoesDePlacarPendentes;

public class ObterConfirmacoesDePlacarPendentesCommand : IRequest<ObterConfirmacoesDePlacarPendentesResponse>
{
    public string UsuarioId { get; set; }
}
