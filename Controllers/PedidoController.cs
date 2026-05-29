using Microsoft.AspNetCore.Mvc;
using RealDougAPI.Contracts;
using RealDougAPI.DTO;
using RealDougAPI.Models;
using RealDougAPI.Services;

namespace RealDougAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/<PedidosController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            var pedidos = _pedidoService.Get();

            if (pedidos.Any())
            {
                return Ok(pedidos);
            }
            return NotFound(new { Message = $"Nenhum pedido foi encontrado!" });
        }

        // GET api/<PedidosController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { Message = "ID invalido!" });
            }

            var pedidoExiste = _pedidoService.GetById(id);

            if (pedidoExiste == null)
            {
                return NotFound(new { Message = $"Pedido com id {id} não encontrado." });
            }
            return Ok(pedidoExiste);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CriarPedidoDTO pedidoDTO)
        {
            var pedido = _pedidoService.Create(pedidoDTO);

            if (pedido == null)
            {
                return NotFound("Nenhum produto válido foi encontrado.");
            }

            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }
    }
}
