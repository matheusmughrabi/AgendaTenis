namespace AgendaTenis.Core.Jogadores.Dominio;

public class JogadorEntity : Entity
{
    private JogadorEntity(){  }

    public JogadorEntity(
        Guid usuarioId, 
        string nome, 
        string sobrenome, 
        DateTime dataNascimento, 
        string telefone, 
        LocalizacaoEntity localizacao, 
        CaracteristicaDeJogoEntity caracteristicaDeJogo)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Sobrenome = sobrenome;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Localizacao = localizacao;
        CaracteristicaDeJogo = caracteristicaDeJogo;
    }

    public Guid UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Telefone { get; private set; }
    public LocalizacaoEntity Localizacao { get; private set; }
    public Guid? LocalizacaoId { get; private set; }
    public CaracteristicaDeJogoEntity CaracteristicaDeJogo { get; private set; }
    public Guid? CaracteristicaDeJogoId { get; private set; }
}
