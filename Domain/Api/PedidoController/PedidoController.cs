using Domain.CasosDeUso.CriarPedido;
using Domain.CasosDeUso.CriarPedido.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Api.PedidoController;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly CriarPedidoUseCase _criarPedidoUseCase;

    public PedidoController(CriarPedidoUseCase criarPedidoUseCase)
    {
        _criarPedidoUseCase = criarPedidoUseCase;
    }

    /// <summary>
    /// Cria um novo pedido integrando PostgreSQL, MongoDB e Redis.
    /// </summary>
    /// <param name="input">Dados do pedido (IDs do produto e do usuário + quantidade).</param>
    [AllowAnonymous]
    [HttpPost("criar")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarPedidoAPI([FromBody] CriarPedidoUseCaseInput input)
    {
        try
        {
            if (input == null)
                return BadRequest("Corpo da requisição inválido.");

            var sucesso = await _criarPedidoUseCase.CadastrarPedidoUseCase(input);

            if (!sucesso)
                return BadRequest("Falha ao criar pedido.");

            return CreatedAtAction(nameof(CriarPedidoAPI), new { idUsuario = input.IdUsuario, idProduto = input.IdProduto },
                new { mensagem = "Pedido criado com sucesso!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { erro = e.Message });
        }
    }
}