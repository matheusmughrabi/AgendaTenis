using AgendaTenis.Core.Jogadores.Enums;

namespace AgendaTenis.Core.Jogadores.Dominio;

public class PontuacaoEntity : Entity
{
    private PontuacaoEntity() { }

    public PontuacaoEntity(Guid jogadorId)
    {
        JogadorId = jogadorId;
        PontuacaoAtual = 0; // Todo jogador começa com zero pontos
    }

    public Guid JogadorId { get; private set; }
    public JogadorEntity Jogador { get; private set; }
    public double PontuacaoAtual { get; private set; }

    public CategoriaEnum ObterCategoria()
    {
        if (PontuacaoAtual > 1000)
        {
            return CategoriaEnum.Atp;
        }
        else if(PontuacaoAtual > 750)
        {
            return CategoriaEnum.Avancado;
        }
        else if (PontuacaoAtual > 500)
        {
            return CategoriaEnum.Intermediario;
        }
        else
        {
            return CategoriaEnum.Iniciante;
        }
    }
}
