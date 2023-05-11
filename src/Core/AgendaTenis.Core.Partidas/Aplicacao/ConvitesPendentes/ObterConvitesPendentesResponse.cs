using AgendaTenis.Core.Partidas.Enums;

namespace AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;

public class ObterConvitesPendentesResponse
{
    public List<Partida> Partidas { get; set; }

    public class Partida
    {
        public string Id { get; set; }
        public string DesafianteId { get; set; }
        public string AdversarioId { get; set; }
        public DateTime DataDaPartida { get; set; }
        public string DescricaoLocal { get; set; }
        public ModeloPartidaEnum ModeloDaPartida { get; set; }
    }
}
