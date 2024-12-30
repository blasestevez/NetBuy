using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;

namespace LaChozaComercial.Repositories
{
    public interface IPublicacionRepository
    {
        Task<List<PublicacionDTO>> GetMisPublicacionesAsync(string usuarioId);
        Task<PublicacionDTO> CreatePublicacionAsync(CreatePublicacionRequestDTO createPublicacionDTO);
        Task<List<PublicacionDTO>> GetPublicacionesAsync();

    }
}
