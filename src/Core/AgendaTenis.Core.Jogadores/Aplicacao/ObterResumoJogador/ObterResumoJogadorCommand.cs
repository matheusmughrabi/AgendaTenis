using MediatR;

namespace AgendaTenis.Core.Jogadores.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorCommand : IRequest<ObterResumoJogadorResponse>
{
    public Guid UsuarioId { get; set; }
}
