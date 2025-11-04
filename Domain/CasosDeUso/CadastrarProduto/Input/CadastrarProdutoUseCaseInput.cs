namespace Domain.CasosDeUso.CadastrarProduto.Input;

public class CadastrarProdutoUseCaseInput
{
    public string NomeProduto { get; set; }
    public string DescricaoProduto { get; set; }
    public string CorProduto { get; set; }
    public string DimensaoProduto { get; set; }
    public decimal PrecoProduto { get; set; }

    public CadastrarProdutoUseCaseInput(string nomeProduto, string descricaoProduto, string corProduto, string dimensaoProduto, decimal precoProduto)
    {
        NomeProduto = nomeProduto;
        DescricaoProduto = descricaoProduto;
        CorProduto = corProduto;
        DimensaoProduto = dimensaoProduto;
        PrecoProduto = precoProduto;
    }
}