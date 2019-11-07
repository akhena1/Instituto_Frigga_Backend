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
    public class ProdutoController : ControllerBase
    {
        ProdutoRepository repositorio = new ProdutoRepository();

        /// <summary>
        /// Mostra lista de Produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produto = await repositorio.Listar();

            if(produto == null)
            {
                return NotFound(new{mensagem = "Nenhum produto encontrado"});
            }
            
            return produto;
        }
        /// <summary>
        /// Mostra produtos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await repositorio.BuscarPorId(id);

            if(produto == null)
            {
                return NotFound(new{mensagem = "Nenhum produto encontrado para o ID informado"});
            }

            return produto;
        }
        /// <summary>
        /// Insere dados em Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            try
            {
                await repositorio.Salvar(produto);
                return produto;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            
        }
        /// <summary>
        /// Atualiza dados de Produtos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult> Put(int id , Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest(new{mensagem = "Erro na validação de produto por ID"});
            }

            try
            {
                await repositorio.Alterar(produto);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var produto_valido = await repositorio.BuscarPorId(id);

                if(produto_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhum produto encontrado para o ID informado"});
                }
                else
                {
                    return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados de Produtos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = await repositorio.BuscarPorId(id);
            if(produto == null)
            {
                  return NotFound(new{mensagem = "Nenhum produto encontrado para o ID informado"});
            }
            produto = await repositorio.Excluir(produto);

            return produto;
        }
    }
}