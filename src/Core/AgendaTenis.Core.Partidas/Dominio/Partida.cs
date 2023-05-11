using AgendaTenis.Core.Partidas.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;

namespace AgendaTenis.Core.Partidas.Dominio;

public class Partida
{
    public Partida(string desafianteId, string adversarioId, DateTime dataDaPartida, string descricaoLocal, ModeloPartidaEnum modeloDaPartida)
    {
        DesafianteId = desafianteId;
        AdversarioId = adversarioId;
        DataDaPartida = dataDaPartida;
        DescricaoLocal = descricaoLocal;
        ModeloDaPartida = modeloDaPartida;
        StatusConvite = StatusConviteEnum.Pendente;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    public string DesafianteId { get; private set; }
    public string AdversarioId { get; private set; }
    public DateTime DataDaPartida { get; private set; }
    public string DescricaoLocal { get; private set; }
    public ModeloPartidaEnum ModeloDaPartida { get; private set; }
    public StatusConviteEnum StatusConvite { get; private set; }
    public StatusPlacarEnum? StatusPlacar { get; private set; }
    public string? VencedorId { get; private set; }
    public string? JogadorWO { get; private set; }
    public List<Set> Sets { get; private set; }

    public void RegistrarPlacar(string vencedorId, List<Set> sets, string? jogadorWO)
    {
        VencedorId = vencedorId;
        Sets = sets;
        JogadorWO = jogadorWO;
        StatusPlacar = StatusPlacarEnum.Pendente;
    }

    public void ResponderConvite(StatusConviteEnum statusConvite)
    {
        StatusConvite = statusConvite;
    }
}

public class Set
{
    public Set(int numeroSet, int gamesDesafiante, int gamesAdversario, int? tiebreakDesafiante, int? tiebreakAdversario)
    {
        NumeroSet = numeroSet;
        GamesDesafiante = gamesDesafiante;
        GamesAdversario = gamesAdversario;
        TiebreakDesafiante = tiebreakDesafiante;
        TiebreakAdversario = tiebreakAdversario;
    }

    public int NumeroSet { get; set; }
    public int GamesDesafiante { get; set; }
    public int GamesAdversario { get; set; }
    public int? TiebreakDesafiante { get; set; }
    public int? TiebreakAdversario { get; set; }
}
