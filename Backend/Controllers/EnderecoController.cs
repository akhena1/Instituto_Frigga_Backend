using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "1")]
    public class EnderecoController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> Get()
        {
            var endereco = await _context.Endereco.ToListAsync();

            if(endereco == null)
            {
                return NotFound();
            }
            
            return endereco;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<Endereco>> Get(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);

            if(endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }
        /// <summary>
        /// Insere dados em Endereco
        /// </summary>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize (Roles = "1")]
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Endereco>> Post(Endereco endereco)
        {
            try
            {
                await _context.AddAsync(endereco);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return endereco;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1")]
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult> Put(int id , Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return BadRequest();
            }

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var endereco_valido = await _context.Endereco.FindAsync();

                if(endereco_valido == null)
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
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Endereco>> Delete(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if(endereco == null)
            {
                return NotFound();
            }
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();

            return endereco;
        }
    }
}