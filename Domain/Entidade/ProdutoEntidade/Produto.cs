namespace Domain.Entidade.ProdutoEntidade;

public class Produto : EntidadeBase
{
    public string NomeProduto { get; set; }
    public string DescricaoProduto { get; set; }
    public string CorProduto { get; set; }
    public string DimensaoProduto { get; set; }
    public decimal PrecoProduto { get; set; }
    
    private Produto(){}

    public static Produto CriarProduto(string nome, string descricao, string cor, string dimensao, decimal preco)
    {
        Produto produto = new Produto()
        {
            Id = Guid.NewGuid(),
            NomeProduto = nome,
            DescricaoProduto = descricao,
            CorProduto = cor,
            DimensaoProduto = dimensao,
            PrecoProduto = preco
        };
        return produto;
    }
}