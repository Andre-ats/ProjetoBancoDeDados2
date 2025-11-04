using Domain.CasosDeUso.CriarPedido.Input;
using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Repositorio;
using MongoDB.Driver;

namespace Domain.CasosDeUso.CriarPedido;

public class CriarPedidoUseCase
{
    public async Task<bool> CadastrarPedidoUseCase(CriarPedidoUseCaseInput input)
    {
        using var conexaoPsql = new DataBaseContextPostgres();
        var conexaoMongo = new DataBaseContextMongoDB();
        var conexaoRedis = new DataBaseContextRedis();

        try
        {
            var usuario = conexaoPsql.Usuarios.SingleOrDefault(x => x.Id == input.IdUsuario);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            conexaoPsql.Attach(usuario);

            var produto = await conexaoMongo.Produtos
                .Find(x => x.Id == input.IdProduto)
                .FirstOrDefaultAsync();
            if (produto == null)
                throw new Exception("Produto não encontrado.");

            var pedido = Pedido.CriarPedido(usuario);
            var produtoPedido = ProdutoPedido.CriarProdutoPedido(pedido, produto, input.QuantidadeProduto);

            conexaoPsql.Pedidos.Add(pedido);
            await conexaoPsql.SaveChangesAsync();

            await conexaoRedis.InsertProdutoPedidoAsync(produtoPedido, usuario);


            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao cadastrar pedido: {e.Message}");
            return false;
        }
    }
}