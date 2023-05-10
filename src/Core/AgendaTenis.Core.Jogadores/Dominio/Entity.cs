namespace AgendaTenis.Core.Jogadores.Dominio;

public class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime DataCriacao { get; private set; }
}
