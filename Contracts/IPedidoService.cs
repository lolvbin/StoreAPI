using StoreAPI.DTO;
using StoreAPI.Enums;
using StoreAPI.Models;

namespace StoreAPI.Contracts
{
    public interface IPedidoService
    {
        List<Pedido> Get();
        Pedido GetById(Guid id);
        Pedido Create(CriarPedidoDTO pedidoDTO);
    }
}
