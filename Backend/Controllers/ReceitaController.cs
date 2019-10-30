using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        ReceitaRepository repositorio = new ReceitaRepository();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get()
        {
            var receita = await repositorio.Listar();

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
            var receita = await repositorio.BuscarPorId(id);

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
        [Authorize (Roles = "1, 2, 3")]
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

                await repositorio.Salvar(receita);
                return receita;
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1, 2, 3")]
        public async Task<ActionResult> Put(int id , Receita receita)
        {
            if (id != receita.ReceitaId)
            {
                return BadRequest();
            }

            try
            {
                await repositorio.Alterar(receita);
            }
            catch(DbUpdateConcurrencyException)
            {
                var receita_valido = await repositorio.BuscarPorId(id);

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
        [Authorize (Roles = "1, 2, 3")]
        public async Task<ActionResult<Receita>> Delete(int id)
        {
            var receita = await repositorio.BuscarPorId(id);
            if(receita == null)
            {
                return NotFound();
            }
            receita = await repositorio.Excluir(receita);

            return receita;
        }
    }
}