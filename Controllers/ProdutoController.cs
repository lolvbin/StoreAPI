using Microsoft.AspNetCore.Mvc;
using RealDougAPI.Contracts;
using RealDougAPI.Models;
using System.Collections.Immutable;
using RealDougAPI.DTO;

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
        public ActionResult Post([FromBody] CriarProdutoDTO dto)
        {
            var novoProduto = _produtoService.Create(dto);

            return CreatedAtAction(nameof(Get), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AtualizarProdutoDTO dto)
        {
            var atualizado = _produtoService.Update(id, dto);

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

            return Ok(new { Message = $"Produto com ID {id} removido com sucesso!" });
        }
    }
}
