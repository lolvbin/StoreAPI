using RealDougAPI.Enums;

namespace RealDougAPI.DTO
{
    public class CriarPedidosDTO
    {
        public List<int> ProdutosIds { get; set; }
        public StatusPedido Status { get; set; }
        
    }
}
