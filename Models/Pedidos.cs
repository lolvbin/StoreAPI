using RealDougAPI.Enums;

namespace RealDougAPI.Models;
public class Pedidos
    {
        public int Id { get; set; }
        public DateTime Data {  get; set; }
        public StatusPedido Status { get; set; }
        public List<Produtos> Produtos { get; set; }
    }

