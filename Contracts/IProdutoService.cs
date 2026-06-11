using StoreAPI.Models;
using StoreAPI.DTO;

namespace StoreAPI.Contracts
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
