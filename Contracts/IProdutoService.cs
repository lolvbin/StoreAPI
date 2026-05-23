using RealDougAPI.Models;
using RealDougAPI.DTO;

namespace RealDougAPI.Contracts
{
    public interface IProdutoService
    {
        List<Produto> GetAll();
        Produto GetById(int id);
        Produto Create(CriarProdutoDTO dto);
        bool Update(int id, AtualizarProdutoDTO dto);
        bool Delete(int id);


    }
}
