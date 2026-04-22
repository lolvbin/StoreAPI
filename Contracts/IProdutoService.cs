using RealDougAPI.Models;

namespace RealDougAPI.Contracts
{
    public interface IProdutoService
    {
        List<Produto> GetAll();
        Produto GetById(int id);
        Produto Create(Produto produto);
        bool Update(int id, Produto produtoAtualizado);
        bool Delete(int id);


    }
}
