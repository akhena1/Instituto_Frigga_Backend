using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface IProduto
    {
        Task<List<Produto>> Listar();

        Task<Produto> BuscarPorId(int id);

        Task<Produto> Salvar(Produto produto);

        Task<Produto> Alterar(Produto produto);

        Task<Produto> Excluir(Produto produto);
    }
}