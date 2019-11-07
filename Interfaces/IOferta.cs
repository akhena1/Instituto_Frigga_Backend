using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface IOferta
    {
        Task<List<Oferta>> Listar();

        Task<Oferta> BuscarPorId(int id);

        Task<Oferta> Salvar(Oferta oferta);

        Task<Oferta> Alterar(Oferta oferta);

        Task<Oferta> Excluir(Oferta oferta);
    }
}