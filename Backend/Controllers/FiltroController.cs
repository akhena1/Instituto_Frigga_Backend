using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltroController : ControllerBase
    {
        FiltroRepository repositorio = new FiltroRepository();

        /// <summary>
        /// Filtra oferta por ID de Categoria Produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("filtrooferta/{id}")]
        public List<Oferta> FiltroOferta(int id)
        {

            return repositorio.FiltrarOferta(id);
            
        }

        /// <summary>
        /// Filtra receita por ID de Categoria Receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("filtroreceita/{id}")]
        public List<Receita> FiltroReceita(int id)
        {
            
            return repositorio.FiltrarReceita(id);

        }
    
        
    }
}