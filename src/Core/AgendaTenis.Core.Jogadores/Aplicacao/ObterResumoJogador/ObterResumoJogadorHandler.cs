using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Regras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Core.Jogadores.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorHandler : IRequestHandler<ObterResumoJogadorCommand, ObterResumoJogadorResponse>
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public ObterResumoJogadorHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task<ObterResumoJogadorResponse> Handle(ObterResumoJogadorCommand request, CancellationToken cancellationToken)
    {
        var categoriaRegras = new CategoriaRegras();

        var jogador = await _jogadoresDbContext.Jogador
            .AsNoTracking()
            .Where(c => c.UsuarioId == request.UsuarioId)
            .Select(p => new ObterResumoQueryModel
            {
                Id = p.Id,
                NomeCompleto = $"{p.Nome} {p.Sobrenome}",
                DataNascimento = p.DataNascimento,
                Pontuacao = p.Pontuacao.PontuacaoAtual
            }).FirstOrDefaultAsync();

        var response = new ObterResumoJogadorResponse()
        {
            Id = jogador.Id,
            NomeCompleto = jogador.NomeCompleto,
            Idade = CalcularIdade(jogador.DataNascimento),
            Categoria = categoriaRegras.ObterCategoria(jogador.Pontuacao)
        };

        return response;
    }

    private int CalcularIdade(DateTime dataNascimento)
    {
        int idade = DateTime.Now.Year - dataNascimento.Year;

        if(DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
        {
            idade--;
        }

        return idade;
    }
}

public class ObterResumoQueryModel
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public double Pontuacao { get; set; }
}
