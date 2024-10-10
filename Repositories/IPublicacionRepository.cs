using LaChozaComercial.Models;

namespace LaChozaComercial.Repositories
{
    public interface IPublicacionRepository
    {
        Task<IEnumerable<Publicacion>> GetMisPublicacionesAsync(string usuarioId);
        Task<Publicacion> CreatePublicacionAsync(Publicacion publicacion);
        Task<IEnumerable<Publicacion>> GetPublicacionesAsync();

    }
}
