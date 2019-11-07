using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Microsoft.Data.SqlClient;

namespace Backend.Interfaces
{
    public interface IFiltro
    {
         void  Conexao();
         SqlConnection Conectar();
         void Desconectar();

         List<Oferta> FiltrarOferta(int id);
         List<Receita> FiltrarReceita(int id);

         Task<CategoriaProduto> BuscarPorId(int id);


         



    }
}