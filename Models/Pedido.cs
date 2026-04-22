using RealDougAPI.Enums;

namespace RealDougAPI.Models;
public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public List<Produto> Produtos { get; set; }
    public StatusPedido Status { get; set; }
}

