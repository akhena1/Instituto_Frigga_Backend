using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Repositories;
using Instituto_Frigga_Backend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        LoginRepository repositorio = new LoginRepository();

        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private Usuario ValidaUsuario(LoginViewModel login)
        {
            var usuario = repositorio.ValidaUsuario(login);

            return usuario;
        }

        private string GerarToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userInfo.TipoUsuarioId.ToString()),
                new Claim("Role", userInfo.TipoUsuarioId.ToString()),
                new Claim("Id", userInfo.UsuarioId.ToString()),
                new Claim(ClaimTypes.PrimarySid, userInfo.UsuarioId.ToString()),
            };
            
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims, 
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials : credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        /// <summary>
        /// Valida o login e gera o Toker de Autorização de acordo com nível de Permissão
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var user = ValidaUsuario(login);

            if(user != null)
            {
                var tokenString = GerarToken(user);
                response = Ok( new { token = tokenString });
            }

            return response;
        }

    }
}