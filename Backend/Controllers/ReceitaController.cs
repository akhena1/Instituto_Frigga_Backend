using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get()
        {
            var receita = await _context.Receita.ToListAsync();

            if(receita == null)
            {
                return NotFound();
            }
            
            return receita;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(int id)
        {
            var receita = await _context.Receita.FindAsync(id);

            if(receita == null)
            {
                return NotFound();
            }

            return receita;
        }
        /// <summary>
        /// Insere dados em Receita
        /// </summary>
        /// <param name="receita"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Receita>> Post(Receita receita)
        {
            try
            {
                await _context.AddAsync(receita);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return receita;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , Receita receita)
        {
            if (id != receita.ReceitaId)
            {
                return BadRequest();
            }

            _context.Entry(receita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var receita_valido = await _context.Receita.FindAsync();

                if(receita_valido == null)
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
        public async Task<ActionResult<Receita>> Delete(int id)
        {
            var receita = await _context.Receita.FindAsync(id);
            if(receita == null)
            {
                return NotFound();
            }
            _context.Receita.Remove(receita);
            await _context.SaveChangesAsync();

            return receita;
        }
    }
}