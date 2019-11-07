using System.Collections.Generic;
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
    public class TipoUsuarioController : ControllerBase
    {
        TipoUsuarioRepository repositorio = new TipoUsuarioRepository();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            var tipoUsuario = await repositorio.Listar();

            if(tipoUsuario == null)
            {
                return NotFound(new{mensagem = "Nenhuma tipo de usuário encontrado"});
            }
            
            return tipoUsuario;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            var tipoUsuario = await repositorio.BuscarPorId(id);

            if(tipoUsuario == null)
            {
                return NotFound(new{mensagem = "Nenhum tipo de usuário encontrado para o ID informado"});
            }

            return tipoUsuario;
        }
        /// <summary>
        /// Insere dados de Tipo de Usuarios
        /// </summary>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipoUsuario)
        {
            try
            {
                await repositorio.Alterar(tipoUsuario);
                return tipoUsuario;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            
        }
        /// <summary>
        /// Atualiza dados em Tipo Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1")]
        public async Task<ActionResult> Put(int id , TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.TipoUsuarioId)
            {
                return BadRequest(new{mensagem = "Erro de validação do tipo de usuário por ID"});
            }
            
            try
            {
                await repositorio.Alterar(tipoUsuario);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var tipoUsuario_valido = await repositorio.BuscarPorId(id);

                if(tipoUsuario_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhum tipo de usuário encontrado para o ID informado"});
                }
                else
                {
                    return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
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
        public async Task<ActionResult<TipoUsuario>> Delete(int id)
        {
            var tipoUsuario = await repositorio.BuscarPorId(id);
            if(tipoUsuario == null)
            {
                return NotFound(new{mensagem = "Nenhum tipo de usuário encontrado para o ID informado"});
            }
            tipoUsuario = await repositorio.Excluir(tipoUsuario);

            return tipoUsuario;
        }



    }
}