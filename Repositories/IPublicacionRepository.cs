using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;

namespace LaChozaComercial.Repositories
{
    public interface IPublicacionRepository
    {
        Task<IEnumerable<PublicacionDTO>> GetMisPublicacionesAsync(string usuarioId);
        Task<Publicacion> CreatePublicacionAsync(CreatePublicacionRequestDTO createPublicacionDTO);
        Task<IEnumerable<PublicacionDTO>> GetPublicacionesAsync();

    }
}
