using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestesController(ITeste _repos) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Testes teste)
        {
            try
            {
                // Definindo o parametro de saída para a mensagem
                var mensagem = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Chama a stored procedure InserirTeste com o parametro de saída para a mensagem
                await _repos.InserirTeste(teste, mensagem);

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
        public async Task<ActionResult<IEnumerable<Testes>>> GetAll()
        {
            try
            {
                // Chama a stored procedure ConsultarTestes
                var testes = await _repos.ConsultarTestes();

                // Verifica se a lista está vazia
                if (testes == null || !testes.Any())
                {
                    return NoContent();
                }
                // Retorna os testes
                return Ok(testes);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Testes>> ConsultarTestesPorProduto(int id)
        {
            try
            {
                // Chama a stored procedure ConsultarTestesPorProduto
                var teste = await _repos.ConsultarTestesPorProduto(id);

                // Retorna o teste
                return Ok(teste);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Testes teste)
        {
            try
            {
                // Definindo o parametro de saída para a mensagem
                var mensagem = new SqlParameter("@Mensagem", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Chama a stored procedure AtualizarTeste com o parametro de saída para a mensagem
                await _repos.AtualizarTeste(teste, mensagem);

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

                // Chama a stored procedure RemoverTeste com o parametro de saída para a mensagem
                await _repos.RemoverTeste(id, mensagem);

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
