using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using StackExchange.Redis;

namespace Domain.Repositorio;

public record UsuarioSnapshot(string Nome, string Email, string Cpf);
public record ProdutoSnapshot(string NomeProduto, decimal PrecoProduto);
public record ProdutoPedidoRedisDto(
    Guid Id,
    Guid IdPedido,
    Guid IdProduto,
    int Quantidade,
    UsuarioSnapshot Usuario,
    ProdutoSnapshot Produto
);

public class DataBaseContextRedis : IAsyncDisposable
{
    private readonly IConnectionMultiplexer _connection;
    private readonly IDatabase _db;
    private const string Prefix = "produto_pedido:";

    public DataBaseContextRedis()
    {
        var conn =
            "growing-cobra-27510.upstash.io:6379," +
            "password=AWt2AAIncDIzOTliNGU2MzE2YTM0MGRhOTNmNTU5OTY3ODg5OWE2ZHAyMjc1MTA," +
            "user=default," +
            "ssl=True," +
            "abortConnect=false," +
            "connectTimeout=10000," +   
            "syncTimeout=10000";        

        _connection = ConnectionMultiplexer.Connect(conn);
        _db = _connection.GetDatabase();
    }

    public async Task InsertProdutoPedidoAsync(ProdutoPedido produtoPedido, Usuario usuario)
    {
        var dto = new ProdutoPedidoRedisDto(
            produtoPedido.Id,
            produtoPedido.IdPedido,
            produtoPedido.IdProduto,
            produtoPedido.QuantidadeProdutoUnico,
            new UsuarioSnapshot(usuario.Nome, usuario.Email, usuario.Cpf),
            new ProdutoSnapshot(
                produtoPedido.Produto.NomeProduto,
                produtoPedido.Produto.PrecoProduto
            )
        );

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        var value = JsonSerializer.Serialize(dto, jsonOptions);
        await _db.StringSetAsync($"{Prefix}{dto.Id}", value);

        Console.WriteLine($"✅ Gravado no Redis: {dto.Id}");
    }


    public async Task<ProdutoPedido?> GetProdutoPedidoAsync(Guid id)
    {
        var value = await _db.StringGetAsync($"{Prefix}{id}");
        if (value.IsNullOrEmpty) return null;
        return JsonSerializer.Deserialize<ProdutoPedido>(value!);
    }

    public async Task DeleteProdutoPedidoAsync(Guid id)
    {
        await _db.KeyDeleteAsync($"{Prefix}{id}");
    }

    public ValueTask DisposeAsync()
    {
        _connection.Dispose();
        return ValueTask.CompletedTask;
    }
}