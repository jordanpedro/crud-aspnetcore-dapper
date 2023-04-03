using CRUD.Domain;
using CRUD.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace CRUD.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private const string connectionString = "Server=localhost,1433;Database=Loja;User ID=sa;Password=@Jordan2030@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        public async Task<bool> CreateAsync(Produto produto)
        {
            await using (var connection = new SqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("INSERT INTO Produtos VALUES(@Nome)", new { Nome = produto.Nome }).ConfigureAwait(false);
                return retorno > 0;

            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using (var connection = new SqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("DELETE FROM Produtos Where Id = @id", new { Id = id }).ConfigureAwait(false);
                return retorno > 0;

            }
        }

        public async Task<IList<Produto>> GetAllAsync()
        {
            await using (var connection = new SqlConnection(connectionString))
            {
                return (await connection.QueryAsync<Produto>("Select Id, Nome FROM Produtos").ConfigureAwait(false)).AsList();
            }
        }

        public async Task<Produto> GetAsync(int id)
        {
            await using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Produto>("Select Id, Nome FROM Produtos Where Id = @id", new { Id = id }).ConfigureAwait(false);
            }
        }

        public async Task<bool> UpdateAsync(Produto produto)
        {
            await using (var connection = new SqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("UPDATE Produtos SET Nome = @Nome WHERE Id = @id", new { Id = produto.Id, Nome = produto.Nome }).ConfigureAwait(false);
                return retorno > 0;

            }
        }
    }
}
