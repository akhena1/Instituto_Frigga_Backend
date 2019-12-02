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
    /* [Authorize] */
    public class CategoriaReceitaController : ControllerBase
    {
        CategoriaReceitaRepository repositorio = new CategoriaReceitaRepository();

        /// <summary>
        /// Mostra lista de Categorias de receitas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaReceita>>> Get()
        {
            var categoriaReceita = await repositorio.Listar();

            if(categoriaReceita == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada"});
            }
            
            return categoriaReceita;
        }
        /// <summary>
        /// Mostra Categoria de Receita por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaReceita>> Get(int id)
        {
            var categoriaReceita = await repositorio.BuscarPorId(id);

            if(categoriaReceita == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada para o ID informado"});
            }

            return categoriaReceita;
        }
        /// <summary>
        /// Insere dados em Categoria Receita
        /// </summary>
        /// <param name="categoriaReceita"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoriaReceita>> Post(CategoriaReceita categoriaReceita)
        {
            try
            {
                await repositorio.Salvar(categoriaReceita);
                return categoriaReceita;
            }
            catch(DbUpdateConcurrencyException ex )
            {
                return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            
        }
        /// <summary>
        /// Atualiza dados em Categoria Receita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaReceita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id , CategoriaReceita categoriaReceita)
        {
            if (id != categoriaReceita.CategoriaReceitaId)
            {
                return BadRequest(new{mensagem = "Erro de validação da categoria por ID"});
            }

            try
            {
                await repositorio.Alterar(categoriaReceita);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var categoriaReceita_valido = await repositorio.BuscarPorId(id);

                if(categoriaReceita_valido == null)
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
        /// Deleta dados de Categoria Receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaReceita>> Delete(int id)
        {
            var categoriaReceita = await repositorio.BuscarPorId(id);
            if(categoriaReceita == null)
            {
                return NotFound(new{mensagem = "Nenhuma categoria encontrada para o ID informado"});
            }
            categoriaReceita = await repositorio.Excluir(categoriaReceita);

            return categoriaReceita;
        }



    }
}