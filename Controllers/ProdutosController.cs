using Microsoft.AspNetCore.Mvc;
using RealDougAPI.Models;
using System.Collections.Immutable;

namespace RealDougAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        public static List<Produtos> produtos = new List<Produtos>();

        // GET: api/<ProdutosController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(produtos);
        }

        // GET api/<ProdutosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if(id == null)
            {
                return NotFound(new { Message = $"O produto com ID {id} não foi encontrado ou não existe!"});
            }
            return Ok(produtos.FirstOrDefault(x => x.Id == id));
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public ActionResult Post([FromBody] Produtos produto)
        {
            produtos.Add(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produtos produtoAtualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            produto.Name = produtoAtualizado.Name;
            produto.Price = produtoAtualizado.Price;
            produto.Stock = produtoAtualizado.Stock;

            return Ok(produto);
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound(new { Message = $"O produto com ID {id} não foi encontrado!" });
            }

            produtos.Remove(produto);
            return Ok(new { Message = $"O produto com o ID {id} foi removido com sucesso!" });

        }
    }
}
