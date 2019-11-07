using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instituto_Frigga_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        EnderecoRepository repositorio = new EnderecoRepository();

        /// <summary>
        /// Mostra lista de endereços
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> Get()
        {
            var endereco = await repositorio.Listar();

            if(endereco == null)
            {
                return NotFound(new{mensagem = "Nenhuma endereço encontrado"});
            }
            
            return endereco;
        }
        /// <summary>
        /// Mostra endereço por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> Get(int id)
        {
            var endereco = await repositorio.BuscarPorId(id);

            if(endereco == null)
            {
                return NotFound(new{mensagem = "Nenhum endereço encontrado para o ID informado"});
            }

            return endereco;
        }
        /// <summary>
        /// Insere dados de endereço
        /// </summary>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Endereco>> Post(Endereco endereco)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var idClaim = claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();

                endereco.UsuarioId = Convert.ToInt32((idClaim.Value));

                await repositorio.Salvar(endereco);
                return endereco;
            }
            catch(DbUpdateConcurrencyException ex )
            {
                return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            
        }
        /// <summary>
        /// Atualiza dados de endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return BadRequest(new{mensagem = "Erro de validação do endereço por ID"});
            }

            try
            {
                await repositorio.Alterar(endereco);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var endereco_valido = await repositorio.BuscarPorId(id);

                if(endereco_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhum endereço encotrado para o ID informado"});
                }
                else
                {
                     return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados de endereço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Endereco>> Delete(int id)
        {
            var endereco = await repositorio.BuscarPorId(id);
            if(endereco == null)
            {
                return NotFound(new{mensagem = "Nenhum endereço encontrado para o ID informado"});
            }
            endereco = await repositorio.Excluir(endereco);

            return endereco;
        }
    }
}