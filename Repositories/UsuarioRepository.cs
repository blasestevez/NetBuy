using AutoMapper;
using LaChozaComercial.Data;
using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LaChozaComercial.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly NetBuyDbContext dbContext;
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;

        public UsuarioRepository(NetBuyDbContext dbContext, UserManager<Usuario> userManager, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // Registro de usuario tomando como parámetro un DTO enviado por el controlador
        public async Task<UsuarioDTO> RegisterUserAsync(CreateUsuarioRequestDTO usuarioRequestDTO)
        {
            // Mapea el DTO como Domain Model
            var usuario = mapper.Map<Usuario>(usuarioRequestDTO);

            // Crea el usuario utilizando el Domain Model
            var result = await userManager.CreateAsync(usuario, usuarioRequestDTO.password);

            // Verifica si la creacion fue exitosa
            if (result.Succeeded)
            {
                // Devuelve el usuario mapeado como DTO
                return mapper.Map<UsuarioDTO>(usuario); 
            }

            // Lanza una excepción si hubo errores
            throw new Exception("Error al registrar el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }


        // Inicio de sesión tomando como parámetro un nombre de usuario y una contraseña enviadas por el controlador
        public async Task<UsuarioDTO> LoginUserAsync(string nombreUsuario, string contraseña)
        {
            // Busca en la base de datos si el usuario existe
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UserName == nombreUsuario);
            
            // Si el usuario no existe
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Si el usuario existe chequea que la contraseña sea válida en el usuario de Identity
            var isPasswordValid = await userManager.CheckPasswordAsync(usuario, contraseña);
            
            // Si la contraseña no es correcta
            if (!isPasswordValid)
            {
                throw new Exception("Contraseña incorrecta");
            }

            // Si el inicio de sesión fue exitoso, devuelve el usuario mapeado como DTO
            return mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
