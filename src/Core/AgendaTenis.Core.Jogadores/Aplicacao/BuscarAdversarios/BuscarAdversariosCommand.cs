using AgendaTenis.Core.Jogadores.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace AgendaTenis.Core.Jogadores.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosCommand : IRequest<BuscarAdversariosResponse>
{
    public Guid? UsuarioId { get; set; }
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public CategoriaEnum? Categoria { get; set; }
}
