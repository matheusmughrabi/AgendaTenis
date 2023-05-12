using AgendaTenis.Core.Jogadores.Enums;

namespace AgendaTenis.Core.Jogadores.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorResponse
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public int Idade { get; set; }
    public double Pontuacao { get; set; }
    public CategoriaEnum Categoria { get; set; }
}
