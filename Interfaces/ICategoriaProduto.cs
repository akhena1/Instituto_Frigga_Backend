using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
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