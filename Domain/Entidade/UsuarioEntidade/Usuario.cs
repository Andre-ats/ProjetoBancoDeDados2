using Domain.Entidade.PedidoEntidade;

namespace Domain.Entidade.UsuarioEntidade;

public class Usuario : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }
    public List<Pedido> ListaPedidos { get; set; } = new();
    
    private Usuario(){}

    public static Usuario CriarUsuario(string nome, string email, string senha, string cpf)
    {
        Usuario usuario = new Usuario()
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            Email = email,
            Senha = senha,
            Cpf = cpf
        };

        return usuario;
    }
}