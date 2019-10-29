using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface ICategoriaProduto
    {
        Task<List<CategoriaProduto>> Listar();

        Task<CategoriaProduto> BuscarPorId(int id);

        Task<CategoriaProduto> Salvar(CategoriaProduto categoriaProduto);

        Task<CategoriaProduto> Alterar(CategoriaProduto categoriaProduto);

        Task<CategoriaProduto> Excluir(CategoriaProduto categoriaProduto);
    }
}