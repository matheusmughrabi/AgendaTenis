using MediatR;

namespace AgendaTenis.Core.Jogadores.Aplicacao.AtualizarPontuacao;

public class AtualizarPontuacaoCommand : IRequest
{
    public Guid VencedorId { get; set; }
    public Guid PerdedorId { get; set; }
}
