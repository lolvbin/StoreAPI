using RealDougAPI.Models;
using RealDougAPI.Contracts;
using RealDougAPI.DTO;

namespace RealDougAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private static List<Produto> produtos = new List<Produto>();

        public List<Produto> GetAll()
        {
            return produtos;
        }

        public Produto GetById(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public Produto Create(CriarProdutoDTO dto)
        {
            var produto = new Produto
            {
            Id = produtos.Count + 1, // Implementar Guid futuramente 👀
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
            };

            produtos.Add(produto);
            return produto;
        }

        public bool Update(int id, AtualizarProdutoDTO dto)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return false;

            produto.Name = dto.Name;
            produto.Price = dto.Price;
            produto.Stock = dto.Stock;

            return true;
        }

        public bool Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return false;
            }
            
            produtos.Remove(produto);
            return true;
        }
    }
}
