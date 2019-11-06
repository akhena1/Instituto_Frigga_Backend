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
        /// Mostra lista das categorias de produtos
        /// </summary>
        /// <returns>Categoria de Produtos</returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaProduto>>> Get()
        {
            var categoriaProduto = await repositorio.Listar();

            if(categoriaProduto == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada"});
            }
            
            return categoriaProduto;
        }
        /// <summary>
        /// Mostra categoria de produtos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categoria de produto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProduto>> Get(int id)
        {
            var categoriaProduto = await repositorio.BuscarPorId(id);

            if(categoriaProduto == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada para o ID informado"});
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
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new{mensagem = "Erro no envio de dados: " + ex});
            }
            
        }
        /// <summary>
        /// Atualiza dados de categorias de produtos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaProduto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , CategoriaProduto categoriaProduto)
        {
            if (id != categoriaProduto.CategoriaProdutoId)
            {
                return BadRequest(new{mensagem = "Erro de validação da categoria por ID"});
            }

            try
            {
                await repositorio.Alterar(categoriaProduto);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var categoriaProduto_valido = await repositorio.BuscarPorId(id);

                if(categoriaProduto_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhuma categoria encontrada para o ID informado"});
                }
                else
                {
                    return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados de Categoria de Produtos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaProduto>> Delete(int id)
        {
            var categoriaProduto = await repositorio.BuscarPorId(id);
            if(categoriaProduto == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada para o ID informado"});
            }
            categoriaProduto =  await repositorio.Excluir(categoriaProduto);

            return categoriaProduto;
        }



    }
}