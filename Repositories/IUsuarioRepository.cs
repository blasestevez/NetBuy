using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
using System.Threading.Tasks;

namespace LaChozaComercial.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDTO> RegisterUserAsync(CreateUsuarioRequestDTO usuarioRequestDTO);
        Task<UsuarioDTO> LoginUserAsync(string nombreUsuario, string contraseña);
    }
}
