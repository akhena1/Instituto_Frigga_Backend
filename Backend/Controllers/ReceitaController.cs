using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost , DisableRequestSizeLimit]
        [Authorize (Roles = "1")]
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Receita>> Post([FromForm]Receita receita)
        {
            try
            {

                var fileName = "";

                var file = Request.Form.Files[0];
                var folderName = Path.Combine ("imagens");
                var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), folderName);

                if (file.Length > 0) {
                    fileName = ContentDispositionHeaderValue.Parse (file.ContentDisposition).FileName.Trim ('"');
                    var fullPath = Path.Combine (pathToSave, fileName);
                    var dbPath = Path.Combine (folderName, fileName);

                    using (var stream = new FileStream (fullPath, FileMode.Create)) {
                        file.CopyTo (stream);
                    }
                }

                receita.ImagemReceita = fileName;

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
        [Authorize (Roles = "1")]
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
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
        [Authorize (Roles = "1")]
        [Authorize (Roles = "2")]
        [Authorize (Roles = "3")]
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