using AutoMapper;
using LaChozaComercial.Data;
using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
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
        private readonly IMapper mapper;

        public PublicacionRepository(LaChozaComercialDbContext dbContext, UserManager<Usuario> userManager, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // Obtener las publicaciones del usuario autenticado
        public async Task<List<PublicacionDTO>> GetMisPublicacionesAsync(string usuarioId)
        {
            var publicaciones = await dbContext.Publicaciones
                .Where(p => p.usuarioId == usuarioId)
                .ToListAsync();

            return mapper.Map<List<PublicacionDTO>>(publicaciones);
        }

        // Crear una nueva publicación y guardarla en la base de datos
        public async Task<PublicacionDTO> CreatePublicacionAsync(CreatePublicacionRequestDTO createPublicacionDTO)
        {
            var publicacion = mapper.Map<Publicacion>(createPublicacionDTO);

            await dbContext.Publicaciones
                .AddAsync(publicacion);

            await dbContext.SaveChangesAsync();



            return mapper.Map<PublicacionDTO>(publicacion);
        }

        // Obtener todas las publicaciones desde la base de datos
        public async Task<List<PublicacionDTO>> GetPublicacionesAsync()
        {
           var publicaciones = await dbContext.Publicaciones
                .Include(p => p.autorPublicacion)
                .ToListAsync();

            return mapper.Map<List<PublicacionDTO>>(publicaciones);
        }
    }
}
