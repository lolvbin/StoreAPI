using RealDougAPI.DTO;
using RealDougAPI.Enums;
using RealDougAPI.Models;

namespace RealDougAPI.Contracts
{
    public interface IPedidoService
    {
        List<Pedido> Get();
        Pedido GetById(Guid id);
        Pedido Create(CriarPedidoDTO pedidoDTO);
    }
}
