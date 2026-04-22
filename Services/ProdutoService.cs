using RealDougAPI.Models;
using RealDougAPI.Contracts;

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

        public Produto Create(Produto produto)
        {
            produto.Id = produtos.Count + 1;
            produtos.Add(produto);
            return produto;
        }

        public bool Update(int id, Produto produtoAtualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return false;

            produto.Name = produtoAtualizado.Name;
            produto.Price = produtoAtualizado.Price;

            return true;
        }

        public bool Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return false;

            produtos.Remove(produto);
            return true;
        }
    }
}
