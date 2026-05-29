using FluentValidation;
using FluentValidation.Results;

namespace RealDougAPI.Models
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
