using Domain.CasosDeUso.CadastrarProduto;
using Domain.CasosDeUso.CadastrarProduto.Input;
using Domain.CasosDeUso.CadastrarUsuario;
using Domain.CasosDeUso.CadastrarUsuario.Input;
using Domain.CasosDeUso.CriarPedido;
using Domain.CasosDeUso.CriarPedido.Input;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Repositorio;
using MongoDB.Driver;     

/*Console.WriteLine("Oi");
//7fe149fc-4f8e-4513-8f13-868d2df225f7

var mongo = new DataBaseContextMongoDB(); 

/*Usuario usuario = Usuario.CriarUsuario("Teste", "teste@gmail.com", "OIOIawkdakdwad", "11111111111");
new CadastrarUsuarioUseCase().CriarUsuarioUseCase(new CadastrarUsuarioUseCaseInput(usuario.Nome, usuario.Email, usuario.Senha, usuario.Cpf));*/


/*var useCase = new CadastrarProdutoUseCase();
var id = await useCase.CriarProdutoUseCase(
    new CadastrarProdutoUseCaseInput("Teste 0202", "Descricao", "Azul", "10x10", 10m));

var inserted = await mongo.Produtos.Find(x => x.Id == id).FirstOrDefaultAsync();
Console.WriteLine($"Inserido no Mongo: {inserted?.Id} - {inserted?.NomeProduto}");

var useCase = new CriarPedidoUseCase();

var ok = await useCase.CadastrarPedidoUseCase(
    new CriarPedidoUseCaseInput(
        Guid.Parse("7fe149fc-4f8e-4513-8f13-868d2df225f7"),
        Guid.Parse("21a7b31c-a7b7-4263-8ffb-e69b89aac360"),
        5
    )
);

Console.WriteLine(ok ? "Pedido criado" : "Falha ao criar pedido");*/

using System.Text;
using Domain.CasosDeUso.CadastrarProduto;
using Domain.CasosDeUso.CadastrarProduto.Input;
using Domain.CasosDeUso.CadastrarUsuario;
using Domain.CasosDeUso.CadastrarUsuario.Input;
using Domain.CasosDeUso.CriarPedido;
using Domain.CasosDeUso.CriarPedido.Input;
using Domain.Repositorio;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// 🧩 URLs para API
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// 🧩 CORS liberado
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🧩 Controllers + JSON settings
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

// 🧩 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BancoDeDadosProjeto API",
        Version = "v1",
        Description = "API de integração PostgreSQL + MongoDB + Redis"
    });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

// 🧩 Injeção de dependência (DI)
builder.Services.AddScoped<DataBaseContextPostgres>();
builder.Services.AddScoped<DataBaseContextMongoDB>();
builder.Services.AddScoped<DataBaseContextRedis>();

builder.Services.AddScoped<CadastrarProdutoUseCase>();
builder.Services.AddScoped<CadastrarUsuarioUseCase>();
builder.Services.AddScoped<CriarPedidoUseCase>();

var app = builder.Build();

// 🧩 CORS
app.UseCors();

// 🧩 Swagger (sempre habilitado)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BancoDeDadosProjeto API v1");
    c.RoutePrefix = string.Empty; // abre direto na raiz
});

// 🧩 Auth (placeholder)
app.UseAuthentication();
app.UseAuthorization();

// 🧩 Controllers
app.MapControllers();

app.Run();
