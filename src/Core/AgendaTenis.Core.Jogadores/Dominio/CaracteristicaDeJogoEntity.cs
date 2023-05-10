namespace AgendaTenis.Core.Jogadores.Dominio;

public class CaracteristicaDeJogoEntity : Entity
{
    public JogadorEntity Jogador { get; private set; }
    public Guid? JogadorId { get; private set; }
    public string MaoDominante { get; private set; }
    public string Backhand { get; private set; }
    public string EstiloDeJogo { get; private set; }
}
