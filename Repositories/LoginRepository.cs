using System.Linq;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Interfaces;
using Instituto_Frigga_Backend.ViewModel;

namespace Instituto_Frigga_Backend.Repositories
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