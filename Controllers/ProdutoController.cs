using Microsoft.AspNetCore.Mvc;
using RealDougAPI.Contracts;
using RealDougAPI.Models;
using System.Collections.Immutable;

namespace RealDougAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public static List<Produto> produtos = new List<Produto>();

        // GET: api/<ProdutosController>
        [HttpGet]
        public ActionResult Get()
        {
            var produtos = _produtoService.GetAll();

            if (!produtos.Any())
                return NotFound(new { Message = "Nenhum produto encontrado!" });

            return Ok(produtos);
        }

        // GET api/<ProdutosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "ID inválido!" });

            var produto = _produtoService.GetById(id);

            if (produto == null)
                return NotFound(new { Message = $"Produto com ID {id} não encontrado!" });

            return Ok(produto);
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            var novoProduto = _produtoService.Create(produto);

            return CreatedAtAction(nameof(Get), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produtoAtualizado)
        {
            var atualizado = _produtoService.Update(id, produtoAtualizado);

            if (!atualizado)
                return NotFound(new { Message = $"Produto {id} não encontrado!" });

            return Ok(new { Message = "Produto atualizado com sucesso!" });
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var removido = _produtoService.Delete(id);

            if (!removido)
                return NotFound(new { Message = $"Produto com ID {id} não encontrado!" });

            return Ok(new { Message = "Produto removido com sucesso!" });
        }
    }
}
