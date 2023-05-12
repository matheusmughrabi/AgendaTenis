using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Regras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Jogadores.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosHandler : IRequestHandler<BuscarAdversariosCommand, BuscarAdversariosResponse>
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public BuscarAdversariosHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task<BuscarAdversariosResponse> Handle(BuscarAdversariosCommand request, CancellationToken cancellationToken)
    {
        var categoriaRegras = new CategoriaRegras();
        var pontuacaoMinima = categoriaRegras.ObterPontuacaoMinima(request.Categoria.GetValueOrDefault());
        var pontuacaoMaxima = categoriaRegras.ObterPontuacaoMaxima(request.Categoria.GetValueOrDefault());

        var adversarios = await _jogadoresDbContext.Jogador
            .AsNoTracking()
            .Include(c => c.Pontuacao)
            .Where(c => c.UsuarioId != request.UsuarioId
                        && c.Pais == request.Pais
                        && c.Estado == request.Estado
                        && c.Cidade == request.Cidade
                        && (request.Categoria == null || (c.Pontuacao.PontuacaoAtual >= pontuacaoMinima && c.Pontuacao.PontuacaoAtual <= pontuacaoMaxima)))
            .Select(p => new BuscarAdversariosResponse.Adversario()
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NomeCompleto = $"{p.Nome} {p.Sobrenome}"
            }).ToListAsync();

        return new BuscarAdversariosResponse()
        {
            Adversarios = adversarios
        };
    }
}
