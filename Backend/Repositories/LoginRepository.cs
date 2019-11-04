using System.Linq;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Backend.ViewModel;

namespace Backend.Repositories
{
    public class LoginRepository : ILogin
    {
        public Usuario ValidaUsuario(LoginViewModel login)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                var usuario = _context.Usuario.FirstOrDefault(u => u.Email == login.Email &&  u.Senha == login.Senha);

                return usuario;
                
            };


        }
    }
}