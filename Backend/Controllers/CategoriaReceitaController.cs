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
    public class CategoriaReceitaController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaReceita>>> Get()
        {
            var categoriaReceita = await _context.CategoriaReceita.ToListAsync();

            if(categoriaReceita == null)
            {
                return NotFound();
            }
            
            return categoriaReceita;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaReceita>> Get(int id)
        {
            var categoriaReceita = await _context.CategoriaReceita.FindAsync(id);

            if(categoriaReceita == null)
            {
                return NotFound();
            }

            return categoriaReceita;
        }
        /// <summary>
        /// Insere dados em CategoriaReceita
        /// </summary>
        /// <param name="categoriaReceita"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoriaReceita>> Post(CategoriaReceita categoriaReceita)
        {
            try
            {
                await _context.AddAsync(categoriaReceita);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return categoriaReceita;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaReceita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , CategoriaReceita categoriaReceita)
        {
            if (id != categoriaReceita.CategoriaReceitaId)
            {
                return BadRequest();
            }

            _context.Entry(categoriaReceita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var categoriaReceita_valido = await _context.CategoriaReceita.FindAsync();

                if(categoriaReceita_valido == null)
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
        public async Task<ActionResult<CategoriaReceita>> Delete(int id)
        {
            var categoriaReceita = await _context.CategoriaReceita.FindAsync(id);
            if(categoriaReceita == null)
            {
                return NotFound();
            }
            _context.CategoriaReceita.Remove(categoriaReceita);
            await _context.SaveChangesAsync();

            return categoriaReceita;
        }



    }
}