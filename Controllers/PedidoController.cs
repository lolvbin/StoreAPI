using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts;
using StoreAPI.DTO;
using StoreAPI.Models;
using StoreAPI.Responses;
using Microsoft.AspNetCore.Authorization;

namespace StoreAPI.Controllers
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
        [Authorize(Roles = "Admin, Vendedor")] // Este endpoint requer autenticação.
        public ActionResult<string> Get()
        {
            var pedidos = _pedidoService.Get();

            if (pedidos.Any())
            {
                return Ok(new object[] { new APIResponse<IEnumerable<Pedido>>(true, "Pedidos encontrados.", pedidos) });
            }
            return NotFound(new APIResponse<string>(false, $"Nenhum pedido foi encontrado!", null));
        }

        // GET api/<PedidosController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Vendedor")] // Este endpoint requer autenticação.
        public ActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new APIResponse<string>(false, "ID invalido!", null));
            }

            var pedidoExiste = _pedidoService.GetById(id);

            if (pedidoExiste == null)
            {
                return NotFound(new APIResponse<string>(false, $"Pedido com id {id} não encontrado.", null));
            }
            return Ok(new APIResponse<Pedido>(true, "Pedido encontrado.", pedidoExiste));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Vendedor")] // Este endpoint requer autenticação.
        public ActionResult Post([FromBody] CriarPedidoDTO pedidoDTO)
        {
            var pedido = _pedidoService.Create(pedidoDTO);

            if (pedido == null)
            {
                return NotFound(new APIResponse<string>(false, "Nenhum produto válido foi encontrado.", null));
            }

            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }
    }
}
