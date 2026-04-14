using RealDougAPI.Enums;

namespace RealDougAPI.Models
{
    public class CriarPedidosDTO
    {
        public List<int> ProdutosIds { get; set; }
        public StatusPedido Status { get; set; }
        
    }
}
