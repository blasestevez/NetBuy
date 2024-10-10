using LaChozaComercial.Models;
using System.Threading.Tasks;

namespace LaChozaComercial.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> RegisterUserAsync(Usuario usuario, string contraseña);
        Task<Usuario> LoginUserAsync(string nombreUsuario, string contraseña);
    }
}
