using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;

namespace Domain.Entidade.ProdutoPedidoEntidade;

public class ProdutoPedido : EntidadeBase
{
    public Guid IdPedido { get; set; }
    public Pedido Pedido { get; set; }
    public Guid IdProduto { get; set; }
    public Produto Produto { get; set; }
    public int QuantidadeProdutoUnico { get; set; }
    
    private ProdutoPedido(){}
    

    public static ProdutoPedido CriarProdutoPedido(Pedido pedido, Produto produto, int quantidadeProdutoUnico)
    {
        ProdutoPedido produtoPedido = new ProdutoPedido()
        {
            Id = Guid.NewGuid(),
            IdPedido = pedido.Id,
            IdProduto = produto.Id,
            Pedido = pedido,
            Produto = produto,
            QuantidadeProdutoUnico = quantidadeProdutoUnico
        };

        return produtoPedido;
    }
}