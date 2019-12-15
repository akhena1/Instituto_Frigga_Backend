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
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository repositorio = new UsuarioRepository();

        /// <summary>
        /// Mostra lista de Usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuario = await repositorio.Listar();

            if(usuario == null)
            {
                return NotFound(new{mensagem = "Nenhum usuário encontrado"});
            }
            
            return usuario;
        }
        /// <summary>
        /// Exibe usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await repositorio.BuscarPorId(id);

            if(usuario == null)
            {
                return NotFound(new{mensagem = "Nenhum usuário encontrado para o ID informado"});
            }

            return usuario;
        }
        /// <summary>
        /// Insere dados em Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                DocValidatorController docValidator = new DocValidatorController();

                bool ValidaDoc = false;
                if( usuario.CnpjCpf.Length == 11)
                {
                    ValidaDoc = docValidator.ValidaCpf(usuario.CnpjCpf);
                }
                if( usuario.CnpjCpf.Length == 14 )
                {
                    ValidaDoc = docValidator.ValidaCnpj(usuario.CnpjCpf);
                }     
                

                if(ValidaDoc == false)
                {
                    return BadRequest(new{mensagem = "Erro no envio de dados"});
                }
                await repositorio.Salvar(usuario);
                return usuario;
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            
        }

        

        
        /// <summary>
        /// Atualiza dados de Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id , Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest(new{mensagem = "Erro de validção do usuário por ID"});
            }

            try
            {
                await repositorio.Alterar(usuario);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var usuario_valido = await repositorio.BuscarPorId(id);

                if(usuario_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhum usuário encontrado para o ID informado"});
                }
                else
                {
                    return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados de Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuario = await repositorio.BuscarPorId(id);
            if(usuario == null)
            {
                return NotFound(new{mensagem = "Nenhum usuário encontrado para o ID informado"});
            }
            usuario = await repositorio.Excluir(usuario);

            return usuario;
        }
    }
}