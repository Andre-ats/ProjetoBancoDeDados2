1 - Descricao projeto:
- Este projeto é um sistema de pedidos desenvolvido em .NET 8, integrando três bancos de dados diferentes, cada um responsável por uma parte específica do domínio da aplicação.
O PostgreSQL armazena usuários e pedidos, o MongoDB guarda os produtos, e o Redis é utilizado como armazenamento rápido para snapshots de ProdutoPedido, garantindo consultas ágeis e eficientes.

2 - Linguagens: 
- Linguagem c# dotnet.

Instalacao do DOTNET

sudo apt update
sudo apt install -y dotnet-sdk-8.0

3 - Banco de Dados:
Pasta Domain/Repositorios
- Bancos usados: Redis/PSQL/MongoDB
- Ira aparecer todas as conexoes de banco, precisa entrar em um por um, ajustar as string de conexao para um banco local ou um banco online.
- Para o psql precisa rodar a build na base de dados.
