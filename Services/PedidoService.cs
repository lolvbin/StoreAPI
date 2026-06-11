using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts;
using StoreAPI.Controllers;
using StoreAPI.DTO;
using StoreAPI.Models;

namespace StoreAPI.Services
{
    public class PedidoService : IPedidoService
    {

        private AppDbContext _context;
        private readonly IProdutoService _produtoService;

        public PedidoService(IProdutoService produtoService, AppDbContext _context)
        {
            _produtoService = produtoService;
            this._context = _context;
        }

        public List<Pedido> Get()
        {
            return _context.Pedidos.Include(p => p.Produtos).ToList();
        }

        public Pedido GetById(Guid id)
        {
            return _context.Pedidos.Include(p => p.Produtos).FirstOrDefault(p => p.Id == id);
        }

        public Pedido Create(CriarPedidoDTO pedidoDTO)
        {
            var produtosDoPedido = _produtoService.Get()
                .Where(p => pedidoDTO.ProdutosIds.Contains(p.Id))
                .ToList();

            if (!produtosDoPedido.Any())
            {
                throw new Exception("Nenhum produto válido encontrado! Verifique os IDs e tente novamente.");
            }

            var pedido = new Pedido
            {
                Data = DateTime.Now,
                Status = pedidoDTO.Status,
                Produtos = produtosDoPedido
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            return pedido;

        }
    }
}
