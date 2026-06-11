using StoreAPI.Enums;
using FluentValidation;
using FluentValidation.Results;

namespace StoreAPI.Models;
public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Data { get; set; }
    public List<Produto> Produtos { get; set; }
    public StatusPedido Status { get; set; }
}

