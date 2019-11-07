using System.Threading.Tasks;
using Backend.Domains;
using Backend.ViewModel;

namespace Backend.Interfaces
{
    public interface ILogin
    {
         Usuario ValidaUsuario(LoginViewModel login);
    }
}