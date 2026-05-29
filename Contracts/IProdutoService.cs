using RealDougAPI.Models;
using RealDougAPI.DTO;

namespace RealDougAPI.Contracts
{
    public interface IProdutoService
    {
        List<Produto> Get();
        Produto GetById(Guid id);
        Produto Create(CriarProdutoDTO dto);
        bool Update(Guid id, AtualizarProdutoDTO dto);
        bool Delete(Guid id);


    }
}
