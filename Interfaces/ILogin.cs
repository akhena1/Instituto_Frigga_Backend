using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.ViewModel;

namespace Instituto_Frigga_Backend.Interfaces
{
    public interface ILogin
    {
         Usuario ValidaUsuario(LoginViewModel login);
    }
}