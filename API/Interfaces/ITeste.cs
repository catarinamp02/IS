using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Interfaces
{
    public interface ITeste
    {
        Task InserirTeste(Testes teste, SqlParameter mensagem);
        Task AtualizarTeste(Testes teste, SqlParameter mensagem);
        Task RemoverTeste(int id, SqlParameter mensagem);
        Task<IEnumerable<Testes>> ConsultarTestes();
        Task<Testes> ConsultarTestesPorProduto(int id);
    }


    public class TesteRepos(IConfiguration config) : ITeste
    {
        private string connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task AtualizarTeste(Testes teste, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("AtualizarTeste", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID_Teste",teste.ID_Teste);
            command.Parameters.AddWithValue("@ID_Produto", teste.ID_Produto);
            command.Parameters.AddWithValue("@Codigo_Resultado", teste.Codigo_Resultado);
            command.Parameters.AddWithValue("@Data_Teste", teste.Data_Teste);
            command.Parameters.Add(mensagem);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Testes>> ConsultarTestes()
        {
            var testes = new List<Testes>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("ConsultarTestes", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                testes.Add(new Testes
                {
                    ID_Teste = reader.GetInt32(0),
                    ID_Produto = reader.GetInt32(1),
                    Codigo_Resultado = reader.GetString(2),
                    Data_Teste = DateOnly.FromDateTime(reader.GetDateTime(3))
                });
            }
            return testes;
        }

        public async Task<Testes> ConsultarTestesPorProduto(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("ConsultarTestesPorProduto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID_Produto", id);
     

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            Testes _teste = new();

            while (await reader.ReadAsync())
            {
                _teste = new()
                {
                    ID_Teste = reader.GetInt32(0),
                    ID_Produto = reader.GetInt32(1),
                    Codigo_Resultado = reader.GetString(2),
                    Data_Teste = DateOnly.FromDateTime(reader.GetDateTime(3))
                };  
            }
            return _teste;
        }

        public async Task InserirTeste(Testes teste, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("InserirTeste", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID_Produto", teste.ID_Produto);
            command.Parameters.AddWithValue("@Codigo_Resultado", teste.Codigo_Resultado);
            command.Parameters.AddWithValue("@Data_Teste", teste.Data_Teste);
            command.Parameters.Add(mensagem);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task RemoverTeste(int id, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("RemoverTeste", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID_Teste", id);
            command.Parameters.Add(mensagem);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}