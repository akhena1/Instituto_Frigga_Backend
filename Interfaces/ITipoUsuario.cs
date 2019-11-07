using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface ITipoUsuario
    {
        Task<List<TipoUsuario>> Listar();

        Task<TipoUsuario> BuscarPorId(int id);

        Task<TipoUsuario> Salvar(TipoUsuario tipoUsuario);

        Task<TipoUsuario> Alterar(TipoUsuario tipoUsuario);

        Task<TipoUsuario> Excluir(TipoUsuario tipoUsuario);

    }
}