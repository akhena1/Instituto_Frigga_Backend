using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        InstitutoFriggaContext _context = new InstitutoFriggaContext();

        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private Usuario ValidaUsuario(Usuario login)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == login.Email &&  u.Senha == login.Senha);

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
                new Claim(ClaimTypes.Role, userInfo.TipoUsuarioId.ToString())
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Usuario login)
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