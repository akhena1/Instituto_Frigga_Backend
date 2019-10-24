using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuario = await _context.Usuario.ToListAsync();

            if(usuario == null)
            {
                return NotFound();
            }
            
            return usuario;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }
        /// <summary>
        /// Insere dados em Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                await _context.AddAsync(usuario);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return usuario;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var usuario_valido = await _context.Usuario.FindAsync();

                if(usuario_valido == null)
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
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if(usuario == null)
            {
                return NotFound();
            }
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}