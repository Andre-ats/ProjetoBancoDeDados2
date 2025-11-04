using Domain.Entidade.PedidoEntidade.EnumsPedidoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;

namespace Domain.Entidade.PedidoEntidade;

public class Pedido : EntidadeBase
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public List<ProdutoPedido> ListaProdutos { get; set; } = new();
    
    private Pedido(){}

    public static Pedido CriarPedido(Usuario usuario)
    {
        Pedido pedido = new Pedido()
        {
            Id = Guid.NewGuid(),
            Usuario = usuario,
            UsuarioId = usuario.Id,
        };
        
        return pedido;
    }
}