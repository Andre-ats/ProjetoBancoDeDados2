namespace Domain.CasosDeUso.CadastrarUsuario.Input;

public class CadastrarUsuarioUseCaseInput
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }

    public CadastrarUsuarioUseCaseInput(string nome, string email, string senha, string cpf)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Cpf = cpf;
    }
}