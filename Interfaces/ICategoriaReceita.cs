using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface ICategoriaReceita
    {
         Task<List<CategoriaReceita>> Listar();

        Task<CategoriaReceita> BuscarPorId(int id);

        Task<CategoriaReceita> Salvar(CategoriaReceita categoriaReceita);

        Task<CategoriaReceita> Alterar(CategoriaReceita categoriaReceita);

        Task<CategoriaReceita> Excluir(CategoriaReceita categoriaReceita);
    }
}