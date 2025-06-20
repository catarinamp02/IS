﻿using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace API.Interfaces
{
    public interface IProduto
    {
        Task InserirProduto(Produto produto, SqlParameter mensagem);
        Task AtualizarProduto(Produto produto, SqlParameter mensagem);
        Task RemoverProduto(int produto, SqlParameter mensagem);
        Task<IEnumerable<Produto>> ConsultarProdutos();
        Task<Produto> ConsultarProdutoPorId(int id);
    }

    public class ProdutoRepos(IConfiguration config) : IProduto
    {
        private string connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task InserirProduto(Produto produto, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("InserirProduto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Garante que Hora_Producao tenha segundos
            if (!TimeSpan.TryParseExact(produto.Hora_Producao, new[] { "hh\\:mm", "hh\\:mm\\:ss" }, null, out TimeSpan horaFormatada))
            {
                throw new ArgumentException("Formato inválido para Hora_Producao. Use HH:mm ou HH:mm:ss.");
            }

            command.Parameters.AddWithValue("@Codigo_Peca", produto.Codigo_Peca);
            command.Parameters.AddWithValue("@Data_Producao", produto.Data_Producao);
            command.Parameters.AddWithValue("@Hora_Producao", horaFormatada);
            command.Parameters.AddWithValue("@Tempo_Producao", produto.Tempo_Producao);
            command.Parameters.Add(mensagem);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }
        public async Task AtualizarProduto(Produto produto, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("AtualizarProduto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Garante que Hora_Producao tenha segundos
            if (!TimeSpan.TryParseExact(produto.Hora_Producao, new[] { "hh\\:mm", "hh\\:mm\\:ss" }, null, out TimeSpan horaFormatada))
            {
                throw new ArgumentException("Formato inválido para Hora_Producao. Use HH:mm ou HH:mm:ss.");
            }

            command.Parameters.AddWithValue("@ID_Produto", produto.ID_Produto);
            command.Parameters.AddWithValue("@Codigo_Peca", produto.Codigo_Peca);
            command.Parameters.AddWithValue("@Data_Producao", produto.Data_Producao);
            command.Parameters.AddWithValue("@Hora_Producao", horaFormatada);
            command.Parameters.AddWithValue("@Tempo_Producao", produto.Tempo_Producao);
            command.Parameters.Add(mensagem);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        public async Task RemoverProduto(int id, SqlParameter mensagem)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("RemoverProduto", connection) 
            { 
                CommandType =CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID_Produto", id);
            command.Parameters.Add(mensagem);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }
        public async Task<IEnumerable<Produto>> ConsultarProdutos()
        {
            var produtos = new List<Produto>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("ConsultarProdutos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                produtos.Add(new Produto
                {
                    ID_Produto = reader.GetInt32(0),
                    Codigo_Peca = reader.GetString(1),
                    Data_Producao = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    Hora_Producao = reader.GetTimeSpan(3).ToString(@"hh\:mm\:ss"),
                    Tempo_Producao = reader.GetInt32(4)
                });
            }
            return produtos;
        }
        public async Task<Produto> ConsultarProdutoPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("ConsultarUmProduto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID_Produto", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            Produto _produto = new();

            while (await reader.ReadAsync())
            {
                _produto = new()
                {
                    ID_Produto = reader.GetInt32(0),
                    Codigo_Peca = reader.GetString(1),
                    Data_Producao = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    Hora_Producao = reader.GetTimeSpan(3).ToString(@"hh\:mm\:ss"),
                    Tempo_Producao = reader.GetInt32(4)
                };
            }
            return _produto;    
        }
    }
}