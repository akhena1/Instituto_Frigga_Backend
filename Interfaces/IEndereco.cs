using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface IEndereco
    {
        Task<List<Endereco>> Listar();

        Task<Endereco> BuscarPorId(int id);

        Task<Endereco> Salvar(Endereco endereco);

        Task<Endereco> Alterar(Endereco endereco);

        Task<Endereco> Excluir(Endereco endereco);
    }
}