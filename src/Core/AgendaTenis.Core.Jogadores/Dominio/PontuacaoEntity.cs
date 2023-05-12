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

    public void AtualizarPontuacao(double pontos)
    {
        PontuacaoAtual += pontos;

        // Se o jogador está com zero pontos e perder o jogo ele continua com zero. Não vamos deixar ele com uma pontuação negativa.
        if (PontuacaoAtual < 0)
            PontuacaoAtual = 0;
    }
}
