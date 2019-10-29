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
    [Authorize (Roles = "1")]
    public class CategoriaProdutoController : ControllerBase
    {
        CategoriaProdutoRepository repositorio = new CategoriaProdutoRepository();

        /// <summary>
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaProduto>>> Get()
        {
            var categoriaProduto = await repositorio.Listar();

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
            var categoriaProduto = await repositorio.BuscarPorId(id);

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
                await repositorio.Salvar(categoriaProduto);
                return categoriaProduto;
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
        /// <param name="categoriaProduto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , CategoriaProduto categoriaProduto)
        {
            if (id != categoriaProduto.CategoriaProdutoId)
            {
                return BadRequest();
            }

            try
            {
                await repositorio.Alterar(categoriaProduto);
            }
            catch(DbUpdateConcurrencyException)
            {
                var categoriaProduto_valido = await repositorio.BuscarPorId(id);

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
            var categoriaProduto = await repositorio.BuscarPorId(id);
            if(categoriaProduto == null)
            {
                return NotFound();
            }
            categoriaProduto =  await repositorio.Excluir(categoriaProduto);

            return categoriaProduto;
        }



    }
}