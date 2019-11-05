using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    public class UploadController : ControllerBase
    {
        /// <summary>
        /// Valída uma imagem, envia ela para uma pasta e retorna o seu nome e extensão de arquivo
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pasta"></param>
        /// <returns>Nome da Imagem</returns>
        [HttpPost]              
        public string UploadImg(IFormFile file,string pasta)//É dado o arquivo e o nome da pasta como argumento
        {   
            // Declara variável que irá receber o nome e extensão da imagem, que sera o que iremos armazenar no banco
            var fileName = "";

            try
            {
                    // Verifica se o arquivo enviado é realmente uma imagem
                    if (file.ContentType == "image/jpeg"||
                        file.ContentType == "image/png" ||
                        file.ContentType == "image/gif" ||
                        file.ContentType == "image/bmp" ||
                        file.ContentType == "image/jpg"  )
                        {
                            // Declara o nome do diretorio que vai armazenar as imagens
                            var folderName = Path.Combine (pasta);
                            // Declara o caminho do diretorio para salvar a imagem
                            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), folderName);


                            if (file.Length > 0) 
                            {
                                //Pega o nome da imagem, tira as aspas e adiciona data para diferenciar das outras imagens e não substituir imagens com nomes iguais
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
                            
                       } 
                       return fileName; 
                         
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }
    }
}