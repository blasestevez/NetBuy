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
            // Buscar las publicaciones en la base de datos
            var publicaciones = await dbContext.Publicaciones
                .Where(p => p.usuarioId == usuarioId)
                .ToListAsync();

            // Devuelve una lista de las publicaciones mapeadas como DTOs
            return mapper.Map<List<PublicacionDTO>>(publicaciones);
        }

        // Crea una publicación con el DTO recibido por el controlador y la guarda en la base de datos
        public async Task<PublicacionDTO> CreatePublicacionAsync(CreatePublicacionRequestDTO createPublicacionDTO)
        {
            // Mapea la publicacion a Domain Model desde el DTO
            var publicacion = mapper.Map<Publicacion>(createPublicacionDTO);

            // Guarda en la base de datos
            await dbContext.Publicaciones
                .AddAsync(publicacion);

            await dbContext.SaveChangesAsync();


            // Devuelve la publicacion mapeada a DTO nuevamente
            return mapper.Map<PublicacionDTO>(publicacion);
        }

        // Obtiene todas las publicaciones de la base de datos
        public async Task<List<PublicacionDTO>> GetPublicacionesAsync()
        {
            // Busca las publicaciones en la base de datos
            var publicaciones = await dbContext.Publicaciones
                .Include(p => p.autorPublicacion)
                .ToListAsync();

            // Las devuelve mapeadas como una lista de DTOs
            return mapper.Map<List<PublicacionDTO>>(publicaciones);
        }
    }
}
