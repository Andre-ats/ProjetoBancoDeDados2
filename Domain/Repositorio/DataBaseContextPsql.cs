using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositorio;

public class DataBaseContextPostgres : DbContext
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    public DataBaseContextPostgres(DbContextOptions<DataBaseContextPostgres> options)
        : base(options) { }

    public DataBaseContextPostgres() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                "Host=shinkansen.proxy.rlwy.net;" +
                "Port=30362;" +
                "Database=railway;" +
                "Username=postgres;" +
                "Password=KBNbcYxEIUFRQAWymJMLYnMryGEzNcKs;" +
                "Ssl Mode=Prefer;" +
                "Trust Server Certificate=true";

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.Ignore<Produto>();
        modelBuilder.Ignore<ProdutoPedido>();

        modelBuilder.Entity<Usuario>(b =>
        {
            b.ToTable("usuarios");
            b.HasKey(x => x.Id);
            b.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            b.Property(x => x.Email).IsRequired().HasMaxLength(200);
            b.Property(x => x.Senha).IsRequired().HasMaxLength(256);
            b.Property(x => x.Cpf).IsRequired().HasMaxLength(14);
            b.HasIndex(x => x.Email).IsUnique();
            b.HasIndex(x => x.Cpf).IsUnique();
            b.HasMany(x => x.ListaPedidos).WithOne(p => p.Usuario).HasForeignKey(p => p.UsuarioId);
        });

        modelBuilder.Entity<Pedido>(b =>
        {
            b.ToTable("pedidos");
            b.HasKey(x => x.Id);
            b.Property(x => x.UsuarioId).IsRequired();
            b.Ignore(x => x.ListaProdutos);
        });
    }
}