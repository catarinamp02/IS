using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController (IProduto _repos) : ControllerBase
    {
        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody] Produto produto)
        //{
        //    await _repos.InserirProduto(produto);
        //    return CreateAtAction(nameof);
        //}
    }
}
