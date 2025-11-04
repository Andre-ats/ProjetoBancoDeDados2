using Domain.CasosDeUso.CadastrarUsuario;
using Domain.CasosDeUso.CadastrarUsuario.Input;
using Domain.Entidade.UsuarioEntidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Api.UsuarioController;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly CadastrarUsuarioUseCase _cadastrarUsuarioUseCase;

    public UsuarioController(CadastrarUsuarioUseCase cadastrarUsuarioUseCase)
    {
        _cadastrarUsuarioUseCase = cadastrarUsuarioUseCase;
    }

    /// <summary>
    /// Cadastra um novo usuário no PostgreSQL.
    /// </summary>
    /// <param name="input">Dados do usuário.</param>
    [AllowAnonymous]
    [HttpPost("cadastrar")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CadastrarUsuarioAPI([FromBody] CadastrarUsuarioUseCaseInput input)
    {
        try
        {
            if (input == null)
                return BadRequest("Corpo da requisição inválido.");

            var usuario = _cadastrarUsuarioUseCase.CriarUsuarioUseCase(input);
            if (usuario == null)
                return BadRequest("Erro ao criar usuário.");

            return CreatedAtAction(nameof(CadastrarUsuarioAPI), new { id = usuario.Id }, usuario);
        }
        catch (Exception e)
        {
            return BadRequest(new { erro = e.Message });
        }
    }
}