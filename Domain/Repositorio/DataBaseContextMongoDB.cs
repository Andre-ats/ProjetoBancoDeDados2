using Domain.Entidade.ProdutoEntidade;
using MongoDB.Driver;

namespace Domain.Repositorio;

public class DataBaseContextMongoDB
{
    public IMongoDatabase Database { get; }
    public IMongoCollection<Produto> Produtos { get; }
    
    public DataBaseContextMongoDB()
    {
        var connectionString =
            "mongodb://mongo:wahhXkumXZVrPgKPSsuMKZPoRnOMyrIF@yamabiko.proxy.rlwy.net:56966";
        var databaseName = "railway";

        var clientSettings = MongoClientSettings.FromConnectionString(connectionString);

        clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(clientSettings);
        Database = client.GetDatabase(databaseName);
        Produtos = Database.GetCollection<Produto>("produtos");
    }
    
}