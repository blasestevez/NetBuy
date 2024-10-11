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
        private readonly LaChozaComercialDbContext dbContext;
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;

        public UsuarioRepository(LaChozaComercialDbContext dbContext, UserManager<Usuario> userManager, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // Registro de un nuevo usuario con UserManager y DbContext
        public async Task<UsuarioDTO> RegisterUserAsync(CreateUsuarioRequestDTO usuarioRequestDTO)
        {
            var usuario = mapper.Map<Usuario>(usuarioRequestDTO);
            var result = await userManager.CreateAsync(usuario, usuarioRequestDTO.password);

            // Verificar si la creación fue exitosa
            if (result.Succeeded)
            {
                return mapper.Map<UsuarioDTO>(usuario); 
            }

            // Lanzar una excepción si hubo errores
            throw new Exception("Error al registrar el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }


        // Iniciar sesión y devolver el usuario si la autenticación es correcta
        public async Task<UsuarioDTO> LoginUserAsync(string nombreUsuario, string contraseña)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UserName == nombreUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(usuario, contraseña);
            if (!isPasswordValid)
            {
                throw new Exception("Contraseña incorrecta");
            }

            return mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
