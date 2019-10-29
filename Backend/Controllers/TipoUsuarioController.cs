using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            var tipoUsuario = await _context.TipoUsuario.ToListAsync();

            if(tipoUsuario == null)
            {
                return NotFound();
            }
            
            return tipoUsuario;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);

            if(tipoUsuario == null)
            {
                return NotFound();
            }

            return tipoUsuario;
        }
        /// <summary>
        /// Insere dados em TipoUsuario
        /// </summary>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipoUsuario)
        {
            try
            {
                await _context.AddAsync(tipoUsuario);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return tipoUsuario;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1")]
        public async Task<ActionResult> Put(int id , TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.TipoUsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(tipoUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var tipoUsuario_valido = await _context.TipoUsuario.FindAsync();

                if(tipoUsuario_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados em Tipo usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id)
        {
            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);
            if(tipoUsuario == null)
            {
                return NotFound();
            }
            _context.TipoUsuario.Remove(tipoUsuario);
            await _context.SaveChangesAsync();

            return tipoUsuario;
        }



    }
}