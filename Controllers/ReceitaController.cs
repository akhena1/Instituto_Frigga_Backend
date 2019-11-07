using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        /// Mostra lista de Receitas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get()
        {
            var receita = await repositorio.Listar();

            if(receita == null)
            {
                return NotFound(new{mensagem = "Nenhuma receita encontrada"});
            }
            
            return receita;
        }
        /// <summary>
        /// Mostra Receitas por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(int id)
        {
            var receita = await repositorio.BuscarPorId(id);

            if(receita == null)
            {
                return NotFound(new{mensagem = "Nenhuma receita encontrada para o ID informado"});
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
                UploadController upload = new UploadController();
                var file = Request.Form.Files[0];

                receita.ImagemReceita = upload.UploadImg(file, "ImagensReceita");

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var idClaim = claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();

                receita.UsuarioId = Convert.ToInt32((idClaim.Value));

                
                await repositorio.Salvar(receita);
                
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            return receita;
            
        }
        /// <summary>
        /// Atualiza dados em Receita
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
                return BadRequest(new{mensagem = "Erro de validação da receita por ID"});
            }

            try
            {
                await repositorio.Alterar(receita);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var receita_valido = await repositorio.BuscarPorId(id);

                if(receita_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhuma receita encontrada para o ID informado"});
                }
                else
                {
                     return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados em Receitas
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
                return NotFound(new{mensagem = "Nenhuma receita encontrada para o ID informado"});
            }
            receita = await repositorio.Excluir(receita);

            return receita;
        }
    }
}