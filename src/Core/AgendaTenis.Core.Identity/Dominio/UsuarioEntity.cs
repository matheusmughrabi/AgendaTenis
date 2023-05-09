namespace AgendaTenis.Core.Identity.Dominio;

public class UsuarioEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
