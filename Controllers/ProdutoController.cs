using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts;
using StoreAPI.Models;
using StoreAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using StoreAPI.Responses;

namespace StoreAPI.Controllers;

    [ApiController]
    [Route("api/produtos")]
    // Controller protegida contra usuarios não autenticados.
    [Authorize]
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
        [AllowAnonymous] // Permite acesso a este endpoint mesmo sem autenticação.
        public ActionResult Get()
        {
            var produtos = _produtoService.Get();

            if (!produtos.Any())
                return NotFound(new APIResponse<string>(false, "Nenhum produto encontrado!", null));

            return Ok(new APIResponse<IEnumerable<Produto>>(true, "Produtos encontrados!", produtos));
        }

        // GETBYID api/<ProdutosController>/3f2504e0-2f89-41d3-9a0c-0305d82c5b11
        [HttpGet("{id}")]
        [AllowAnonymous] // Permite acesso a este endpoint mesmo sem autenticação.
        public ActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new APIResponse<string>(false, "ID inválido!", null));

            var produto = _produtoService.GetById(id);

            if (produto == null)
                return NotFound(new APIResponse<string>(false, $"Produto com ID {id} não encontrado!", null));

            return Ok(new APIResponse<Produto>(true, "Produto encontrado!", produto));
        }

        // POST api/<ProdutosController>
        [HttpPost]
        [Authorize(Roles = "Admin,Vendedor")] // Apenas usuários com as roles "Admin" ou "Vendedor" podem acessar este endpoint.
        public ActionResult Post([FromBody] CriarProdutoDTO dto)
        {
            var novoProduto = _produtoService.Create(dto);

            return CreatedAtAction(nameof(Get), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Vendedor")] // Apenas usuários com as roles "Admin" ou "Vendedor" podem acessar este endpoint.
        public ActionResult Put(Guid id, [FromBody] AtualizarProdutoDTO dto)
        {
            var atualizado = _produtoService.Update(id, dto);

            if (!atualizado)
                return NotFound(new APIResponse<string>(false, $"Produto {id} não encontrado!", null));

            return Ok(new APIResponse<string>(true, "Produto atualizado com sucesso!", null));
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Apenas usuários com a role "Admin" podem acessar este endpoint.
        public ActionResult Delete(Guid id)
        {
            var removido = _produtoService.Delete(id);

            if (!removido)
                return NotFound(new { Message = $"Produto com ID {id} não encontrado!" });

            return Ok(new APIResponse<string>(true, $"Produto com ID {id} removido com sucesso!", null));
        }
    }