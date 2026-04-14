using Microsoft.AspNetCore.Mvc;
using RealDougAPI.Models;

namespace RealDougAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private static List<Pedidos> pedidos = new List<Pedidos>();

        // GET: api/<PedidosController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(pedidos);
        }

        // GET api/<PedidosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id == null)
            {
                return NotFound(new { Message = $"O seu pedido com ID {id} não foi encontrado ou não foi feito!" });
            }
            return Ok(pedidos.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] CriarPedidosDTO pedidoDTO) 
        {
            var produtosDoPedido = ProdutosController.produtos.Where(p => pedidoDTO.ProdutosIds.Contains(p.Id)).ToList();

            if (!produtosDoPedido.Any())
            {
                return BadRequest("Nenhum produto válido foi encontrado.");
            }

            var pedido = new Pedidos
            {
                Id = pedidos.Count + 1,
                Data = DateTime.Now,
                Status = pedidoDTO.Status,
                Produtos = produtosDoPedido
            };

            pedidos.Add(pedido);

            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }
    }
}
