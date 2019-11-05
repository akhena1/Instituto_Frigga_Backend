using System.Collections.Generic;
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
                return NotFound();
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
                return NotFound();
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
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
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
                return BadRequest();
            }
            
            try
            {
                await repositorio.Alterar(tipoUsuario);
            }
            catch(DbUpdateConcurrencyException)
            {
                var tipoUsuario_valido = await repositorio.BuscarPorId(id);

                if(tipoUsuario_valido == null)
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
        public async Task<ActionResult<TipoUsuario>> Delete(int id)
        {
            var tipoUsuario = await repositorio.BuscarPorId(id);
            if(tipoUsuario == null)
            {
                return NotFound();
            }
            tipoUsuario = await repositorio.Excluir(tipoUsuario);

            return tipoUsuario;
        }



    }
}