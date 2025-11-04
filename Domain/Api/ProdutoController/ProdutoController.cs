using Domain.CasosDeUso.CadastrarProduto;
using Domain.CasosDeUso.CadastrarProduto.Input;
using Domain.Entidade.ProdutoEntidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Domain.Api.ProdutoController;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly CadastrarProdutoUseCase _cadastrarProdutoUseCase;
    private readonly IMongoCollection<Produto> _produtos;

    public ProdutoController(CadastrarProdutoUseCase cadastrarProdutoUseCase)
    {
        _cadastrarProdutoUseCase = cadastrarProdutoUseCase;
        var mongo = new Domain.Repositorio.DataBaseContextMongoDB();
        _produtos = mongo.Produtos;
    }

    /// <summary>
    /// Cadastra um novo produto no MongoDB.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("cadastrar")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarProdutoAPI([FromBody] CadastrarProdutoUseCaseInput input)
    {
        try
        {
            if (input == null)
                return BadRequest("Corpo da requisição inválido.");

            var id = await _cadastrarProdutoUseCase.CriarProdutoUseCase(input);
            var inserted = await _produtos.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (inserted == null)
                return BadRequest("Erro ao salvar produto no MongoDB.");

            return CreatedAtAction(nameof(CadastrarProdutoAPI), new { id = inserted.Id }, inserted);
        }
        catch (Exception e)
        {
            return BadRequest(new { erro = e.Message });
        }
    }

    /// <summary>
    /// Lista todos os produtos do MongoDB.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("listar")]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarProdutos()
    {
        var lista = await _produtos.Find(_ => true).ToListAsync();
        return Ok(lista);
    }

    /// <summary>
    /// Busca um produto pelo ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarProduto(Guid id)
    {
        var produto = await _produtos.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (produto == null)
            return NotFound("Produto não encontrado.");
        return Ok(produto);
    }
}
