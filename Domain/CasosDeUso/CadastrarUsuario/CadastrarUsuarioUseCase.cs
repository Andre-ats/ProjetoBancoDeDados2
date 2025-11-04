using Domain.CasosDeUso.CadastrarUsuario.Input;
using Domain.Entidade.UsuarioEntidade;
using Domain.Repositorio;

namespace Domain.CasosDeUso.CadastrarUsuario;

public class CadastrarUsuarioUseCase
{
        
    public Usuario CriarUsuarioUseCase(CadastrarUsuarioUseCaseInput usuarioUseCaseInput)
    {
        Usuario usuario = Usuario.CriarUsuario(usuarioUseCaseInput.Nome, usuarioUseCaseInput.Email, usuarioUseCaseInput.Senha, usuarioUseCaseInput.Cpf);

        var DbConection = new DataBaseContextPostgres();

        try
        {
            DbConection.Add(usuario);
            DbConection.SaveChanges();
            return usuario;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}