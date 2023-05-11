using AgendaTenis.Core.Partidas.Dominio;
using MongoDB.Driver;

namespace AgendaTenis.Core.Partidas.Repositorios;

public class PartidasRepositorio : IPartidasRepositorio
{
    private readonly IMongoCollection<Partida> _partidasCollection;

    public PartidasRepositorio(IMongoClient client)
    {
        string databaseName = "PartidasDb";
        string collectionName = "PartidasCollection";

        var db = client.GetDatabase(databaseName);
        _partidasCollection = db.GetCollection<Partida>(collectionName);
    }

    public async Task<Partida> ObterPorIdAsync(string id)
    {
        var filter = Builders<Partida>.Filter.Eq(c => c.Id, id);

        var partida = await _partidasCollection.Find(filter).FirstOrDefaultAsync();

        return partida;
    }

    public async Task InsertAsync(Partida partida)
    {
        if (partida is null)
            throw new ArgumentNullException(nameof(partida));

        await _partidasCollection.InsertOneAsync(partida);
    }

    public async Task<bool> Update(Partida partida)
    {
        var filter = Builders<Partida>.Filter.Eq(c => c.Id, partida.Id);
        var result = await _partidasCollection.ReplaceOneAsync(filter, partida);

        return result.ModifiedCount > 0;
    }
}
