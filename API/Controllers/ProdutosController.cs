using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController (IProduto _repos) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            try
            {
                // Definindo o parametro de saída para a mensagem
                var mensagem = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Chama a stored procedure InserirProduto com o parametro de saída para a mensagem
                await _repos.InserirProduto(produto, mensagem);

                // Verifica a mensagem de saída
                if (!string.IsNullOrEmpty(mensagem.Value.ToString()))
                {
                    // Se a mensagem de sucesso foi configurada, retorna uma resposta de sucesso
                    return Ok(mensagem.Value.ToString());
                }

                return BadRequest("Erro ao processar a solicitação.");
            }
            catch (SqlException ex)
            { 
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult <IEnumerable <Produto>>> GetAll()
        {
            try
            {
                // Chama a stored procedure ConsultarProdutos
                var produtos = await _repos.ConsultarProdutos();

                // Verifica se a lista está vazia
                if (produtos == null || !produtos.Any())
                {
                    return NoContent();
                }
                // Retorna os produtos
                return Ok(produtos);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> ConsultarProdutoPorId(int id)
        {
            try
            {
                // Chama a stored procedure ConsultarProdutoPorId
                var produto = await _repos.ConsultarProdutoPorId(id);

                // Retorna o produto
                return Ok(produto);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Produto produto)
        {
            try
            {
                // Definindo o parametro de saída para a mensagem
                var mensagem = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Chama a stored procedure AtualizarProduto com o parametro de saída para a mensagem
                await _repos.AtualizarProduto(produto, mensagem);

                // Verifica a mensagem de saída
                if (!string.IsNullOrEmpty(mensagem.Value.ToString()))
                {
                    // Se a mensagem de sucesso foi configurada, retorna uma resposta de sucesso
                    return Ok(mensagem.Value.ToString());
                }

                return BadRequest("Erro ao processar a solicitação.");
            }
            catch (SqlException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Definindo o parametro de saída para a mensagem
                var mensagem = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Chama a stored procedure RemoverProduto com o parametro de saída para a mensagem
                await _repos.RemoverProduto(id,mensagem);

                // Verifica a mensagem de saída
                if (!string.IsNullOrEmpty(mensagem.Value.ToString()))
                {
                    // Se a mensagem de sucesso foi configurada, retorna uma resposta de sucesso
                    return Ok(mensagem.Value.ToString());
                }

                return BadRequest("Erro ao processar a solicitação.");
            }
            catch (SqlException ex)
            {
                return StatusCode(400, $" {ex.Message}");
            }
        }


    }
}
