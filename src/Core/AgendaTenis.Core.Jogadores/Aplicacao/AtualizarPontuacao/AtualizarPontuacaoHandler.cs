using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Jogadores.Aplicacao.AtualizarPontuacao;

public class AtualizarPontuacaoHandler : IRequestHandler<AtualizarPontuacaoCommand>
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public AtualizarPontuacaoHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task Handle(AtualizarPontuacaoCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = await _jogadoresDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Estamos indo duas vezes ao banco. Talvez fosse mais interessante realizar uma única ida ao banco e já obter o vencedor e perdedor
                var vencedor = await _jogadoresDbContext.Jogador.Include(c => c.Pontuacao).FirstOrDefaultAsync(c => c.UsuarioId == request.VencedorId);
                if (vencedor is null)
                    throw new JogadorNaoEncontradoException($"Vencedor com id {request.VencedorId} não foi encontrado");

                var perdedor = await _jogadoresDbContext.Jogador.Include(c => c.Pontuacao).FirstOrDefaultAsync(c => c.UsuarioId == request.PerdedorId);
                if (perdedor is null)
                    throw new JogadorNaoEncontradoException($"Perdedor com id {request.PerdedorId} não foi encontrado");

                vencedor.AtualizarPontuacaoVencedor();
                perdedor.AtualizarPontuacaoPerdedor();

                await _jogadoresDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (JogadorNaoEncontradoException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
