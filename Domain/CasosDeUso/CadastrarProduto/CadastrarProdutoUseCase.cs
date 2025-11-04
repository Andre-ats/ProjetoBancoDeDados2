using Domain.CasosDeUso.CadastrarProduto.Input;
using Domain.Entidade.ProdutoEntidade;
using Domain.Repositorio;

namespace Domain.CasosDeUso.CadastrarProduto;

public class CadastrarProdutoUseCase
{
    public async Task<Guid> CriarProdutoUseCase(CadastrarProdutoUseCaseInput input)
    {
        Produto produto = Produto.CriarProduto(input.NomeProduto, input.DescricaoProduto,
            input.CorProduto, input.DimensaoProduto, input.PrecoProduto);

        try
        {
            var mongo = new DataBaseContextMongoDB();
            await mongo.Produtos.InsertOneAsync(produto);
            return produto.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}