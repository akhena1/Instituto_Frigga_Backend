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

        
        [HttpGet("filtrooferta/{id}")]
        public List<Oferta> FiltroOferta(int id)
        {

            return repositorio.FiltrarOferta(id);
            
        }
        [HttpGet("filtroreceita/{id}")]
        public List<Receita> FiltroReceita(int id)
        {
            
            return repositorio.FiltrarReceita(id);

        }
    
        
    }
}