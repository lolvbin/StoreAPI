using StoreAPI.Models;
using StoreAPI.Contracts;
using StoreAPI.DTO;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace StoreAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private AppDbContext _context;

        public ProdutoService(AppDbContext _context)
        {
            this._context = _context;
        }

        public List<Produto> Get()
        {
            return _context.Produtos.ToList();
        }

        public Produto GetById(Guid id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public Produto Create(CriarProdutoDTO dto)
        {
            var produto = new Produto
            {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool Update(Guid id, AtualizarProdutoDTO dto)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return false;

            produto.Name = dto.Name;
            produto.Price = dto.Price;
            produto.Stock = dto.Stock;

            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id) 
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return false;
            }
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }
    }
}
