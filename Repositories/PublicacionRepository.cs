using LaChozaComercial.Data;
using LaChozaComercial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaChozaComercial.Repositories
{
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly LaChozaComercialDbContext dbContext;
        private readonly UserManager<Usuario> userManager;

        public PublicacionRepository(LaChozaComercialDbContext dbContext, UserManager<Usuario> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        // Obtener las publicaciones del usuario autenticado
        public async Task<IEnumerable<Publicacion>> GetMisPublicacionesAsync(string usuarioId)
        {
            return await dbContext.Publicaciones
                .Where(p => p.usuarioId == usuarioId)
                .ToListAsync();
        }

        // Crear una nueva publicación y guardarla en la base de datos
        public async Task<Publicacion> CreatePublicacionAsync(Publicacion publicacion)
        {
            await dbContext.Publicaciones
                .AddAsync(publicacion);

            await dbContext.SaveChangesAsync();
            return publicacion;
        }

        // Obtener todas las publicaciones desde la base de datos
        public async Task<IEnumerable<Publicacion>> GetPublicacionesAsync()
        {

            return await dbContext.Publicaciones
                .Include(p => p.autorPublicacion)
                .ToListAsync();
        }
    }
}
