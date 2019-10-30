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
        /// Mostra lista de tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Oferta>>> Get()
        {
            var oferta = await repositorio.Listar();

            if(oferta == null)
            {
                return NotFound();
            }
            
            return oferta;
        }
        /// <summary>
        /// Mostra tipo de usuário por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> Get(int id)
        {
            var oferta = await repositorio.BuscarPorId(id);

            if(oferta == null)
            {
                return NotFound();
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
                // Declara variável que irá receber o nome e extensão da imagem, que sera o que iremos armazenar no banco
                var fileName = "";
                // Declara uma requisição de arquivo
                var file = Request.Form.Files[0];
                    
                    // Verifica se o arquivo enviado é realmente uma imagem
                    if (file.ContentType == "image/jpeg"||
                        file.ContentType == "image/png" ||
                        file.ContentType == "image/gif" ||
                        file.ContentType == "image/bmp" ||
                        file.ContentType == "image/jpg"  )
                        {
                            // Declara o nome do diretorio que vai armazenar as imagens
                            var folderName = Path.Combine ("imagens");
                            // Declara o caminho do diretorio para salvar a imagem
                            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), folderName);


                            if (file.Length > 0) 
                            {
                                //Pega o nome da imagem, tira as aspas e adiciona data para diferenciar das outras imagens
                                fileName = ContentDispositionHeaderValue.Parse (file.ContentDisposition).FileName.Trim ('"');
                                fileName = DateTime.Now.ToFileTimeUtc().ToString() + fileName;
                                
                                // Declara o caminho completo e o caminho do banco
                                var fullPath = Path.Combine (pathToSave, fileName);
                                var dbPath = Path.Combine (folderName, fileName);
                                
                                // Cria o arquivo de fato no diretório passado
                                using (var stream = new FileStream (fullPath, FileMode.Create)) 
                                {
                                    file.CopyTo (stream);
                                }
                            }
                            
                            // Declara que o atributo Imagem em oferta será o nome do arquivo recebido
                            oferta.ImagemProduto = fileName;
                            // Salva esse nome no Banco de dados
                            await repositorio.Salvar(oferta);
                        }
                        else
                        {
                            // \"erro\": \"Insira uma imagem válida! (jpg, jpeg, png)\" } 
                            return BadRequest(new {mensagem = "Insira uma imagem válida!"});
                        }

                
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return oferta;
        }
        
        
        
        /// <summary>
        /// Atualiza dados em Tipo Usuario
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
                return BadRequest();
            }

            try
            {
                await repositorio.Alterar(oferta);
            }
            catch(DbUpdateConcurrencyException)
            {
                var oferta_valido = await repositorio.BuscarPorId(id);

                if(oferta_valido == null)
                {
                    return NotFound();
                }
                else
                {
                   return BadRequest();
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
        [Authorize (Roles = "1 , 3")]
        public async Task<ActionResult<Oferta>> Delete(int id)
        {
            var oferta = await repositorio.BuscarPorId(id);
            if(oferta == null)
            {
                return NotFound();
            }
            oferta = await repositorio.Excluir(oferta);

            return oferta;
        }
    }
}