using RealDougAPI.Contracts;
using RealDougAPI.Controllers;
using RealDougAPI.DTO;
using RealDougAPI.Models;

namespace RealDougAPI.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IProdutoService _produtoService;

        public PedidoService(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        private static List<Pedido> pedidos = new List<Pedido>();

        public List<Pedido> GetAll()
        {
            return pedidos;
        }

        public Pedido GetById(int id)
        {
            return pedidos.FirstOrDefault(p => p.Id == id);
        }

        public Pedido Create(CriarPedidoDTO pedidoDTO)
        {
            var produtosDoPedido = _produtoService.GetAll()
                .Where(p => pedidoDTO.ProdutosIds.Contains(p.Id))
                .ToList();

            if (!produtosDoPedido.Any())
            {
                throw new Exception("Nenhum produto válido encontrado! Verifique os IDs e tente novamente.");
            }

            var pedido = new Pedido
            {
                Id = pedidos.Count + 1,
                Data = DateTime.Now,
                Status = pedidoDTO.Status,
                Produtos = produtosDoPedido
            };

            pedidos.Add(pedido);

            return pedido;

        }
    }
}
