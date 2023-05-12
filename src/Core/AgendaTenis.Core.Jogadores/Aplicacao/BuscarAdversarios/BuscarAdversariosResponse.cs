using AgendaTenis.Core.Jogadores.Enums;

namespace AgendaTenis.Core.Jogadores.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosResponse
{
    public List<Adversario> Adversarios { get; set; }

    public class Adversario
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public double Pontuacao { get; set; }
        public CategoriaEnum Categoria { get; set; }
    }
}
