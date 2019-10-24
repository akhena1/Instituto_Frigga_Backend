using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProdutoController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaProduto>>> Get()
        {
            var categoriaProduto = await _context.CategoriaProduto.ToListAsync();

            if(categoriaProduto == null)
            {
                return NotFound();
            }
            
            return categoriaProduto;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProduto>> Get(int id)
        {
            var categoriaProduto = await _context.CategoriaProduto.FindAsync(id);

            if(categoriaProduto == null)
            {
                return NotFound();
            }

            return categoriaProduto;
        }
        /// <summary>
        /// Insere dados em CategoriaProduto
        /// </summary>
        /// <param name="categoriaProduto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoriaProduto>> Post(CategoriaProduto categoriaProduto)
        {
            try
            {
                await _context.AddAsync(categoriaProduto);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return categoriaProduto;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaProduto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , CategoriaProduto categoriaProduto)
        {
            if (id != categoriaProduto.CategoriaProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(categoriaProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var categoriaProduto_valido = await _context.CategoriaProduto.FindAsync();

                if(categoriaProduto_valido == null)
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
        public async Task<ActionResult<CategoriaProduto>> Delete(int id)
        {
            var categoriaProduto = await _context.CategoriaProduto.FindAsync(id);
            if(categoriaProduto == null)
            {
                return NotFound();
            }
            _context.CategoriaProduto.Remove(categoriaProduto);
            await _context.SaveChangesAsync();

            return categoriaProduto;
        }



    }
}