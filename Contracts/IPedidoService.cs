using RealDougAPI.DTO;
using RealDougAPI.Enums;
using RealDougAPI.Models;

namespace RealDougAPI.Contracts
{
    public interface IPedidoService
    {
        List<Pedido> GetAll();
        Pedido GetById(int id);
        Pedido Create(CriarPedidoDTO pedidoDTO);
    }
}
