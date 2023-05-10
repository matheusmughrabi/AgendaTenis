namespace AgendaTenis.Core.Jogadores.Dominio;

public class LocalizacaoEntity : Entity
{
    public JogadorEntity Jogador { get; private set; }
    public Guid? JogadorId { get; private set; }
    public string Pais { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
}
