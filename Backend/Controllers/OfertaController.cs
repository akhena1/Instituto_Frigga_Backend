using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        OfertaRepository repositorio = new OfertaRepository();

        /// <summary>
        /// Mostra lista de Ofertas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Oferta>>> Get()
        {
            var oferta = await repositorio.Listar();

            if(oferta == null)
            {
                return NotFound(new{mensagem = "Nenhuma oferta encontrada"});
            }
            
            return oferta;
        }
        /// <summary>
        /// Mostra Oferta por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> Get(int id)
        {
            var oferta = await repositorio.BuscarPorId(id);

            if(oferta == null)
            {
                return NotFound(new{mensagem = "Nenhuma oferta encontrada para o ID informado"});
            }

            return oferta;
        }

        /// <summary>
        /// Insere dados em Oferta
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult<Oferta>> Post([FromForm] Oferta oferta)
        {
        
            try
            {
                UploadController upload =  new UploadController();
                
                var file = Request.Form.Files[0];
                oferta.ImagemProduto = upload.UploadImg(file, "ImagensOferta");            
                            

                await repositorio.Salvar(oferta);
   
            }
            catch(DbUpdateConcurrencyException ex)
            {
            return BadRequest(new{mensagem = "Erro no envio de dados" + ex});
            }
            return oferta;
        }
        
        
        
        /// <summary>
        /// Atualiza dados de Ofertas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oferta"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult> Put(int id , Oferta oferta)
        {
            if (id != oferta.OfertaId)
            {
                return BadRequest(new{mensagem = "Erro na validação da oferta por ID"});
            }

            try
            {
                await repositorio.Alterar(oferta);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var oferta_valido = await repositorio.BuscarPorId(id);

                if(oferta_valido == null)
                {
                    return NotFound(new{mensagem = "Nenhuma oferta encontrada para o ID informado"});
                }
                else
                {
                   return BadRequest(new{mensagem = "Erro na alteração de dados por ID" + ex});
                }
            }
            
            return Accepted();
        }
        
        /// <summary>
        /// Deleta dados em Ofertas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult<Oferta>> Delete(int id)
        {
            var oferta = await repositorio.BuscarPorId(id);
            if(oferta == null)
            {
                return NotFound(new{mensagem = "Nenhuma oferta encontrada para o ID informado"});
            }
            oferta = await repositorio.Excluir(oferta);

            return oferta;
        }
    }
}