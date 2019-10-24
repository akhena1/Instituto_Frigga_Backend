using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produto = await _context.Produto.ToListAsync();

            if(produto == null)
            {
                return NotFound();
            }
            
            return produto;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if(produto == null)
            {
                return NotFound();
            }

            return produto;
        }
        /// <summary>
        /// Insere dados em Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            try
            {
                await _context.AddAsync(produto);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return produto;
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var produto_valido = await _context.Produto.FindAsync();

                if(produto_valido == null)
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
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if(produto == null)
            {
                return NotFound();
            }
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}