namespace Domain.CasosDeUso.CriarPedido.Input;

public class CriarPedidoUseCaseInput
{
    public Guid IdProduto { get; set; }
    public Guid IdUsuario { get; set; }
    public int QuantidadeProduto { get; set; }

    public CriarPedidoUseCaseInput(Guid idProduto, Guid idUsuario, int quantidadeProduto)
    {
        IdProduto = idProduto;
        IdUsuario = idUsuario;
        QuantidadeProduto = quantidadeProduto;
    }
}