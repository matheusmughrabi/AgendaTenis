using AgendaTenis.Core.Partidas.Dominio;
using MediatR;

namespace AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;

public class RegistrarPlacarCommand : IRequest<RegistrarPlacarResponse>
{
    public string Id { get; set; }
    public string VencedorId { get; set; }
    public List<Set> Sets { get; set; }

    public class Set
    {
        public int NumeroSet { get; set; }
        public int GamesDesafiante { get; set; }
        public int GamesAdversario { get; set; }
        public int? TiebreakDesafiante { get; set; }
        public int? TiebreakAdversario { get; set; }
    }
}
