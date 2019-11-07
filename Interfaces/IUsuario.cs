using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface IUsuario
    {
        Task<List<Usuario>> Listar();

        Task<Usuario> BuscarPorId(int id);

        Task<Usuario> Salvar(Usuario usuario);

        Task<Usuario> Alterar(Usuario usuario);

        Task<Usuario> Excluir(Usuario usuario);
    }
}